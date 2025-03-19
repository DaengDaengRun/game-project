using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        // currentStage 초기화
        Player.instance.SetCurrentStage(1);  // Stage 1로 초기화

        SceneManager.LoadScene("Start"); // 스테이지 선택 씬 로드
    }
}
