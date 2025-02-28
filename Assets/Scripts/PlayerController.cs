using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask groundlayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool DoubleJump;
    private Animator animator;
    /*   isGrounded: Xác định nhân vật có đang chạm đất không.
        canDoubleJump: Nếu true, cho phép nhân vật nhảy lần hai.
         groundCheck: Một Transform đặt dưới chân nhân vật để kiểm tra va chạm với mặt đất.
          LayerMask groundLayer: Xác định layer của nền đất để tránh kiểm tra nhầm với các vật thể khác.*/
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimesion(); 
    }
    private void HandleMovement()
    {
        // nhân vật di chuyển trái phải
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        // thay đổi hướng mặt của nhân vật theo chiều di chuyển
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
   
    private void HandleJump()
    {
        if (IsGrounded())
        {
            DoubleJump = true;// Reset double jump khi cham dat
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (DoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                DoubleJump = false;// tắt nhảy đôi 
            }

        }

    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);
    }
    private void UpdateAnimesion()
    {
        bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
        bool isJumping = !IsGrounded();
        animator.SetBool("isRuning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }
}
