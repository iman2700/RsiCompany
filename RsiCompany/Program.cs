using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RsiCompany.Extensions;
 
using RsiCompany.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configures the services for adding controllers to the application.
// Enables respect for the Accept header from the browser and returns HTTP Not Acceptable (406) when appropriate.
// Inserts a JSON patch input formatter at the beginning of the input formatters list.
// Adds XML data contract serializer formatters for handling XML content negotiation.
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddXmlDataContractSerializerFormatters();


builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServiceManager();


//AddCustomCSVFormatter()
builder.Services.AddControllers()
.AddApplicationPart(typeof(RsiCompany.Presentation.AssemblyReference).Assembly);

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();
builder.Services.AddScoped<ValidationFilterAttribute>();


app.Run(async context =>
{
await context.Response.WriteAsync("Hello from the middleware component.");
});

/// <summary>
/// Retrieves the JSON patch input formatter used for handling JSON patch requests.
/// </summary>
/// <returns>An instance of NewtonsoftJsonPatchInputFormatter for processing JSON patch requests.</returns>
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() => new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services
        .BuildServiceProvider()
        .GetRequiredService<IOptions<MvcOptions>>()
        .Value
        .InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>()
        .First();




//builder.Services.ConfigureCors();
//builder.Services.ConfigureIISIntegration();

