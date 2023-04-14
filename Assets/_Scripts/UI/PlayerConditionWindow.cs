using TMPro;
using UnityEngine;


public class PlayerConditionWindow : MonoBehaviour, IPlayerConditionWindow
{
    [SerializeField] TextMeshProUGUI coords;
    [SerializeField] TextMeshProUGUI rotation;
    [SerializeField] TextMeshProUGUI speed;

    [SerializeField] TextMeshProUGUI laserCount;
    [SerializeField] TextMeshProUGUI laserTime;
    [SerializeField] TextMeshProUGUI scores;


    public void SetCoords(Vector2 position)
    {
        coords.text = $"coords: {position.x:0.0} {position.y:0.0}";
    }

    public void SetRotation(float angle)
    {
        rotation.text = $"rotation: {angle:0.0}";
    }

    public void SetSpeed(float spd)
    {
        speed.text = $"speed: {spd:0.0}";
    }

    public void SetLaserCount(int count)
    {
        laserCount.text = $"laser bolts count: {count}";
    }

    public void SetLaserTime(float time)
    {
        laserTime.text = $"laser recharge time: {time:0.0}s";
    }

    public void SetScores(int scoresCount)
    {
        scores.text = scoresCount.ToString();
    }
}
