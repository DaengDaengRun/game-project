using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("Start"); // 스테이지 선택 씬 로드
    }
}
