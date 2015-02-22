using Autofac;

namespace Gitablog.Infrastructure
{
    public class IoC : IIoC
    {
        private IContainer _container;

        public void RegisterContainer(IContainer container)
        {
            _container = container;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
