namespace Polygon.Helper.JsonHelper {
    public class OsmObject {
        public long PlaceId { get; set; }
        public string Licence { get; set; }
        public string OsmType { get; set; }
        public long OsmId { get; set; }
        public string[] Boundingbox { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string DisplayName { get; set; }
        public string Class { get; set; }
        public string Type { get; set; }
        public float Importance { get; set; }
        public string Icon { get; set; }
        public GeojsonParse Geojson { get; set; }
    }
}