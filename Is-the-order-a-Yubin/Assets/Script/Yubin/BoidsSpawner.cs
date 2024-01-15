using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public static BoidSpawner Instance;    // BoidSpawner의 정적 변수

    public GameObject boidPrefab;           // Boid 프리팹
    public int numberOfBoids = 50;          // Boid 개체의 수
    public float spawnAreaWidth = 20f;      // 생성 영역의 가로 길이
    public float spawnAreaHeight = 20f;     // 생성 영역의 세로 길이
    public List<Boid> boids = new List<Boid>();  // 생성된 Boid 개체들을 저장하는 리스트

    void Awake()
    {
        Instance = this;    // 인스턴스를 설정하여 싱글톤 패턴을 적용
        // 싱글톤 패턴: 어떤 클래스에 대해 단 하나의 인스턴스만 존재
    }

    void Start()
    {
        // 개체들을 생성 및 초기화
        for (int i = 0; i < numberOfBoids; i++)
        {
            // 랜덤한 위치에 개체 생성
            GameObject boidObject = Instantiate(boidPrefab, RandomSpawnPosition(), Quaternion.identity);

            // Boid 스크립트 컴포넌트 가져오기
            Boid boid = boidObject.GetComponent<Boid>();

            // Boid 개체의 속도 설정
            boid.velocity = Random.insideUnitCircle * boid.maxSpeed; // insideUnitCircle는 반지름이 1인 원에서 무작위 값 (-1.0 ~ 1.0)

            // 생성된 Boid를 리스트에 추가
            boids.Add(boid);
        }
    }

    Vector3 RandomSpawnPosition()
    {
        // 생성 영역 내에서 랜덤한 위치 반환
        float x = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        float y = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
        return new Vector3(x, y, 0f) + transform.position;
    }

    // 생성 영억을 기즈모를 사용하여 표현
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth, spawnAreaHeight, 0f));
    }
}
