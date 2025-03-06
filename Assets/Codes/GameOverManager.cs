using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // 게임 종료 UI 패널

    void Start()
    {
        gameOverPanel.SetActive(true); // 게임 오버 시 패널 활성화
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        int previousSceneIndex = PlayerPrefs.GetInt("LastScene", 0); // 저장된 씬 가져오기
        SceneManager.LoadScene(previousSceneIndex); // 현재 씬 다시 로드
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        SceneManager.LoadScene("Start");
    }
}
