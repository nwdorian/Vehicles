using Autofac;
using Vehicles.Service.Common;

namespace Vehicles.Service;
public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VehicleService>().As<IVehicleService>();
    }
}
