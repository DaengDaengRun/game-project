using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SuccessManager : MonoBehaviour
{
    public TMP_Text stageTimeText;  // 현재 스테이지 시간 표시용
    public Button goFasterButton;     // Go Faster 버튼
    public Button nextStageButton;    // Next Stage 버튼
    public TMP_Text buttonText;       // 버튼 텍스트

    private void Start()
    {
        int currentStage = PlayerPrefs.GetInt("CurrentStage", 1);  // Default to Stage 1 if not set

        float stageTime = 0f;

        // 현재 스테이지의 시간만 불러와서 표시
        switch (currentStage)
        {
            case 1:
                stageTime = PlayerPrefs.GetFloat("Stage1Time", 0f);
                break;
            case 2:
                stageTime = PlayerPrefs.GetFloat("Stage2Time", 0f);
                break;
            case 3:
                stageTime = PlayerPrefs.GetFloat("Stage3Time", 0f);
                break;
        }

        // 화면에 현재 스테이지 시간 표시
        stageTimeText.text = "Stage " + currentStage + " Time: " + stageTime.ToString("F1") + "s";

        int stageNum = GameStateManager.Instance.CurrentPlayingStage;

        string message = GetStageClearMessage(stageNum);
        buttonText.text = message;

        // Debug.Log("성공 화면 버튼 문구 설정: " + message);

        // 버튼 클릭 이벤트 설정
        goFasterButton.onClick.AddListener(GoFaster);
        nextStageButton.onClick.AddListener(NextStage);
    }

    // Go Faster 버튼 클릭 시
    public void GoFaster()
    {
        int lastSceneIndex = PlayerPrefs.GetInt("LastScene", 0); // 직전 씬 인덱스 저장
        SceneManager.LoadScene(lastSceneIndex);
    }

    private string GetStageClearMessage(int stageNum)
    {
        switch (stageNum)
        {
            case 1:
                return "Next Stage";
            case 2:
                return "Next Stage";
            case 3:
                return "Ending";
            default:
                return "Next Stage";
        }
    } 

    // Next Stage 버튼 클릭 시
    public void NextStage()
    {
        int currentStage = PlayerPrefs.GetInt("CurrentStage", 1);
        // int currentStage = 3; // 테스트용
        int nextStage = currentStage + 1;

        // 마지막 스테이지(3)까지 완료했으면 Ending으로
        if (currentStage == 3)
        {
            // Debug.Log("모든 스테이지 클리어! Ending 씬으로 이동");
            SceneManager.LoadScene("Ending");
            return;
        }
        else 
        {
            // Debug.Log("➡️ 다음 스테이지로 이동: " + nextStage);
            SceneManager.LoadScene("Choose");
        }
    }
}