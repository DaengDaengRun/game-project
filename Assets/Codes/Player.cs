using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 움직임을 위한 인풋 변수 (상하좌우)
    public Vector2 inputVec;
    public float speed;
    // 물리적 변화
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;  // 기존 캐릭터
    public Sprite sickDogSprite;    // 적과 충돌 시 나타나는 캐릭터

    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;  // 초기 스프라이트 저장
    }

    // 하나의 프레임마다 한번씩 호출되는 생명주기 함수
    void Update(){
        // 모든 입력을 관리하는 클래스
        // GetAxis: 움직임이 부드럽게 보정됨
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate(){
        // 물리 프레임 하나만큼 소비된 시간 - fixedDeltaTime
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
 
        // 3. 위치 이동 (현재 위치 + 위치 이동값)
        rigid.MovePosition(rigid.position + nextVec);
    }

    // void OnTriggerEnter2D(Collider2D other){
    //     if(other.CompareTag("Enemy")){
    //         Debug.Log("충돌 (Trigger)");
    //     }
    // }

     void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (inputVec.x != 0){
            spriteRenderer.flipX = inputVec.x < 0;
        }
    }

    private int collisionCount = 0;
    private int maxCollision = 3;

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            Debug.Log("충돌 (Collision)");
            spriteRenderer.sprite = sickDogSprite;
            collisionCount++;  // 충돌 횟수 증가

            if (collisionCount >= maxCollision){
                Debug.Log("Player가 죽었습니다!");
                Destroy(gameObject);  // 플레이어 오브젝트 제거
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Enemy")){
            spriteRenderer.sprite = originalSprite; 
        }
    }
    
}
