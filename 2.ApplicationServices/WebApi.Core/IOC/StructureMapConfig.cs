using StructureMap;
using WebApi.Core.Common.MEF;

namespace WebApi.Core.IOC
{
    public static class StructureMapConfig
    {
        public static IContainer RegisterComponents()
        {
            var container = BuildContainer();

            return container;
        }

        private static IContainer BuildContainer()
        {
            var container = new Container();

            RegisterTypes(container);

            return container;
        }

        private static void RegisterTypes(IContainer container)
        {
            //Module initialization thru MEF
            StructureMapModuleLoader.LoadContainer(container, ".\\bin\\Debug\\netcoreapp2.0", "WebApi.Core.*.dll");
        }

        public static IContainer RegisterComponents(IContainer container)
        {
            RegisterTypes(container);

            return container;
        }

    }
}