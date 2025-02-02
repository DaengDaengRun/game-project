using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float speed;
    public Rigidbody2D target;
    bool isLive = true; // 몬스터가 살이있는지 죽어있는지 
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    // 몬스터 방향 뒤집기 코드
    void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }
}
