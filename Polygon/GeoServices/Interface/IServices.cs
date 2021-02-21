using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polygon.GeoServices.Interface {
    public interface IServices {
        Task<List<string>> GetPolygon(string address);
    }
}
