using System;
using System.Collections.Generic;

public class FieldCell : IFieldCell
{
    private Dictionary<Type, HashSet<BaseController>> gameEntities;
    public FieldCell()
    {
        gameEntities = new Dictionary<Type, HashSet<BaseController>>();
    }


    public void Add<T>(Type t, T item) where T : BaseController
    {
        if (!gameEntities.ContainsKey(t))
            gameEntities.Add(t, new HashSet<BaseController>());

        gameEntities[t].Add(item);
    }

    public void Remove<T>(Type t, T item) where T : BaseController
    {
        gameEntities[t].Remove(item);
    }

    public HashSet<BaseController> Get<T>() where T : BaseController
    {
        if (gameEntities.ContainsKey(typeof(T)))
            return gameEntities[typeof(T)];

        return new HashSet<BaseController>();
    }
}








