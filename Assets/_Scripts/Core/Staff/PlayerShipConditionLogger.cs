using UnityEngine;

public class PlayerShipConditionLogger : IUpdate, IService
{
    private readonly TransformInfo playerTransformInfo;
    private readonly IPlayerConditionWindow playerConditionWindow;
    private int framesCount;

    public int CurrentLaserCount { set => playerConditionWindow.SetLaserCount(value); }

    public float CurrentLaserRechargeTime { set => playerConditionWindow.SetLaserTime(value); }

    public int PlayerScores { set => playerConditionWindow.SetScores(value); }

    public PlayerShipConditionLogger(IPlayerConditionWindow playerConditionWindow, TransformInfo playerTransformInfo)
    {
        this.playerConditionWindow = playerConditionWindow;
        this.playerTransformInfo = playerTransformInfo;
    }

    public void OnUpdate(float deltaTime)
    {
        framesCount++;
        if (framesCount % 30 == 0)
        {
            playerConditionWindow.SetCoords(playerTransformInfo.Position);
            playerConditionWindow.SetRotation(playerTransformInfo.CurrentRadians * Mathf.Rad2Deg);
            playerConditionWindow.SetSpeed(playerTransformInfo.Velocity.magnitude);
        }
    }
}

