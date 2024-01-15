using UnityEngine;

public class Boid : MonoBehaviour
{
    // ��ü�� ���� �ӵ�
    public Vector2 velocity;

    // ��ü�� �ִ� �ӵ�
    public float maxSpeed = 3f;

    // �ٸ� ��ü�� �����ϴ� ����
    public float neighborRadius = 2f;

    // �и� ��Ģ �ּ� �Ÿ�
    public float separationDistance = 1f;

    // ���� ��Ģ �ִ� �Ÿ�
    public float alignmentDistance = 2f;

    // ���� ��Ģ �ִ� �Ÿ�
    public float cohesionDistance = 4f;

    void Update()
    {
        // Boid�� �ӵ� ������Ʈ �� �̵�
        UpdateBoidVelocity();
        MoveBoid();
    }

    void UpdateBoidVelocity()
    {
        // �и�, ����, ���� ��Ģ�� �����Ͽ� �ӵ��� ������Ʈ
        Vector2 separation = CalculateSeparation();
        Vector2 alignment = CalculateAlignment();
        Vector2 cohesion = CalculateCohesion();

        velocity += separation + alignment + cohesion;

        // �ִ� �ӵ� ����
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
    }

    void MoveBoid()
    {
        // ���� �ӵ��� �̿��Ͽ� Boid�� �̵�
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    Vector2 CalculateSeparation()
    {
        // �и� ��Ģ
        Vector2 separation = Vector2.zero;
        int count = 0;

        // �ٸ� Boid����� �Ÿ��� ����Ͽ� �и� ���͸� ���
        foreach (Boid otherBoid in BoidSpawner.Instance.boids)
        {
            if (otherBoid != this)
            {
                float distance = Vector2.Distance(transform.position, otherBoid.transform.position);

                // �ּ� �Ÿ����� ������ �и� ���Ϳ� �߰�
                if (distance < separationDistance)
                {
                    Vector2 moveAway = (Vector2)(transform.position - otherBoid.transform.position).normalized / distance;
                    separation += moveAway;
                    count++;
                }
            }
        }

        // �и� ������ ����� ����
        if (count > 0)
        {
            separation /= count;
        }

        return separation;
    }

    Vector2 CalculateAlignment()
    {
        // ���� ��Ģ
        Vector2 averageAlignment = Vector2.zero;
        int count = 0;

        // �ٸ� Boid����� �Ÿ��� ����Ͽ� ���� ���͸� ���
        foreach (Boid otherBoid in BoidSpawner.Instance.boids)
        {
            if (otherBoid != this)
            {
                float distance = Vector2.Distance(transform.position, otherBoid.transform.position);

                // �ִ� �Ÿ����� ������ ���� ���Ϳ� �߰�
                if (distance < alignmentDistance)
                {
                    averageAlignment += otherBoid.velocity;
                    count++;
                }
            }
        }

        // ���� ������ ����� ����
        if (count > 0)
        {
            averageAlignment /= count;
        }

        return averageAlignment;
    }

    Vector2 CalculateCohesion()
    {
        // ���� ��Ģ
        Vector2 centerOfMass = Vector2.zero;
        int count = 0;

        // �ٸ� Boid����� �Ÿ��� ����Ͽ� ���� ���͸� ���
        foreach (Boid otherBoid in BoidSpawner.Instance.boids)
        {
            if (otherBoid != this)
            {
                float distance = Vector2.Distance(transform.position, otherBoid.transform.position);

                // �ִ� �Ÿ����� ������ ���� ���Ϳ� �߰�
                if (distance < cohesionDistance)
                {
                    centerOfMass += (Vector2)otherBoid.transform.position;
                    count++;
                }
            }
        }

        // ���� ������ ����� ����
        if (count > 0)
        {
            centerOfMass /= count;
            Vector2 moveToward = centerOfMass - (Vector2)transform.position;

            // ���� ���͸� �����ϱ� ���� ������ ���� ���� ������ ������ ȸ���� ����
            moveToward = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * moveToward;

            return moveToward.normalized * maxSpeed - velocity;
        }

        return Vector2.zero;
    }
}
