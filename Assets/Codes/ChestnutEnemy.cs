using UnityEngine;

public class ChestnutEnemy : MonoBehaviour
{
    public float speed = 2f; // 이동 속도
    public float changeDirectionInterval = 2f; // 방향 변경 간격
    private Vector2 moveDirection; // 현재 이동 방향
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection(); // 처음 시작 시 방향 설정
        InvokeRepeating(nameof(ChangeDirection), changeDirectionInterval, changeDirectionInterval); // 일정 시간마다 방향 변경
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed; // 현재 방향으로 이동
    }

    void ChangeDirection()
    {
        // 새로운 랜덤 방향 설정 (360도)
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 벽이나 장애물과 부딪히면 반대 방향으로 변경
        moveDirection = -moveDirection;
    }
}
