using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polygon.BL.Geocoder.Interfaces {
    public interface IOsmGeocoder {
        Task<List<string>> GeocodeAsync(string geoCode);
    }
}
