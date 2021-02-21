using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Polygon.BL.Geocoder;
using Polygon.Configuration;
using Polygon.GeoServices.Interface;

namespace Polygon.GeoServices {
    public class OsmService : IServices {
        private readonly OsmGeocoder _geocoder;
        private readonly Config _config;
        private readonly WebClient _webClient;

        public OsmService(OsmGeocoder geocoder, Config config, WebClient webClient) {
            _geocoder = geocoder;
            _config = config;
            _webClient = webClient;
        }

        public async Task<List<string>> GetPolygon(string address) {
            var requestUrl = _config.RequestOsmUrl + UrlMakeValid(address) + _config.FormatResponse;
            var polygon = await _geocoder.GeocodeAsync(await DownloadString(requestUrl));
            return polygon.ToList();
        }

        private async Task<string> DownloadString(string url) {
            _webClient.Headers.Add(HttpRequestHeader.UserAgent, ".NET Core Test Client");
            return await _webClient.DownloadStringTaskAsync(url);
        }

        private string UrlMakeValid(string address) =>
            address.Replace(" ", "+").Replace("&", "").Replace("?", "");
    }
}
