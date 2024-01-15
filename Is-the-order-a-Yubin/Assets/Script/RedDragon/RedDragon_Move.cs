using System.Collections;
using UnityEngine;

public class RedDragon_Move : MonoBehaviour
{
    public Transform Player;
    public float speed = 1;
    public double stoppingDistance = 0.3;
    public GameObject flamePrefab;

    private bool isAttacking = false;

    void Update()
    {
        if (Player != null)
        {
            Vector3 directionToPlayer = Player.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

            if (distanceToPlayer > stoppingDistance && !isAttacking)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
        else
        {
            Debug.LogWarning("Player reference is null. Assign a player to the RedDragon_Move script in the Inspector.");
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackMultipleTimes(50, 0.03f));
        }
    }

    IEnumerator AttackMultipleTimes(int count, float interval)
    {
        isAttacking = true;

        for (int i = 0; i < count; i++)
        {
            Attack();
            yield return new WaitForSeconds(interval);
        }

        isAttacking = false;
    }

    void Attack()
    {
        // 플레이어 방향으로 0.05만큼 이동
        float attackMoveDistance = 0.05f;
        transform.Translate(Vector3.right * attackMoveDistance);

        // flame 프리팹 생성
        Instantiate(flamePrefab, transform.position, transform.rotation);
    }

}
