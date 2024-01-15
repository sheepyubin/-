using UnityEngine;

public class Boid : MonoBehaviour
{
    // 개체의 현재 속도
    public Vector2 velocity;

    // 개체의 최대 속도
    public float maxSpeed = 3f;

    // 다른 개체를 감지하는 범위
    public float neighborRadius = 2f;

    // 분리 규칙 최소 거리
    public float separationDistance = 1f;

    // 정렬 규칙 최대 거리
    public float alignmentDistance = 2f;

    // 결합 규칙 최대 거리
    public float cohesionDistance = 4f;

    void Update()
    {
        // Boid의 속도 업데이트 및 이동
        UpdateBoidVelocity();
        MoveBoid();
    }

    void UpdateBoidVelocity()
    {
        // 분리, 정렬, 결합 규칙을 적용하여 속도를 업데이트
        Vector2 separation = CalculateSeparation();
        Vector2 alignment = CalculateAlignment();
        Vector2 cohesion = CalculateCohesion();

        velocity += separation + alignment + cohesion;

        // 최대 속도 제한
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
    }

    void MoveBoid()
    {
        // 현재 속도를 이용하여 Boid를 이동
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    Vector2 CalculateSeparation()
    {
        // 분리 규칙
        Vector2 separation = Vector2.zero;
        int count = 0;

        // 다른 Boid들과의 거리를 계산하여 분리 벡터를 계산
        foreach (Boid otherBoid in BoidSpawner.Instance.boids)
        {
            if (otherBoid != this)
            {
                float distance = Vector2.Distance(transform.position, otherBoid.transform.position);

                // 최소 거리보다 가까우면 분리 벡터에 추가
                if (distance < separationDistance)
                {
                    Vector2 moveAway = (Vector2)(transform.position - otherBoid.transform.position).normalized / distance;
                    separation += moveAway;
                    count++;
                }
            }
        }

        // 분리 벡터의 평균을 구함
        if (count > 0)
        {
            separation /= count;
        }

        return separation;
    }

    Vector2 CalculateAlignment()
    {
        // 정렬 규칙
        Vector2 averageAlignment = Vector2.zero;
        int count = 0;

        // 다른 Boid들과의 거리를 계산하여 정렬 벡터를 계산
        foreach (Boid otherBoid in BoidSpawner.Instance.boids)
        {
            if (otherBoid != this)
            {
                float distance = Vector2.Distance(transform.position, otherBoid.transform.position);

                // 최대 거리보다 가까우면 정렬 벡터에 추가
                if (distance < alignmentDistance)
                {
                    averageAlignment += otherBoid.velocity;
                    count++;
                }
            }
        }

        // 정렬 벡터의 평균을 구함
        if (count > 0)
        {
            averageAlignment /= count;
        }

        return averageAlignment;
    }

    Vector2 CalculateCohesion()
    {
        // 결합 규칙
        Vector2 centerOfMass = Vector2.zero;
        int count = 0;

        // 다른 Boid들과의 거리를 계산하여 결합 벡터를 계산
        foreach (Boid otherBoid in BoidSpawner.Instance.boids)
        {
            if (otherBoid != this)
            {
                float distance = Vector2.Distance(transform.position, otherBoid.transform.position);

                // 최대 거리보다 가까우면 결합 벡터에 추가
                if (distance < cohesionDistance)
                {
                    centerOfMass += (Vector2)otherBoid.transform.position;
                    count++;
                }
            }
        }

        // 결합 벡터의 평균을 구함
        if (count > 0)
        {
            centerOfMass /= count;
            Vector2 moveToward = centerOfMass - (Vector2)transform.position;

            // 결합 벡터를 적용하기 전에 일정한 각도 범위 내에서 무작위 회전을 더함
            moveToward = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * moveToward;

            return moveToward.normalized * maxSpeed - velocity;
        }

        return Vector2.zero;
    }
}
