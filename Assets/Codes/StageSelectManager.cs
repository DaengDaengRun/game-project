using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요

public class StageSelectManager : MonoBehaviour
{
    public void SceneChange ()
    {
    SceneManager.LoadScene("Scene0"); // GameScene으로 전환
    }
}
