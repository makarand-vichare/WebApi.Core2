using System;

namespace WebApi.Core.Common.MEF
{
    /// <summary>
    /// Allows objects implementing IModule to register types in unity.
    /// </summary>
    public interface IModuleRegistrar
    {
        void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;

        void RegisterTypeInstanceSingleton<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;

        void RegisterTypeInstancePerHttpRequest<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;

        void RegisterType(Type type1, Type type2);
    }
}
