using System;
using UnityEngine;


public abstract class BaseController : IUpdate, IDisposable
{
    protected IServiceLocator Locator;
    protected Transform UnityTransform;
    public bool IsDisposed { get; private set; }

    public virtual TransformInfo TransformInfo { get; set; }
    protected abstract PrefabType PrefabType { get; }
    public IFieldCell FieldCell { get; private set; }
    protected virtual int ScoresByDestroy => 0;


    protected BaseController(IServiceLocator locator)
    {
        if (locator != null)
        {
            Locator = locator;
            Locator.Get<IUpdater>().AddToUpdateList(this);
        }
    }

    public virtual void Dispose()
    {
        IsDisposed = true;

        Locator.Get<ILogicDelayer>().AddDelay(() =>
        {
            Locator.Get<IUpdater>().RemoveFromUpdateList(this);
            if (FieldCell != null)
                FieldCell.Remove(GetConcreteType(), this);
            if (UnityTransform != null)
            {
                Locator.Get<IDespawner>().Despawn(PrefabType, UnityTransform);
                UnityTransform = null;
            }

            var scores = Locator.Get<PlayerScoresContainer>().Scores += ScoresByDestroy;
            Locator.Get<PlayerShipConditionLogger>().PlayerScores = scores;
        });

    }

    public virtual void OnUpdate(float deltaTime)
    {
        if (UnityTransform != null)
        {
            TransformInfo.Position += TransformInfo.Velocity * deltaTime;
            UnityTransform.position = TransformInfo.Position;
        }

        CheckBounds();
        UpdateFieldPosition();
        CheckCollisions();
    }

    private void CheckBounds()
    {
        var pos = TransformInfo.Position;
        var bounds = Locator.Get<MinMaxBounds>();

        if (pos.x > bounds.MaxX)
            pos.x = bounds.MinX;
        if (pos.x < bounds.MinX)
            pos.x = bounds.MaxX;
        if (pos.y > bounds.MaxY)
            pos.y = bounds.MinY;
        if (pos.y < bounds.MinY)
            pos.y = bounds.MaxY;

        TransformInfo.Position = pos;
    }

    private void UpdateFieldPosition()
    {
        var cell = Locator.Get<IPositionsHandler>().GetCell(TransformInfo);
        if (FieldCell == null)
        {
            FieldCell = cell;
            cell.Add(GetConcreteType(), this);
        }

        else if (FieldCell != cell)
        {
            FieldCell.Remove(GetConcreteType(), this);
            cell.Add(GetConcreteType(), this);
            FieldCell = cell;
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

