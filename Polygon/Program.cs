using System;
using System.Threading.Tasks;
using Ninject;
using NLog;
using Polygon.BL.FileManager.Interface;
using Polygon.GeoServices;
using Polygon.GeoServices.Interface;
using Polygon.IoC;
using static System.Console;

namespace Polygon {
    public class Program {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public static async Task Main(string[] args) {
            try {
                var kernel = new StandardKernel(new ServiceNinjectModule());
                var osmService = kernel.Get<IServices<OsmService>>();
                var fileManager = kernel.Get<IFileManager>();

                Write("Введите адрес поиска: ");
                var address = ReadLine();

                Write("Введите частоту точек: ");
                int.TryParse(ReadLine(), out var point);

                Write("Введите имя файла для сохранения: ");
                var fileName = ReadLine();

                var polygon = await osmService.GetPolygon(address);

                await fileManager.WriteFile(polygon, fileName.Contains(".txt") ? fileName : fileName + ".txt", point > 0 ? point : 1);
            } catch (Exception ex) {
                _logger.Error(ex);
                throw;
            }
        }
    }
}
