namespace StaticInterface
{
    public interface IConstructible
    {
        void Initialize(params object[] parameters);
    }

    public interface IConstructible<TParameter>
    {
        void Initialize(TParameter parameter);
    }
}
