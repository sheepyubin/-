using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public static BoidSpawner Instance;    // BoidSpawner�� ���� ����

    public GameObject boidPrefab;           // Boid ������
    public int numberOfBoids = 50;          // Boid ��ü�� ��
    public float spawnAreaWidth = 20f;      // ���� ������ ���� ����
    public float spawnAreaHeight = 20f;     // ���� ������ ���� ����
    public List<Boid> boids = new List<Boid>();  // ������ Boid ��ü���� �����ϴ� ����Ʈ

    void Awake()
    {
        Instance = this;    // �ν��Ͻ��� �����Ͽ� �̱��� ������ ����
        // �̱��� ����: � Ŭ������ ���� �� �ϳ��� �ν��Ͻ��� ����
    }

    void Start()
    {
        // ��ü���� ���� �� �ʱ�ȭ
        for (int i = 0; i < numberOfBoids; i++)
        {
            // ������ ��ġ�� ��ü ����
            GameObject boidObject = Instantiate(boidPrefab, RandomSpawnPosition(), Quaternion.identity);

            // Boid ��ũ��Ʈ ������Ʈ ��������
            Boid boid = boidObject.GetComponent<Boid>();

            // Boid ��ü�� �ӵ� ����
            boid.velocity = Random.insideUnitCircle * boid.maxSpeed; // insideUnitCircle�� �������� 1�� ������ ������ �� (-1.0 ~ 1.0)

            // ������ Boid�� ����Ʈ�� �߰�
            boids.Add(boid);
        }
    }

    Vector3 RandomSpawnPosition()
    {
        // ���� ���� ������ ������ ��ġ ��ȯ
        float x = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        float y = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
        return new Vector3(x, y, 0f) + transform.position;
    }

    // ���� ������ ����� ����Ͽ� ǥ��
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth, spawnAreaHeight, 0f));
    }
}
