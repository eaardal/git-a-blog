using Autofac;

namespace Gitablog.Web.App_Start
{
    public static class AutofacExtensions
    {
        public static void RegisterSingleton<T>(this ContainerBuilder builder)
        {
            builder.RegisterType<T>().AsSelf().AsImplementedInterfaces().SingleInstance();
        }
    }
}