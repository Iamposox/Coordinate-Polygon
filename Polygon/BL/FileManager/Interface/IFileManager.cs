using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polygon.BL.FileManager.Interface {
    public interface IFileManager {
        Task WriteFile(List<string> coordinates, string fileName, int count);
    }
}
