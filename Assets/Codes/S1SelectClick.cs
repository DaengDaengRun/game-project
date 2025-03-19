using UnityEngine;
using UnityEngine.SceneManagement;

public class S1SelectClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        // Stage 1을 선택했을 때 currentStage를 1로 설정
        PlayerPrefs.SetInt("CurrentStage", 1);  // currentStage를 PlayerPrefs에 저장
        PlayerPrefs.Save();  // 저장
        SceneManager.LoadScene("Stage1"); // 스테이지 선택 씬 로드
    }
}

