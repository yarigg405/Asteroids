using UnityEngine;

public class PlayerShipConditionLogger : IUpdate
{
    private TransformInfo playerTransform;
    private IPlayerConditionWindow window;
    private int framesCount = 0;

    public int currentLaserCount { set => window.SetLaserCount(value); }

    public float currentLaserRechargeTime { set => window.SetLaserTime(value); }

    public int playerScores { set => window.SetScores(value); }

    public PlayerShipConditionLogger(IPlayerConditionWindow _window, TransformInfo _playerTransform)
    {
        window = _window;
        playerTransform = _playerTransform;
    }

    public void OnUpdate(float deltaTime)
    {
        framesCount++;
        if (framesCount % 30 == 0)
        {
            window.SetCoords(playerTransform.position);
            window.SetRotation(playerTransform.currentRadians * Mathf.Rad2Deg);
            window.SetSpeed(playerTransform.velocity.magnitude);
        }
    }
}

