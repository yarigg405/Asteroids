

public interface IServiceLocator
{
    void Register(IService service);

    void Register<T>(IService service);

    T Get<T>();

}

