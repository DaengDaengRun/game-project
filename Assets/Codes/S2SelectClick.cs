using UnityEngine;
using UnityEngine.SceneManagement;

public class S2SelectClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        // Stage 2을 선택했을 때 currentStage를 2로 설정
        PlayerPrefs.SetInt("CurrentStage", 2);  // currentStage를 PlayerPrefs에 저장
        PlayerPrefs.Save();  // 저장
        SceneManager.LoadScene("Stage2"); // 스테이지 선택 씬 로드
    }
}
