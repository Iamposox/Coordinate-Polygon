using Microsoft.Extensions.Configuration;
using Ninject.Modules;
using Polygon.BL.FileManager;
using Polygon.BL.FileManager.Interface;
using Polygon.BL.Geocoder;
using Polygon.BL.Geocoder.Interfaces;
using Polygon.Configuration;
using Polygon.GeoServices;
using Polygon.GeoServices.Interface;

namespace Polygon.IoC {
    public class ServiceNinjectModule : NinjectModule {
        public override void Load() {
            var config = GetConfiguration<Config>("Config.json");
            
            Bind<Config>().ToConstant(config);
            Bind<IServices<OsmService>>().To<OsmService>().InSingletonScope();
            Bind<IOsmGeocoder>().To<OsmGeocoder>().InSingletonScope();
            Bind<IFileManager>().To<FileManager>().InSingletonScope();
        }

        private TKey GetConfiguration<TKey>(string fileName) where TKey : new() {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile(fileName);
            IConfigurationRoot config = configBuilder.Build();
            var serviceConfig = new TKey();
            config.Bind(serviceConfig);
            return serviceConfig;
        }
    }
}
