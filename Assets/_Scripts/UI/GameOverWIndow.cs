using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverWindow : MonoBehaviour, IGameOverWindow, IService
{
    [SerializeField] TextMeshProUGUI scoresText;

    public void Show(int scoresCount)
    {
        scoresText.text = scoresCount.ToString();
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClickOnRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
