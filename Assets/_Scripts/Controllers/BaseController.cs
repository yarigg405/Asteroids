using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class BaseController : IUpdate, IDisposable
{
    protected LinksMaster linksMaster;

    protected Transform unityTransform;

    public virtual TransformInfo transformInfo { get; internal set; }
    protected abstract PrefabType prefabType { get; }
    public IFieldCell fieldCell { get; private set; }
    protected virtual int scoresByDestroy { get; }


    public BaseController(LinksMaster _linksMaster)
    {
        if (_linksMaster != null)
        {
            linksMaster = _linksMaster;

            linksMaster.Updater.AddToUpdateList(this);
        }
    }

    public virtual void Dispose()
    {
        linksMaster.LogicDelayer.AddDelay(() =>
        {
            linksMaster.Updater.RemoveFromUpdateList(this);
            if (fieldCell != null)
                fieldCell.Remove(GetConcreteType(), this);
            if (unityTransform != null)
            {
                linksMaster.Despawner.Despawn(prefabType, unityTransform);
                unityTransform = null;
            }
            linksMaster.PlayerScoresContainer.scores += scoresByDestroy;
            linksMaster.PlayerLogger.playerScores =
                    linksMaster.PlayerScoresContainer.scores;
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
        var bounds = linksMaster.MinMaxBounds;

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
        var cell = linksMaster.PositionsHandler.GetCell(transformInfo);
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

