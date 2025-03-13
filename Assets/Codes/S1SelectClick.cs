using UnityEngine;
using UnityEngine.SceneManagement;

public class S1SelectClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("Stage1"); // 스테이지 선택 씬 로드
    }
}

