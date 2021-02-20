using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polygon.GeoServices.Interface {
    public interface IServices<T> {
        Task<List<string>> GetPolygon(string address);
    }
}
