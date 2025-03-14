using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public GameObject successPanel;  // 게임 종료 UI 패널

    void Start()
    {
        successPanel.SetActive(true); // 게임 오버 시 패널 활성화

        // 사망 여부 초기화 (다시 플레이할 때 필요)
        PlayerPrefs.SetInt("IsPlayerDead", 0);
        PlayerPrefs.Save();
    }

    public void GoToNextStage()
    {
        int currentStage = PlayerPrefs.GetInt("CurrentStage", 1);

        if (currentStage < 3) // Stage 1 또는 2이면 진행 가능
        {
            int nextStage = currentStage + 1;
            PlayerPrefs.SetInt("CurrentStage", nextStage); // 다음 스테이지 저장
            PlayerPrefs.Save();

            Debug.Log("다음 스테이지: Stage" + nextStage);
            SceneManager.LoadScene("Stage" + nextStage); // 다음 스테이지로 이동
        }
    }
}
