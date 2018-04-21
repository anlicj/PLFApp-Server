using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using PLFApp.Server.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace PLFApp.Server.Web
{
    public static class DIHelper
    {
        public static IServiceCollection InitializeDI(this IServiceCollection services)
        {
            var allRumTimeAssembly = GetRunTimeAssemblies();
            var repositoryInterfaceAssembly = allRumTimeAssembly.First(m=>m.FullName.Contains("PLFApp.Server.Core"));
            var repositoryImplementAssembly = allRumTimeAssembly.First(m=>m.FullName.Contains("PLFApp.Server.EntityFrameworkCore"));
            var serviceInterfaceAssembly = allRumTimeAssembly.First(m => m.FullName.Contains("PLFApp.Service"));
            var serviceImplementAssembly = allRumTimeAssembly.First(m => m.FullName.Contains("PLFApp.Service"));
            Inject(services, repositoryInterfaceAssembly, repositoryImplementAssembly);
            Inject(services, serviceInterfaceAssembly, serviceImplementAssembly);            
            return services;
        }

        private static void Inject(IServiceCollection service,Assembly interfaceAssembly, Assembly implementAssembly)
        {
            var canUseInterfaces = interfaceAssembly.GetTypes().Where(m => m.IsInterface&&!m.GetTypeInfo().IsGenericType);
            var canUseImplements = implementAssembly.DefinedTypes.Where(m => m.IsClass && !m.IsAbstract && !m.IsGenericType);
            foreach (var type in canUseInterfaces)
            {
                var implement = canUseImplements.FirstOrDefault(m => m.ImplementedInterfaces.Any(i => i.Name == type.Name));
                if (implement!=null)
                {
                    service.AddScoped(type, implement.AsType()); 
                }
            }
        }

        private static IList<Assembly> GetRunTimeAssemblies()
        {
            var list = new List<Assembly>();
            var libs = DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");
            foreach (var lib in libs)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
                catch (Exception)
                {
                }
            }
            return list;
        }
    }
}
