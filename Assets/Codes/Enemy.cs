using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float chaseRange = 5f; // 플레이어를 추적하는 최대 거리
    private Rigidbody2D target;
    private bool isLive = true;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // 🔥 자동으로 Player 찾기
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogError("⚠️ Player를 찾을 수 없습니다! `Player`의 태그를 'Player'로 설정하세요.");
        }
    }

    void FixedUpdate()
    {
        if (!isLive || target == null)
            return;

        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(target.position, rigid.position);

        // 일정 거리 내에 있을 때만 이동
        if (distanceToPlayer <= chaseRange)
        {
            Vector2 dirVec = (target.position - rigid.position).normalized;
            Vector2 nextVec = dirVec * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        }

        rigid.linearVelocity = Vector2.zero; // 🔥 linearVelocity → velocity로 수정
    }

    void LateUpdate()
    {
        if (!isLive || target == null)
            return;

        float distanceToPlayer = Vector2.Distance(target.position, rigid.position);

        // 플레이어가 범위 내에 있을 때만 방향 반전
        if (distanceToPlayer <= chaseRange)
        {
            spriter.flipX = target.position.x < rigid.position.x;
        }
    }
}
