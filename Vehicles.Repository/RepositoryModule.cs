using Autofac;
using Vehicles.Repository.Common;

namespace Vehicles.Repository;
public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VehicleRepository>().As<IVehicleRepository>();
    }
}
