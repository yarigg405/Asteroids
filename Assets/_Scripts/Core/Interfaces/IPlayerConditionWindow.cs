using UnityEngine;

public interface IPlayerConditionWindow
{
    public void SetCoords(Vector2 position);
    public void SetRotation(float angle);
    public void SetSpeed(float spd);
    public void SetLaserCount(int count);
    public void SetLaserTime(float time);
    public void SetScores(int scoresCount);
}
