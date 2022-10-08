using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动参数")]
    public float speed = 8f;
    public float horizontalDivisor = 0.5f;
    public float xVelocity;
    private float offSetY;
    private float sizeY;


    [Header("跳跃参数")]
    public float jumpForce = 0.6f;
    private float holdBoost = 0.4f;
    private float crouchBoost = 0.4f;
    private float jumpDuration;
    private float hangingJumpForce = 3.2f;

    [Header("环境检测")]
    public LayerMask ground;
    private float widthDistance = 0.3f;

    [Header("状态")]
    public bool isCrouch;
    private bool isJumping;
    private bool isHeldJumping;
    public bool isOnGround;
    private bool isOverHead;
    public bool isHanging;

    [Header("物理组件")]
    private Rigidbody2D rb;
    private BoxCollider2D box;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        box = this.gameObject.GetComponent<BoxCollider2D>();
        offSetY = box.offset.y;
        sizeY = box.size.y;
    }

    void Update()
    {
        if (GameManager.isGameOver)
            return;
        //地面头顶检测
        RaycastHit2D leftCheck = Raycast(-widthDistance, 0f, Vector2.down, 0.2f, ground);
        RaycastHit2D rightCheck = Raycast(widthDistance, 0f, Vector2.down, 0.2f, ground);
        RaycastHit2D headCheck = Raycast(0f, box.size.y, Vector2.up, 0.2f, ground);

        //悬挂检测
        float faceDirection = this.transform.localScale.x;
        Vector2 hangDirection = new Vector2(faceDirection, 0f);
        RaycastHit2D hangTopCheck = Raycast(faceDirection * widthDistance, box.size.y, hangDirection, 0.4f, ground);
        RaycastHit2D hangBottomCheck = Raycast(faceDirection * widthDistance, box.size.y - 0.4f, hangDirection, 0.4f, ground);
        RaycastHit2D catchCheck = Raycast(faceDirection * (widthDistance + 0.4f), box.size.y, Vector2.down, 0.4f, ground);

        if (!hangTopCheck && hangBottomCheck && catchCheck && !isOnGround && rb.velocity.y < 0)
        {
            if (!isHanging)
            {
                Vector3 position = this.transform.position;
                position.x += faceDirection * (hangBottomCheck.distance - 0.08f);
                position.y -= (catchCheck.distance);
                this.transform.position = position;
                isHanging = true;
            }
        }

        if (isHanging)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (leftCheck || rightCheck)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if (headCheck)
        {
            isOverHead = true;
        }
        else
        {
            isOverHead = false;
        }

        //蹲起
        if (Input.GetButton("Crouch"))
        {
            if (isHanging)
            {
                isHanging = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            if (!isCrouch && isOnGround)
            {
                Crouch();
            }
        }
        else
        {
            StandUp();
        }

        //跳跃
        if (Input.GetButtonDown("Jump") && !isJumping && (isOnGround || isHanging))
        {
            isJumping = true;
            jumpDuration = Time.time + 0.1f;
            AudioManager.PlaySFX(2);
        }

        if (Input.GetButton("Jump"))
        {
            isHeldJumping = true;
        }
        else
        {
            isHeldJumping = false;
        }

        if (Time.time > jumpDuration)
        {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.isGameOver)
        {
            this.gameObject.SetActive(false);
            return;
        }
        HorizontalMove();

        if (isJumping)
        {
            Jump();
        }
    }

    /* 横轴移动 */
    private void HorizontalMove()
    {
        if (isHanging)
            return;
        xVelocity = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * (isCrouch ? xVelocity * 0.5f : xVelocity), rb.velocity.y);
        if (xVelocity > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xVelocity < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /* 下蹲 */
    private void Crouch()
    {
        isCrouch = true;
        box.offset = new Vector2(box.offset.x, offSetY * 0.5f);
        box.size = new Vector2(box.size.x, sizeY * 0.5f);

    }

    /* 站立 */
    private void StandUp()
    {
        if (!isOverHead)
        {
            isCrouch = false;
            box.offset = new Vector2(box.offset.x, offSetY);
            box.size = new Vector2(box.size.x, sizeY);
        }
    }

    /* 跳跃 */
    private void Jump()
    {
        float totalJumpForce = jumpForce;
        if (isCrouch)
        {
            totalJumpForce += crouchBoost;
            StandUp();
        }
        if (isHeldJumping)
            totalJumpForce += holdBoost;
        if (isHanging)
        {
            totalJumpForce = hangingJumpForce;
            isHanging = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        Vector2 yVelocity = new Vector2(0f, totalJumpForce);
        rb.AddForce(yVelocity, ForceMode2D.Impulse);

    }

    /* 射线 */
    private RaycastHit2D Raycast(float offsetX, float offSetY, Vector2 direction, float distance, LayerMask mask)
    {
        Vector2 startPoint = new Vector2(this.transform.position.x + offsetX, this.transform.position.y + offSetY);
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, distance, mask);
        Color color = hit ? Color.green : Color.red;
        Debug.DrawRay(startPoint, direction, color);
        return hit;
    }


}
