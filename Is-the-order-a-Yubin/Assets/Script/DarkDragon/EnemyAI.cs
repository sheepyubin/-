using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    float attackDelay;

    Enemy enemy;
    Animator enemyAnimator;
    void Start()
    {
        enemy = GetComponent<Enemy>();
    //    //enemyAnimator = enemy.//enemyAnimator;
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        // 타겟과 자신의 거리를 확인
        float distance = Vector3.Distance(transform.position, target.position);

        // 공격 딜레이(쿨타임)가 0일 때, 시야 범위안에 들어올 때
        if (attackDelay == 0 && distance <= enemy.fieldOfVision)
        {
            FaceTarget(); // 타겟 바라보기

            // 공격 범위안에 들어올 때 공격
            if (distance <= enemy.atkRange)
            {
                AttackTarget();
            }
            else // 공격 애니메이션 실행 중이 아닐 때 추적
            {
                    MoveToTarget();
            }
        }
        else // 시야 범위 밖에 있을 때 Idle 애니메이션으로 전환
        {
            //enemyAnimator.SetBool("moving", false);
        }
    }

    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        float dird = target.position.y - transform.position.y;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector3(dir, dird) * enemy.moveSpeed * Time.deltaTime);
        //enemyAnimator.SetBool("moving", true);
    }

    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void AttackTarget()
    {
        target.GetComponent<MoveScript>().nowHp -= enemy.atkDmg;
        //enemyAnimator.SetTrigger("attack"); // 공격 애니메이션 실행
        attackDelay = enemy.atkSpeed; // 딜레이 충전
    }
}
