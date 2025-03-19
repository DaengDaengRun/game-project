using UnityEngine;
using UnityEngine.SceneManagement;

public class S3SelectClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        // Stage 3을 선택했을 때 currentStage를 3로 설정
        PlayerPrefs.SetInt("CurrentStage", 3);  // currentStage를 PlayerPrefs에 저장
        PlayerPrefs.Save();  // 저장
        SceneManager.LoadScene("Stage3"); // 스테이지 선택 씬 로드
    }
}
