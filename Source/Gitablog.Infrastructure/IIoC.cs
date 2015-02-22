using Autofac;

namespace Gitablog.Infrastructure
{
    public interface IIoC
    {
        void RegisterContainer(IContainer container);
        T Resolve<T>();
    }
}