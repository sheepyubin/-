using UnityEngine;

public class RedDragon_Attack : MonoBehaviour
{
    public Vector3 startSize = new Vector3(0.1f, 0.1f, 1.0f);
    public Vector3 targetSize = new Vector3(1.0f, 1.0f, 1.0f);

    public Color startColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    public Color targetColor = new Color(1.0f, 0.0f, 0.0f, 0.0f);
    public Color finalColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    public float animationDuration = 2.0f;
    public float fadeDuration = 2.0f;
    public float moveSpeed = 4.0f;
    public float rotationSpeed = 6.0f;

    private float elapsedTime = 0.0f;

    private Vector3 originalSize;
    private Color originalColor;

    public GameObject player;

    void Start()
    {
        // Player 오브젝트 방향에서 -5도와 5도 사이의 랜덤한 각도 설정
        float randomAngle = Random.Range(-5.0f, 5.0f);
        transform.rotation = Quaternion.Euler(0, 0, randomAngle);

        originalSize = new Vector3(0.1f, 0.1f, 1.0f);
        originalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        player = GameObject.FindGameObjectWithTag("Player");

        Invoke("DestroyGameObject", animationDuration + fadeDuration);
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the player has the 'Player' tag.");
        }

        AnimateSize();
        AnimateColor();
        RotateObject();
        MoveObject();
    }

    void DestroyGameObject()
    {
        SetObjectSize(targetSize);
        SetObjectColor(finalColor);
        Destroy(gameObject);
    }

    void AnimateSize()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / animationDuration);
        Vector3 newSize = Vector3.Lerp(originalSize, targetSize, t);

        transform.localScale = newSize;

        if (t >= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    void AnimateColor()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / fadeDuration);
        Color newColor = Color.Lerp(originalColor, targetColor, t);

        SetObjectColor(newColor);

        // 투명도가 0이 되면 파괴
        if (t >= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    void MoveObject()
    {
        // 진행 방향으로 이동
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void RotateObject()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the player has the 'Player' tag.");
        }
    }

    void SetObjectSize(Vector3 newSize)
    {
        transform.localScale = newSize;
    }

    void SetObjectColor(Color newColor)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material.color = newColor;
        }
        else
        {
            Debug.LogWarning("Renderer not found. Make sure the object has a Renderer component.");
        }
    }
}
