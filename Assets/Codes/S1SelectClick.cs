using UnityEngine;
using UnityEngine.SceneManagement;

public class S1SelectClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        if (GameStateManager.Instance == null)
        {
            // Debug.LogError("🚨 GameStateManager.Instance가 존재하지 않습니다! GameStateManager가 씬에 있는지 확인하세요.");
            return;
        }

        GameStateManager.Instance.SetCurrentPlayingStage(1); 

        // Stage 1을 선택했을 때 currentStage를 1로 설정
        PlayerPrefs.SetInt("CurrentStage", 1);  // currentStage를 PlayerPrefs에 저장
        PlayerPrefs.Save();  // 저장
        
        SceneManager.LoadScene("Stage1"); // 스테이지 선택 씬 로드
    }
}

