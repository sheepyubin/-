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

    private float elapsedTime = 0.0f;

    private Vector3 originalSize;
    private Color originalColor;

    public GameObject player;
    public GameObject bulletPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

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

        originalSize = new Vector3(0.1f, 0.1f, 1.0f);
        originalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        Invoke("DestroyGameObject", animationDuration + fadeDuration);
    }

    private void Update()
    {
        MoveObject();
        AnimateSize();
        AnimateColor();
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

        if (t >= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    bool hasRandomRotation = false;

    void MoveObject()
    {
        // 한 번 랜덤한 각도로 회전
        if (!hasRandomRotation)
        {
            float randomRotation = Random.Range(-5.0f, 5.0f);
            transform.Rotate(Vector3.forward * randomRotation);
            hasRandomRotation = true;
        }

        // 현재 방향으로 이동
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // 총알 생성
        if (bulletPrefab != null)
        {
            GameObject tempObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Rigidbody2D bulletRigidbody = tempObject.GetComponent<Rigidbody2D>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = transform.right * moveSpeed;
            }
            else
            {
                Debug.LogWarning("Rigidbody2D not found. Make sure the bullet prefab has a Rigidbody2D component.");
            }
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
