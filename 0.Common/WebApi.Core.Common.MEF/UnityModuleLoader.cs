using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Unity;

namespace WebApi.Core.Common.MEF
{
    public static class UnityModuleLoader
    {
        public static void LoadContainer(IUnityContainer container, string path, string pattern)
        {
            var assemblies = Directory.GetFiles(path, pattern).Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);
            var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
            try
            {
                using (var componsitionContainer = configuration.CreateContainer())
                {
                    IEnumerable<IModule> modules = componsitionContainer.GetExports<IModule>().Where(m => m != null);
                    var registrar = new UnityModuleRegistrar(container);
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

    internal class UnityModuleRegistrar : IModuleRegistrar
    {
        private readonly IUnityContainer _container;

        public UnityModuleRegistrar(IUnityContainer container)
        {
            this._container = container;
            //Register interception behaviour if any
        }

        public void RegisterType(Type tFrom, Type tTo)
        {
            this._container.RegisterType(tFrom, tTo);
        }

        public void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            if (withInterception)
            { 
                //register with interception 
            }
            else
            {
                this._container.RegisterType<TFrom, TTo>();
            }
        }

        public void RegisterTypeInstancePerHttpRequest<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            throw new NotImplementedException();
        }

        public void RegisterTypeInstanceSingleton<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            throw new NotImplementedException();
        }

    }
}
