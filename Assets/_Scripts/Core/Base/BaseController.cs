using System;
using UnityEngine;


public abstract class BaseController : IUpdate, IDisposable
{
    protected IServiceLocator serviceLocator;
    protected Transform unityTransform;
    public bool isDisposed { get; private set; } = false;

    public virtual TransformInfo transformInfo { get; set; }
    protected abstract PrefabType prefabType { get; }
    public IFieldCell fieldCell { get; private set; }
    protected virtual int scoresByDestroy { get; }


    public BaseController(IServiceLocator _serviceLocator)
    {
        if (_serviceLocator != null)
        {
            serviceLocator = _serviceLocator;
            serviceLocator.Get<IUpdater>().AddToUpdateList(this);
        }
    }

    public virtual void Dispose()
    {
        isDisposed = true;

        serviceLocator.Get<ILogicDelayer>().AddDelay(() =>
        {
            serviceLocator.Get<IUpdater>().RemoveFromUpdateList(this);
            if (fieldCell != null)
                fieldCell.Remove(GetConcreteType(), this);
            if (unityTransform != null)
            {
                serviceLocator.Get<IDespawner>().Despawn(prefabType, unityTransform);
                unityTransform = null;
            }
            var scores = serviceLocator.Get<PlayerScoresContainer>().scores += scoresByDestroy;
            serviceLocator.Get<PlayerShipConditionLogger>().playerScores = scores;
        });

    }

    public virtual void OnUpdate(float deltatime)
    {
        if (unityTransform != null)
        {
            transformInfo.position += transformInfo.velocity * deltatime;
            unityTransform.position = transformInfo.position;
        }

        CheckBounds();
        UpdateFieldPosition();
        CheckCollisions();
    }

    private void CheckBounds()
    {
        var pos = transformInfo.position;
        var bounds = serviceLocator.Get<MinMaxBounds>();

        if (pos.x > bounds.maxX)
            pos.x = bounds.minX;
        if (pos.x < bounds.minX)
            pos.x = bounds.maxX;
        if (pos.y > bounds.maxY)
            pos.y = bounds.minY;
        if (pos.y < bounds.minY)
            pos.y = bounds.maxY;

        transformInfo.position = pos;
    }

    private void UpdateFieldPosition()
    {
        var cell = serviceLocator.Get<IPositionsHandler>().GetCell(transformInfo);
        if (fieldCell == null)
        {
            fieldCell = cell;
            cell.Add(GetConcreteType(), this);
        }

        else if (fieldCell != cell)
        {
            fieldCell.Remove(GetConcreteType(), this);
            cell.Add(GetConcreteType(), this);
            fieldCell = cell;
        }
    }

    protected virtual void CheckCollisions()
    {

    }

    protected virtual Type GetConcreteType()
    {
        return GetType();
    }
}

