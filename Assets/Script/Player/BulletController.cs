using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed = 10f; // Tốc độ di chuyển của viên đạn
    private Vector2 direction; // Hướng di chuyển của viên đạn

    // Thời gian viên đạn tồn tại trước khi tự hủy
    public float lifetime = 5f; // 5 giây

    // Hàm khởi tạo để set direction từ PlayerController
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // Đảm bảo hướng được chuẩn hóa
    }

    void Start()
    {
        // Gọi Destroy sau 5 giây
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Di chuyển viên đạn theo hướng đã set
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Hủy đối tượng Enemy
            Destroy(other.gameObject, 0.05f);

            // Hủy hiệu ứng tấn công sau khi va chạm (nếu bạn muốn)
            Destroy(gameObject, 0.05f);
        }
    }
}
