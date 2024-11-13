using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Tốc độ di chuyển của kẻ địch
    public float changeDirectionTime = 1f; // Thời gian để thay đổi hướng
    private float timer; // Thời gian đếm ngược
    private Vector2 moveDirection; // Hướng di chuyển của kẻ địch

    private Rigidbody2D rb;

    // Giới hạn di chuyển
    private float minX = -5f;
    private float maxX = 5f;
    private float minY = -5f;
    private float maxY = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection();
        timer = changeDirectionTime; // Khởi tạo timer
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChangeDirection();
            timer = changeDirectionTime; // Đặt lại timer
        }
    }

    void FixedUpdate()
    {
        // Di chuyển kẻ địch theo hướng di chuyển hiện tại
        rb.linearVelocity = moveDirection * moveSpeed;

        // Kiểm tra nếu vị trí vượt quá giới hạn thì đổi hướng
        CheckBoundsAndChangeDirection();
    }

    private void ChangeDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;

        // Lật hướng kẻ địch theo trục X
        if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Lật kẻ địch sang trái
        }
        else if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Lật kẻ địch sang phải
        }
    }

    private void CheckBoundsAndChangeDirection()
    {
        Vector3 pos = transform.position;

        // Kiểm tra nếu enemy vượt ra ngoài giới hạn và thay đổi hướng
        if (pos.x < minX || pos.x > maxX)
        {
            moveDirection.x = -moveDirection.x; // Đảo ngược hướng X
            transform.position = new Vector3(Mathf.Clamp(pos.x, minX, maxX), pos.y, pos.z); // Giữ enemy trong giới hạn
        }

        if (pos.y < minY || pos.y > maxY)
        {
            moveDirection.y = -moveDirection.y; // Đảo ngược hướng Y
            transform.position = new Vector3(pos.x, Mathf.Clamp(pos.y, minY, maxY), pos.z); // Giữ enemy trong giới hạn
        }
    }
}
