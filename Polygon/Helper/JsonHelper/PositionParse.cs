using Newtonsoft.Json;

namespace Polygon.Helper.JsonHelper {
    public class Position {
        [JsonIgnore]
        public object Altitude { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
