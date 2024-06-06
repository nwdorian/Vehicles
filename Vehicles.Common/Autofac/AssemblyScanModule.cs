using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace Vehicles.Common.Autofac;
public abstract class AssemblyScanModule : Module
{
    protected abstract Assembly Assembly { get; }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly)
            .AsImplementedInterfaces();
    }

}
