using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Polygon.BL.Geocoder;
using Polygon.Configuration;
using Polygon.GeoServices.Interface;

namespace Polygon.GeoServices {
    public class OsmService : IServices<OsmService> {
        private OsmGeocoder _geocoder;
        private Config _config;

        public OsmService(OsmGeocoder geocoder, Config config) {
            _geocoder = geocoder;
            _config = config;
        }

        public async Task<List<string>> GetPolygon(string address) {
            var requestUrl = _config.RequestOsmUrl + UrlMakeValid(address) + _config.FormatResponse;
            var polygon = await _geocoder.GeocodeAsync(await DownloadString(requestUrl));
            return polygon.ToList();
        }

        private async Task<string> DownloadString(string url) {
            using WebClient wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.UserAgent, ".NET Core Test Client");
            return await wc.DownloadStringTaskAsync(url);
        }

        private string UrlMakeValid(string address) =>
            address.Replace(" ", "+").Replace("&", "").Replace("?", "");
    }
}
