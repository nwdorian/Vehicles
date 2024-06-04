using Autofac;
using Vehicles.Repository.Common;

namespace Vehicles.Repository;
public class RepositoryModule : Module
{
    private readonly string _connectionString;
    public RepositoryModule(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VehicleRepository>().As<IVehicleRepository>().WithParameter("connectionString", _connectionString);
    }
}
