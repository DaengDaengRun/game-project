using System.Collections;
using UnityEngine;

public class PlayerEnding : MonoBehaviour
{
    public float speed = 1.5f;
    public float xRadius = 3f;
    public float yRadius = 3f;
    public float yOffset = -1f;
    public float xOffset = -1f;

    private float time = 0f;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        time += Time.deltaTime * speed;

        float rawX = Mathf.Cos(time) * xRadius;           // flipX 판단용
        float x = rawX + xOffset;                         // 실제 위치
        float y = Mathf.Abs(Mathf.Sin(time) * yRadius) + yOffset;

        // 방향 전환 판단 (rawX 기준)
        if (rawX >= xRadius - 0.01f)
        {
            spriteRenderer.flipX = true;
        }
        else if (rawX <= -xRadius + 0.01f)
        {
            spriteRenderer.flipX = false;
        }

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
