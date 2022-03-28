using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("MOVEMENT")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpRememberTime;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float maxYSpeed;

    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TextMesh numText;

    public int num, jumps;
    public bool grounded, willJump, dead;
    private float jumpTimer;
    private Vector2 groundCheckPos;

    public int Num { get { return num; } set { num = value; } }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        IniPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            MovementControl();
            InputControl();
            JumpControl();
            NumTextControl();
        }
    }

    private void MovementControl()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void InputControl()
    {
        //if (!willJump && Input.GetKeyDown(KeyCode.Space)) JumpCall();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!grounded)
            {
                if (jumps > 0)
                {
                    DoubleJump();
                }
                else if (!willJump)
                {
                    JumpCall();
                }
            }
            else
            {
                Jump();
            }
        }

        for (int number = 0; number <= 9; number++)
        {
            if (Input.GetKeyDown("[" + number.ToString() + "]"))
            {
                num = number > 0 ? number : num;
            }
        }
    }

    private void JumpCall()
    {
        jumpTimer = jumpRememberTime;
        willJump = true;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce);
        jumps--;

        willJump = false;
    }

    private void DoubleJump()
    {
        rb.AddForce(transform.up * jumpForce * 2);

        jumps--;

        willJump = false;
    }

    private void JumpControl()
    {
        CheckGrounded();
        WillJumpControl();

        if (grounded && willJump)
        {
            Jump();
        }

        if (rb.velocity.y > maxYSpeed) rb.velocity = new Vector2(rb.velocity.x, maxYSpeed);
    }

    private void WillJumpControl()
    {
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        else
        {
            willJump = false;
        }
    }

    private void CheckGrounded()
    {
        groundCheckPos = new Vector2(transform.position.x, transform.position.y - 0.5f);
        grounded = Physics2D.OverlapCircle(groundCheckPos, 0.1f, floorMask);

        if (grounded) jumps = 1;
    }

    private void NumTextControl()
    {
        numText.text = num.ToString();
    }

    private void IniPlayer()
    {
        num = 1;
    }
}
