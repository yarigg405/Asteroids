using TMPro;
using UnityEngine;


public class PlayerConditionWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coords;
    [SerializeField] TextMeshProUGUI rotation;
    [SerializeField] TextMeshProUGUI speed;

    [SerializeField] TextMeshProUGUI laserCount;
    [SerializeField] TextMeshProUGUI laserTime;
    [SerializeField] TextMeshProUGUI scores;


    public void SetCoords(Vector2 position)
    {
        coords.text = $"coords: {position.x.ToString("0.0")} {position.y.ToString("0.0")}";
    }

    public void SetRotation(float angle)
    {
        rotation.text = $"rotation: {angle.ToString("0.0")}";
    }

    public void SetSpeed(float spd)
    {
        speed.text = $"speed: {spd.ToString("0.0")}";
    }

    public void SetLaserCount(int count)
    {
        laserCount.text = $"laser bolts count: {count}";
    }

    public void SetLaserTime(float time)
    {
        laserTime.text = $"laser recharge time: {time.ToString("0.0")}s";
    }

    public void SetScores(int scoresCount)
    {
        scores.text = scoresCount.ToString();
    }
}
