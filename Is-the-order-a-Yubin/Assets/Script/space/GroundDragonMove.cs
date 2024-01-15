using System.Collections;
using UnityEngine;

public class GroundDragonMove : MonoBehaviour
{
    public Transform Player;
    public float speed = 1;
    public double stoppingDistance = 0.3;
    public GameObject stonePrefab;

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
        Instantiate(stonePrefab, transform.position, Quaternion.identity);
    }
}
