using System.Collections;
using UnityEngine;

public class GroundDragonMove : MonoBehaviour
{
    public Transform Player;
    public float speed = 1;
    public double stoppingDistance = 3;

    // 공격에 사용될 돌 프리팹
    public GameObject stonePrefab;
    public Transform sPoint;
    public float timeBetweenShots;

    private float shotTime;
    // 현재 공격 중인지 나타내는 플래그
    private bool isAttacking = false;

    private float attackInterval = 3f;
    void Update()
    {
        if (Player != null)
        {
            // 플레이어 방향으로 회전
            Vector3 directionToPlayer = Player.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // 플레이어와의 거리 계산
            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

            // 정지 거리보다 멀리 있고 공격 중이 아닌 경우에만 이동
            if (distanceToPlayer > stoppingDistance && !isAttacking)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
        else
        {
            Debug.LogWarning("Player reference is null. Assign a player to the GroundDragonMove script in the Inspector.");
        }

        // 3초 간격으로 마우스 왼쪽 버튼이 눌렸을 때 공격 코루틴 실행
        //attackInterval -= Time.deltaTime;
        if (attackInterval <= 0f && Input.GetMouseButtonDown(0))
        {
            if (Time.time >= shotTime)
            {
                isAttacking = true;
                //총알 생성
                Instantiate(stonePrefab, transform.position, Quaternion.identity);
                //재장전 총알 딜레이
                shotTime = Time.time + timeBetweenShots;

            }
            isAttacking = false;
        }
    }
}

