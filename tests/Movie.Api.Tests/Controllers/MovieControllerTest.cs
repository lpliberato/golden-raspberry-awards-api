using Newtonsoft.Json;

namespace Movie.Api.Tests
{
    public class MovieControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public MovieControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetMovies_ReturnsExpectedProducerInterval()
        {
            var response = await _client.GetAsync("v1/worst-movies");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProducerIntervalResponse>(responseString);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Min);
            Assert.NotEmpty(result.Max);

            var minInterval = result.Min[0];
            var maxInterval = result.Max[0];

            Assert.Equal("Allan Carr", minInterval.Producer);
            Assert.Equal(0, minInterval.Interval);
            Assert.Equal(1980, minInterval.PreviousWin);
            Assert.Equal(1980, minInterval.FollowingWin);

            Assert.Equal("Bo Derek", maxInterval.Producer);
            Assert.Equal(6, maxInterval.Interval);
            Assert.Equal(1984, maxInterval.PreviousWin);
            Assert.Equal(1990, maxInterval.FollowingWin);
        }
    }
}