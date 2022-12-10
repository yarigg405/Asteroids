using System;
using System.Collections.Generic;

public class FieldCell : IFieldCell
{
    private Dictionary<Type, List<BaseController>> gameEntities;
    public FieldCell()
    {
        gameEntities = new Dictionary<Type, List<BaseController>>();
    }


    public void Add<T>(Type t, T item) where T : BaseController
    {
        if (!gameEntities.ContainsKey(t))
            gameEntities.Add(t, new List<BaseController>());

        gameEntities[t].Add(item);
    }

    public void Remove<T>(Type t, T item) where T : BaseController
    {
        if (gameEntities.ContainsKey(t))
        {
            gameEntities[t].Remove(item);
        }
    }

    public List<BaseController> Get<T>() where T : BaseController
    {
        if (gameEntities.ContainsKey(typeof(T)))
            return gameEntities[typeof(T)];

        return new List<BaseController>();
    }
}








