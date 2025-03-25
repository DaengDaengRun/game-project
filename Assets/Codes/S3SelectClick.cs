using UnityEngine;
using UnityEngine.SceneManagement;

public class S3SelectClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        if (GameStateManager.Instance == null)
        {
            // Debug.LogError("🚨 GameStateManager.Instance가 존재하지 않습니다! GameStateManager가 씬에 있는지 확인하세요.");
            return;
        }

        GameStateManager.Instance.SetCurrentPlayingStage(3); 

        // Stage 3을 선택했을 때 currentStage를 3로 설정
        PlayerPrefs.SetInt("CurrentStage", 3);  // currentStage를 PlayerPrefs에 저장
        PlayerPrefs.Save();  // 저장
        
        SceneManager.LoadScene("Stage3"); // 스테이지 선택 씬 로드
    }
}
