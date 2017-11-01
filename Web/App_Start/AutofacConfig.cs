using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace Web
{
    public static class AutofacConfig
    {
        private static readonly string[] Assemblies = {"DataAccess", "Web"};

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var currentAssembly = Assembly.GetExecutingAssembly();
            var assemblies = BuildManager.GetReferencedAssemblies()
                .Cast<Assembly>()
                .Where(a => Assemblies.Contains(a.GetName().Name));
            assemblies = assemblies.Union(new[] {currentAssembly});

            foreach (var assembly in assemblies)
            {
                builder
                    .RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
            }

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}