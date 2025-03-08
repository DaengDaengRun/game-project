using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // 게임 종료 UI 패널
    public GameObject nextStageButton; // 다음 스테이지 버튼 추가

    void Start()
    {
        gameOverPanel.SetActive(true); // 게임 오버 시 패널 활성화
        int currentStage = PlayerPrefs.GetInt("CurrentStage", 1);
        bool isPlayerDead = PlayerPrefs.GetInt("IsPlayerDead", 0) == 1; // PlayerPrefs에서 사망 여부 확인

        if (isPlayerDead || currentStage == 3)
        {
            nextStageButton.SetActive(false);
        }
        else
        {
            nextStageButton.SetActive(true);
        }

        // 사망 여부 초기화 (다시 플레이할 때 필요)
        PlayerPrefs.SetInt("IsPlayerDead", 0);
        PlayerPrefs.Save();
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

    public void GoToNextStage()
    {
        int currentStage = PlayerPrefs.GetInt("CurrentStage", 1);

        if (currentStage < 3) // Stage 1 또는 2이면 진행 가능
        {
            GameManager.instance.NextStage(); // 스테이지 증가
            SceneManager.LoadScene("Stage" + (currentStage + 1)); // 다음 스테이지로 이동
        }
    }
}
