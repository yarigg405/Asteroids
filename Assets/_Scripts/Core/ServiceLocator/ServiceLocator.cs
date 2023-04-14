using System;
using System.Collections.Generic;


public class ServiceLocator : IServiceLocator
{
    protected Dictionary<Type, IService> Services;

    public ServiceLocator()
    {
        Services = new Dictionary<Type, IService>();
    }


    public void Register(IService service)
    {
        var type = service.GetType();
        if (Services.ContainsKey(type))
        {
            throw new Exception($"Object with type {type} already added");
        }

        Services[type] = service;
    }

    public void Register<T>(IService service)
    {
        var type = typeof(T);
        if (Services.ContainsKey(type))
        {
            throw new Exception($"Object with type {type} already added");
        }

        Services[type] = service;
    }


    public T Get<T>()
    {
        var type = typeof(T);

        if (Services.ContainsKey(type))
            return (T)Services[type];

        throw new Exception($"Service with type {type} not found");
    }


}

