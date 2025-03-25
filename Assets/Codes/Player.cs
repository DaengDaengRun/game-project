using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance; // 싱글턴 패턴 적용
    public bool isDead = false; // 캐릭터 사망 여부

    // 움직임을 위한 인풋 변수 (상하좌우)
    public Vector2 inputVec;
    public float speed;
    // 물리적 변화
    Rigidbody2D rigid;
    private Animator anim;
    private bool isSick = false;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;  // 기존 캐릭터
    public Sprite sickDogSprite;    // 적과 충돌 시 나타나는 캐릭터
    public Sprite happyDogSprite;    // 집 찾으면 나타나는 캐릭터
    public Sprite findDogSprite;    // 뼈다귀 찾으면 나타나는 캐릭터
    public GameObject GetBoneWarning;
    public float warningDisplayTime = 0.1f;
    private bool isSuccess = false;
    public float timeTaken = 0f;
    public int currentStage = 1;

    void Awake(){
        if (instance == null)
        {
            instance = this;
        }        
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;  // 초기 스프라이트 저장
        anim = GetComponent<Animator>();

        // 🎯 기본 속도 설정 (인스펙터에서 조절 가능)
        if (speed <= 0) speed = 5f;
    }

    // 하나의 프레임마다 한번씩 호출되는 생명주기 함수
    void Update(){
        // 모든 입력을 관리하는 클래스
        // GetAxis: 움직임이 부드럽게 보정됨
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (!isSuccess)
        {
            timeTaken += Time.deltaTime;
        }

       Debug.Log("현재 speed 값: " + speed); // 🎯 speed 값 확인

        if (!isSick) 
        {
            // 모든 방향의 이동량을 포함하는 speed 값 설정
            float move = inputVec.magnitude;
            anim.SetFloat("speed", move); // 이동량을 애니메이터에 전달

        if (move != 0)
        {
            transform.Translate(inputVec.normalized * Time.deltaTime * speed);
        }
    }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(SickAnimation());
        }
    }

    private IEnumerator SickAnimation()
    {
        isSick = true;
        anim.SetTrigger("isSick");

        yield return new WaitForSeconds(0.5f); // 0.5초 후 복귀

        isSick = false;
        anim.ResetTrigger("isSick");
    }

    void FixedUpdate(){
        // 물리 프레임 하나만큼 소비된 시간 - fixedDeltaTime
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
 
        // 3. 위치 이동 (현재 위치 + 위치 이동값)
        rigid.MovePosition(rigid.position + nextVec);
    }

     void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        anim.SetFloat("speed", inputVec.magnitude);
        if (inputVec.x != 0){
            spriteRenderer.flipX = inputVec.x < 0;
        }
    }

    private int collisionCount = 0;
    private int maxCollision = 3;
    private bool isFind = false; // 뼈다귀를 찾았는지 여부
    public bool isHome = false; // 집에 도착했는지 여부

void OnCollisionEnter2D(Collision2D collision){
    int currentStage = PlayerPrefs.GetInt("CurrentStage", 1);
    if (collision.gameObject.CompareTag("Enemy")){
        // Debug.Log("⚠️ Enemy와 충돌! 상태: Sick");
        spriteRenderer.sprite = sickDogSprite;
        collisionCount++;  // 충돌 횟수 증가

        // 🎯 HP 감소 UI 업데이트 호출
        HPManager hpManager = HPManager.GetInstance();
        if (hpManager != null)
        {
            hpManager.DecreaseHP(); // HP 감소
        }

        // 🎵 강아지 짖는 소리 재생
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX("DogBark");
            Debug.Log("🐶 강아지가 짖었습니다!");
        }
        else
        {
            Debug.LogError("🚨 AudioManager 인스턴스를 찾을 수 없습니다!");
        }


        if (collisionCount >= maxCollision){
            isDead = true;
            PlayerPrefs.SetInt("IsPlayerDead", 1); // 사망 여부 저장 (1 = 죽음)
            Debug.Log("💀 Player가 죽었습니다! GameOver 씬으로 이동");

        // 🎵 게임 오버 사운드 재생
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX("GameOver");
            Debug.Log("🔊 게임 오버 사운드 재생!");
        }
        else
        {
            Debug.LogError("🚨 AudioManager 인스턴스를 찾을 수 없습니다!");
        }
            // 현재 씬의 Build Index 저장
            PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameOver");
            return;
        }
    }
    else if (collision.gameObject.CompareTag("Bone")){
        // Debug.Log("🍖 Player가 뼈다귀를 찾았습니다!");
        isFind = true;
        anim.SetBool("isFind", true); // FindDog 애니메이션 실행

        // 🎵 뼈다귀 획득 사운드 재생
        if (AudioManager.instance != null)
        {
        AudioManager.instance.PlaySFX("BonePickUp");
        Debug.Log("🔊 뼈다귀 획득 소리 재생!");
        }

    else
    {
        Debug.LogError("🚨 AudioManager 인스턴스를 찾을 수 없습니다!");
    }
        // 뼈다귀 오브젝트 제거
        Destroy(collision.gameObject);
    }
    else if (collision.gameObject.CompareTag("Home")){
        if (isFind){
            spriteRenderer.sprite = happyDogSprite;
            isHome = true;
            isSuccess = true;

            // 🎵 성공 사운드 재생
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX("SuccessSound");
                Debug.Log("🔊 성공 사운드 재생!");
            }
            else
            {
                Debug.LogError("🚨 AudioManager 인스턴스를 찾을 수 없습니다!");
            }


            // 각 스테이지의 성공 시간을 저장
            SaveStageTime();

            // 성공 시간이 기록되었는지 확인
            // Debug.Log("성공 시간 " + currentStage + ":" + timeTaken);

            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.SetLastClearedStage(); // 마지막으로 클리어한 스테이지 저장
                // Debug.Log("🏆 GameStateManager: 마지막으로 클리어한 스테이지 업데이트");
            }
            else
            {
                // Debug.LogError("🚨 GameStateManager.Instance가 존재하지 않습니다!");
            }

            // 성공 씬으로 이동
            SceneManager.LoadScene("Success");
        }
        else {
            // Debug.Log("🍖❌ 뼈다귀를 찾아오세요!");
            ShowWarningMessage();
        }
    }

    // 현재 스테이지의 시간을 PlayerPrefs에 저장
    void SaveStageTime()
    {
        int currentStage = PlayerPrefs.GetInt("CurrentStage", 1);
        switch (currentStage)
        {
            case 1:
                PlayerPrefs.SetFloat("Stage1Time", timeTaken);
                // Debug.Log("Stage 1 Time Saved: " + timeTaken);
                break;
            case 2:
                PlayerPrefs.SetFloat("Stage2Time", timeTaken);
                // Debug.Log("Stage 2 Time Saved: " + timeTaken);
                break;
            case 3:
                PlayerPrefs.SetFloat("Stage3Time", timeTaken);
                // Debug.Log("Stage 3 Time Saved: " + timeTaken);
                break;
        }
        PlayerPrefs.Save();
    }

    void ShowWarningMessage()
    {
        // Debug.Log("⚠️ ShowWarningMessage 호출됨");
        GetBoneWarning.SetActive(true);    // 패널 보여주기
        StartCoroutine(HideWarningAfterTime());
    }

    IEnumerator HideWarningAfterTime()
    {
        yield return new WaitForSeconds(warningDisplayTime);
        // Debug.Log("HideWarningMessage 호출됨 (Coroutine)");
        GetBoneWarning.SetActive(false);
    }
}

    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Enemy")){
            // 뼈다귀를 찾은 상태라면 충돌 후 다시 findDogSprite로 변경
            if (isFind) {
                // Debug.Log("🍖 적과 충돌 후 뼈다귀 상태로 복귀");
                anim.SetBool("isFind", true);
            }
            else{
                anim.SetBool("isFind", false); 
            }
        }
    }
    // 스테이지 전환 시 currentStage 값을 업데이트
    public void SetCurrentStage(int stage)
    {
        currentStage = stage;
        PlayerPrefs.SetInt("CurrentStage", stage);  // currentStage 값을 PlayerPrefs에 저장
        PlayerPrefs.Save();
    }
}