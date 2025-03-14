using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageClick : MonoBehaviour
{
    public void LoadNextStageSelectScene()
    {
        Debug.Log("다음 스테이지 선택");
        SceneManager.LoadScene("Stage2");   
    }
}
