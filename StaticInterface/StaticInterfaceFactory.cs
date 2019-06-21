using System;
using System.Linq;

namespace StaticInterface
{
    public static class StaticInterfaceFactory
    {
        public static IConstructible Create(Type constructibleType, params object[] parameters)
        {
            var result = CreateFromParamlessCtor<IConstructible>(constructibleType);
            result.Initialize(parameters);
            return result;
        }

        public static TConstructible Create<TConstructible>(params object[] parameters)
            where TConstructible : class, IConstructible, new()
        {
            var result = new TConstructible();
            result.Initialize(parameters);
            return result;
        }

        internal static TResultInterface CreateFromParamlessCtor<TResultInterface>(Type constructibleType)
        {
            if (!constructibleType.GetInterfaces().Contains(typeof(TResultInterface)))
            {
                throw new ArgumentException("Provided type does not implement " + typeof(TResultInterface).FullName + ".", nameof(constructibleType));
            }

            var ctor = constructibleType.GetConstructor(new Type[0]);
            if (ctor == null)
            {
                throw new ArgumentException("Provided type does not implement public parameterless constructor.", nameof(constructibleType));
            }

            TResultInterface result = (TResultInterface)ctor.Invoke(new object[0]);
            return result;
        }
    }

    public static class StaticInterfaceFactory<TParameter>
    {
        public static TConstructible Create<TConstructible>(TParameter parameter)
            where TConstructible : class, IConstructible<TParameter>, new()
        {
            var result = new TConstructible();
            result.Initialize(parameter);
            return result;
        }

        public static IConstructible<TParameter> Create(Type constructibleType, TParameter parameter)
        {
            var result = StaticInterfaceFactory.CreateFromParamlessCtor<IConstructible<TParameter>>(constructibleType);
            result.Initialize(parameter);
            return result;
        }
    }
}
