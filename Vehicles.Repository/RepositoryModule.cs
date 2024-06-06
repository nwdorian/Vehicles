using Autofac;
using Vehicles.Repository.Common;

namespace Vehicles.Repository;
public class RepositoryModule : Module /*AssemblyScanModule*/
{
    //protected override Assembly Assembly => Assembly.GetExecutingAssembly();

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VehicleRepository>().As<IVehicleRepository>().InstancePerLifetimeScope();

        //base.Load(builder);
    }
}
