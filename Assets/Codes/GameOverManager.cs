using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // 게임 종료 UI 패널

    void Start()
    {
        gameOverPanel.SetActive(false);  // 게임 시작 시 비활성화
    }

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);   // 게임 종료 시 패널 표시
        Time.timeScale = 0f;             // 게임 멈춤
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        SceneManager.LoadScene("MainMenu"); // 메인 메뉴 씬으로 이동 (씬 이름 수정 가능)
    }
}
