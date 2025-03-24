using System.Collections;
using UnityEngine;

public class PlayerEnding : MonoBehaviour
{
    public float speed = 3f;    // 이동 속도
    public float xRadius = 5f;  // x축 반지름
    public float yRadius = 3f;  // y축 반지름
    public float yOffset = -2f; // 화면 아래로 이동하는 오프셋 값

    private float time = 0f;    // 경과 시간
    private Animator anim;      // 애니메이터
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        time += Time.deltaTime * speed; // 시간에 따라 속도를 증가시킴

        // x는 -xRadius에서 +xRadius 사이에서 왕복, y는 0에서 +yRadius까지만
        float x = Mathf.Cos(time) * xRadius; // x축 (Cos 함수 사용)
        float y = Mathf.Abs(Mathf.Sin(time) * yRadius) + yOffset; // y 오프셋 추가

        // **x값이 xRadius나 -xRadius에 가까워지면 반전**
        if (x >= xRadius - 0.01f) // 타원의 오른쪽 끝 근처
        {
            spriteRenderer.flipX = true;  // 왼쪽을 보고
        }
        else if (x <= -xRadius + 0.01f) // 타원의 왼쪽 끝 근처
        {
            spriteRenderer.flipX = false; // 오른쪽을 보고
        }

        // 최종 위치 업데이트 (yOffset 적용)
        transform.position = new Vector3(x, y, transform.position.z); // z 값은 그대로 두어야 2D 공간에서만 움직임
    }
}
