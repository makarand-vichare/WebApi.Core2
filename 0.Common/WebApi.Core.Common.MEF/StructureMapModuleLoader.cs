using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace WebApi.Core.Common.MEF
{
    public static class StructureMapModuleLoader
    {
        public static void LoadContainer(StructureMap.IContainer container, string path, string pattern)
        {
            var assemblies = Directory.GetFiles(path, pattern)
                                .Select(AssemblyLoadContext.GetAssemblyName)
                                .Select(AssemblyLoadContext.Default.LoadFromAssemblyName)
                                .ToList(); 

            var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
            try
            {
                using (var componsitionContainer = configuration.CreateContainer())
                {
                    IEnumerable<IModule> modules = componsitionContainer.GetExports<IModule>().Where(m => m != null);
                    var registrar = new StructureMapModuleRegistrar(container);
                    foreach (IModule module in modules)
                    {
                        module.Initialize(registrar);
                    }
                }
            }
            catch (ReflectionTypeLoadException typeLoadException)
            {
                var builder = new StringBuilder();
                foreach (Exception loaderException in typeLoadException.LoaderExceptions)
                {
                    builder.AppendFormat("{0}\n", loaderException.Message);
                }

                throw new TypeLoadException(builder.ToString(), typeLoadException);
            }
        }
    }

    internal class StructureMapModuleRegistrar : IModuleRegistrar
    {
        private readonly StructureMap.IContainer _container;

        public StructureMapModuleRegistrar(StructureMap.IContainer container)
        {
            this._container = container;
            //Register interception behaviour if any
        }

        public void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            if (withInterception)
            { 
                //register with interception 
            }
            else
            {
                this._container.Configure(
                    registry =>
                    {
                        registry.For<TFrom>().Use<TTo>();
                    }
                );
            }
        }

        public void RegisterTypeInstanceSingleton<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            this._container.Configure(
                    registry =>
                    {
                        registry.For<TFrom>().Use<TTo>().Singleton();
                    }
                );
        }

        public void RegisterTypeInstancePerHttpRequest<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            this._container.Configure(
                    registry =>
                    {
                        registry.For<TFrom>().Use<TTo>();
                    }
                );
        }

        public void RegisterType(Type tFrom, Type tTo)
        {
            this._container.Configure(
                    registry =>
                    {
                        registry.For(tFrom).Use(tTo);
                    }
                );
        }
    }
}
