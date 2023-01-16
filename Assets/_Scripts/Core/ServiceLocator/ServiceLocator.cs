using System;
using System.Collections.Generic;


public class ServiceLocator : IServiceLocator
{
    protected Dictionary<Type, IService> _services;

    public ServiceLocator()
    {
        _services = new Dictionary<Type, IService>();
    }


    public void Register(IService service)
    {
        var type = service.GetType();
        if (_services.ContainsKey(type))
        {
            throw new Exception($"Object with type {type} already added");
        }

        _services[type] = service;
    }

    public void Register<T>(IService service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            throw new Exception($"Object with type {type} already added");
        }

        _services[type] = service;
    }


    public T Get<T>()
    {
        var type = typeof(T);

        if (_services.ContainsKey(type))
            return (T)_services[type];

        throw new Exception($"Service with type {type} not found");
    }


}

