using Api.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using System.Globalization;

namespace Api.Helpers
{
    public class CsvReaderHelper
    {
        public static List<Movie> ReadCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";",
            };

            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<MovieMap>();

            return csv.GetRecords<Movie>().ToList();
        }
    }
}
