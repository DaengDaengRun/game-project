using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SuccessManager : MonoBehaviour
{
    public TMP_Text stageTimeText;  // 현재 스테이지 시간 표시용
    public Button goFasterButton;     // Go Faster 버튼
    public Button nextStageButton;    // Next Stage 버튼

    private void Start()
    {
        // // 성공 시간 텍스트 설정
        // float successTime = PlayerPrefs.GetFloat("SuccessTime", 0f);
        // successTimeText.text = "Success Time: " + successTime.ToString("F1") + "s";

        // 현재 스테이지에 맞는 성공 시간 불러오기
        // int currentStage = Player.instance.currentStage;
        // PlayerPrefs에서 currentStage 값을 불러오기
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

    // Next Stage 버튼 클릭 시
    public void NextStage()
    {
    }
}
