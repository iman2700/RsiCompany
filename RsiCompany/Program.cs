using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using RsiCompany.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServiceManager();



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



app.Run(async context =>
{
await context.Response.WriteAsync("Hello from the middleware component.");
});


 
 
//builder.Services.ConfigureCors();
//builder.Services.ConfigureIISIntegration();
 
 