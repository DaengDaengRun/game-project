using UnityEngine;
using UnityEngine.SceneManagement;

public class S3SelectClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("Stage3"); // 스테이지 선택 씬 로드
    }
}
