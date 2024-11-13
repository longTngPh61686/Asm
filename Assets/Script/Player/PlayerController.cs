using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public GameObject attackPos; // Tham chiếu đến vị trí tấn công
    public GameObject attackPos2; // Tham chiếu đến vị trí tấn công 2
    public GameObject attackEffectPrefab;  // Prefab AttackEffect
    public GameObject bulletPrefab; // Prefab cho viên đạn
    public Transform bulletSpawnPoint; // Điểm phát ra viên đạn

    private Rigidbody2D rb; // Tham chiếu đến Rigidbody2D
    private Vector2 movement; // Lưu trữ hướng di chuyển
    private Animator animator; // Tham chiếu đến Animator

    private Vector2 lastMovementDirection = Vector2.right; // Hướng di chuyển cuối cùng của nhân vật



    // Start is called before the first frame update
    void Start()
    {
        // Lấy Rigidbody2D của đối tượng
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackPos.SetActive(false); // Đảm bảo AttackPos bắt đầu ở trạng thái không hoạt động
    }

    // Update is called once per frame
    void Update()
    {
        // Nhận đầu vào từ bàn phím
        movement.x = Input.GetAxis("Horizontal"); // Phím trái/phải
        movement.y = Input.GetAxis("Vertical");   // Phím lên/xuống

        // Normalize để đảm bảo tốc độ không vượt quá 1
        movement.Normalize();

        // Gọi hàm flip nếu cần thiết
        if (movement.x > 0)
        {
            Flip(false); // Quay phải
        }
        else if (movement.x < 0)
        {
            Flip(true); // Quay trái
        }

        // Cập nhật animation
        UpdateAnimation();

        // Xử lý các phím nhấn cho swing
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("IsAttack");
            EnableAttackPos();
            StartCoroutine(DisableAttackPosAfterDelay(0.2f)); // Tắt attackPos sau 0.2 giây
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetTrigger("IsAttack2");
            EnableAttackPos();
            StartCoroutine(DisableAttackPosAfterDelay(0.2f)); // Tắt attackPos sau 0.2 giây
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Attack2();
        }
        // Cập nhật hướng di chuyển cuối cùng
        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement;
        }

        // Bắn viên đạn
        if (Input.GetKeyDown(KeyCode.U)) // Giả sử phím Space để bắn
        {
            ShootBullet();
        }
    }

    void FixedUpdate()
    {
        // Sử dụng SetVelocity để di chuyển nhân vật
        rb.linearVelocity = movement * moveSpeed;
    }

    void Flip(bool facingLeft)
    {
        // Thay đổi scale của sprite để flip
        Vector3 theScale = transform.localScale;
        theScale.x = facingLeft ? -1 : 1; // Nếu quay trái, scale x là -1, ngược lại là 1
        transform.localScale = theScale;
    }

    void UpdateAnimation()
    {
        // Cập nhật trạng thái animation
        animator.SetBool("IsRunning", movement != Vector2.zero);
    }

    // Thiết lập trạng thái tấn công
    public void EnableAttackPos()
    {
        attackPos.SetActive(true);
    }

    // Tắt trạng thái tấn công
    public void DisableAttackPos()
    {
        attackPos.SetActive(false);
    }

    // Coroutine để tắt attackPos sau một khoảng thời gian
    private IEnumerator DisableAttackPosAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Chờ khoảng thời gian trước khi tắt
        DisableAttackPos();  // Tắt attackPos
    }

    public void Attack2()
    {
        GameObject attackEffect = Instantiate(attackEffectPrefab, attackPos2.transform.position, Quaternion.identity, transform);
        Debug.Log("Tấn công 2 ");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Va chạm với: " + collision.gameObject.name);
    }
    void ShootBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            BulletController bulletController = bullet.GetComponent<BulletController>();

            if (bulletController != null)
            {
                bulletController.SetDirection(lastMovementDirection); // Gửi hướng di chuyển của nhân vật cho viên đạn
            }
        }
    }
}

