using Microsoft.Extensions.DependencyInjection;

namespace Payment.Solution.Dependency
{
    public interface IDependency
    {
        void Register(IServiceCollection services);
    }
}
