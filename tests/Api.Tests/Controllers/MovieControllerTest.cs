using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Api.Tests.Controllers
{
    public class MovieControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public MovieControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetMovies_ReturnsExpectedProducerInterval()
        {
            var movieCsvContent = "year;title;studios;producers;winner\n" +
                      "1980;Can't Stop the Music;Associated Film Distribution;Allan Carr;yes\n" +
                      "1981;The Formula;MGM, United Artists;Steve Shagan;yes\n" +
                      "1982;Cruising;Lorimar Productions, United Artists;Jerry Weintraub;\n";

            // Criar um arquivo CSV temporário para o teste
            var filePath = Path.Combine(Path.GetTempPath(), "movielist.csv");
            await File.WriteAllTextAsync(filePath, movieCsvContent);

            // Simular a leitura do CSV no método do controlador
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(filePath), "file", "movielist.csv");

            var response = await _client.GetAsync("/v1/worst-movies");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProducerIntervalResponse>(responseString);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Min);
            Assert.NotEmpty(result.Max);

            var minInterval = result.Min[0];
            var maxInterval = result.Max[0];

            Assert.Equal("Allan Carr", minInterval.Producer);
            Assert.Equal(0, minInterval.Interval);  // Intervalo de 1 ano
            Assert.Equal(1980, minInterval.PreviousWin);
            Assert.Equal(1980, minInterval.FollowingWin);

            Assert.Equal("Bo Derek", maxInterval.Producer);
            Assert.Equal(6, maxInterval.Interval);  // Intervalo de 1 ano
            Assert.Equal(1984, maxInterval.PreviousWin);
            Assert.Equal(1990, maxInterval.FollowingWin);
        }
    }
}