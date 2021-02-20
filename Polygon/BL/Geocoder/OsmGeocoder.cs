using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polygon.BL.Geocoder.Interfaces;
using Polygon.Helper.Extensions;
using Polygon.Helper.JsonHelper;

namespace Polygon.BL.Geocoder {
    public class OsmGeocoder : IOsmGeocoder {
        public static List<string> Types = new List<string>();

        public async Task<List<string>> GeocodeAsync(string geoCode) {
            return ParseCoordinatesJson(geoCode);
        }

        private List<string> ParseCoordinatesJson(string json) {
            var geoObject = JsonConvert.DeserializeObject<List<OsmObject>>(json);
            var coordList = new List<string>();
            foreach (var osmObject in geoObject) {
                coordList.Add(osmObject.Geojson.GetGeometry(osmObject.Geojson.type));
                Types.Add(osmObject.Geojson.type);
            }
            
            return coordList;
        }

        
    }
}
