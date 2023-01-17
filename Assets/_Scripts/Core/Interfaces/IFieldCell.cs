using System;
using System.Collections.Generic;

public interface IFieldCell
{
    public void Add<T>(Type t, T item) where T : BaseController;
    public void Remove<T>(Type t, T item) where T : BaseController;
    public HashSet<BaseController> Get<T>() where T : BaseController;
}








