using Autofac;
using Vehicles.Service.Common;

namespace Vehicles.Service;
public class ServiceModule : Module /*AssemblyScanModule*/
{
    //protected override Assembly Assembly => Assembly.GetExecutingAssembly();

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VehicleService>().As<IVehicleService>().InstancePerLifetimeScope();

        //base.Load(builder);
    }
}

