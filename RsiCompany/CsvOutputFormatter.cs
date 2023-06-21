using Microsoft.AspNetCore.Mvc.Formatters;
using Shared.DataTransferObjects;
using System.Net.Http.Headers;
using System.Text;

namespace RsiCompany
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            // Constructor for the CsvOutputFormatter class.
            // Adds supported media types and encodings.
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
        {
            // Determines if the formatter can write the specified type.
            // Checks if the type is assignable from CompanyDto or IEnumerable<CompanyDto>.
            if (typeof(CompanyDto).IsAssignableFrom(type) ||
            typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext  context, Encoding selectedEncoding)
        {
            // Overrides the base method to write the response body asynchronously.
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            // Checks the type of the object in the context and formats it as CSV.
            if (context.Object is IEnumerable<CompanyDto>)
            {
                // Iterates through each CompanyDto object in an IEnumerable<CompanyDto>.
                foreach (var company in (IEnumerable<CompanyDto>)context.Object)
                {
                    FormatCsv(buffer, company);
                }
            }
            else
            {
                // Formats a single CompanyDto object.
                FormatCsv(buffer, (CompanyDto)context.Object);
            }
            // Writes the CSV data to the response body.
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, CompanyDto company)
        {
            // Formats a CompanyDto object as a CSV-formatted string and appends it to the buffer.
            buffer.AppendLine($"{company.Id},\"{company.Name},\"{company.FullAddress}\"");
        }
    }
}
