using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polygon.BL.FileManager.Interface;
using Polygon.BL.Geocoder;
using Polygon.Helper.Constant;
using Position = Polygon.Helper.JsonHelper.Position;

namespace Polygon.BL.FileManager {
    public class FileManager : IFileManager {
        public async Task WriteFile(List<string> coordinates, string fileName, int count) {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (var outputFile = new StreamWriter(Path.Combine(docPath, fileName))) {
                for (int i = 0; i < coordinates.Count - 1; i++) {
                    await WriteAsync(OsmGeocoder.Types[i], coordinates[i], outputFile, count);
                }
            }
        }

        private async Task WriteAsync(string type, string coordinate, StreamWriter outputFile, int count) {
            var text = new StringBuilder();
            switch (type) {
                case GeoTypes.Point:
                    var point = JsonConvert.DeserializeObject<Position>(coordinate);
                    text.AppendLine($"[{point.Latitude},{point.Longitude}]");
                    break;
                case GeoTypes.LineString:
                    var lineString = JsonConvert.DeserializeObject<List<Position>>(coordinate);
                    lineString = lineString.Where((_, i) => i % count == 0).ToList();
                    foreach (var position in lineString) {
                        text.AppendLine($"[{position.Latitude},{position.Longitude}]");
                    }
                    break;
                case GeoTypes.Polygon:
                case GeoTypes.MultiLineString:
                    var coordinates = JsonConvert.DeserializeObject<List<List<Position>>>(coordinate);
                    foreach (var positions in coordinates) {
                        var partPositions = positions.Where((_, i) => i % count == 0).ToList();
                        foreach (var position in partPositions) {
                            text.AppendLine($"[{position.Latitude},{position.Longitude}]");
                        }
                    }
                    break;
                case GeoTypes.MultiPolygon:
                    var multiPolygon = JsonConvert.DeserializeObject<List<List<List<Position>>>>(coordinate);
                    foreach (var polygon in multiPolygon) {
                        foreach (var positions in polygon) {
                            var partPositions = positions.Where((_, i) => i % count == 0).ToList();
                            foreach (var position in partPositions) {
                                text.AppendLine($"[{position.Latitude},{position.Longitude}]");
                            }
                        }
                    }
                    break;
            }
            if (!string.IsNullOrWhiteSpace(text.ToString())) {
                await outputFile.WriteAsync($"{text}");
            }
        }
    }
}