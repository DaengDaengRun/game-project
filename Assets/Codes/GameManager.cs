using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // 싱글톤 인스턴스

    public Player player;  // 플레이어 정보 저장 (필요한 경우)
    

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지되도록
    }
}
