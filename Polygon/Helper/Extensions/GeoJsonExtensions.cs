using System.Linq;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Polygon.Helper.Constant;
using Polygon.Helper.JsonHelper;

namespace Polygon.Helper.Extensions {
    public static class GeoJsonExtensions {
        public static string GetGeometry(this GeojsonParse osmObject, string type) {
            var GeometryString = string.Empty;
            switch (type) {
                case GeoTypes.Polygon:
                    var polygon =
                        JsonConvert.DeserializeObject<GeoJSON.Net.Geometry.Polygon>(JsonConvert.SerializeObject(osmObject));
                    GeometryString = JsonConvert.SerializeObject(polygon.Coordinates.Select(x=>x.Coordinates.Select(x=>x)));
                    break;
                case GeoTypes.MultiPolygon:
                    var multiPolygon =
                        JsonConvert.DeserializeObject<MultiPolygon>(JsonConvert.SerializeObject(osmObject));
                    GeometryString = JsonConvert.SerializeObject(multiPolygon.Coordinates.Select(x=>x.Coordinates.Select(x=>x.Coordinates.Select(x=>x))));
                    break;
                case GeoTypes.Point:
                    var point = JsonConvert.DeserializeObject<Point>(JsonConvert.SerializeObject(osmObject));
                    GeometryString = JsonConvert.SerializeObject(point.Coordinates);
                    break;
                case GeoTypes.MultiPoint:
                    var multiPoint = JsonConvert.DeserializeObject<MultiPoint>(JsonConvert.SerializeObject(osmObject));
                    GeometryString = JsonConvert.SerializeObject(multiPoint.Coordinates.Select(x => x.Coordinates));
                    break;
                case GeoTypes.LineString:
                    var lineString = JsonConvert.DeserializeObject<LineString>(JsonConvert.SerializeObject(osmObject));
                    GeometryString = JsonConvert.SerializeObject(lineString.Coordinates.Select(x=>x));
                    break;
                case GeoTypes.MultiLineString:
                    var multiLineString =
                        JsonConvert.DeserializeObject<MultiLineString>(JsonConvert.SerializeObject(osmObject));
                    GeometryString =
                        JsonConvert.SerializeObject(multiLineString.Coordinates.Select(x => x.Coordinates.Select(x=>x)));
                    break;
            }

            return GeometryString;
        }
    }
}
