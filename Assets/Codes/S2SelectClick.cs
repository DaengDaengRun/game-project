using UnityEngine;
using UnityEngine.SceneManagement;

public class S2SelectClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("Stage2"); // 스테이지 선택 씬 로드
    }
}
