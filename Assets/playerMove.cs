using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool canMove = true;  // Biến bool để kiểm tra có thể di chuyển không
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Chỉ xử lý đầu vào và di chuyển nếu canMove là true
        if (canMove)
        {
            // Lấy input từ người chơi
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Cập nhật các thông số cho Animator
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            // Đặt tốc độ di chuyển về 0 nếu không thể di chuyển
            movement = Vector2.zero;
            animator.SetFloat("Speed", 0);
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật nếu canMove là true
        if (canMove)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}