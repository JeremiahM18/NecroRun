using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float horizontalLimit = 2.5f;

    [Header("Jump Settings")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private LayerMask groundLayer;

    //[SerializeField] private float crouchHeight = 0.5f;
    //public float speed = 7.0f;
    //private bool turnLeft = false;

    [Header("Slide Settings")]
    [SerializeField] private float slideDuration = 1f;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private bool isGrounded = false;
    private bool jumpRequested = false;
    public bool IsJumping => !isGrounded;
    public bool isSliding = false;
    public bool IsSliding => isSliding;
    private float slideTimer = 0f;
    private float targetX;

    private void Start()
    {
        targetX = transform.position.x;
    }

    private void Update()
    {
        if(!GameManager.Instance.isPlaying) return;

        if (animator != null)
        {
            animator.SetBool("isRunning", GameManager.Instance.isPlaying);
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer) 
            || Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        #region JUMPING
       
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
            animator?.SetTrigger("Jump");
        }

        //if (isJumping && Input.GetButton("Jump"))
        //{
        //    if (jumpTimer < jumpTime)
        //    {
        //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        //        jumpTimer += Time.deltaTime;
        //    }
        //    else
        //    {
        //        isJumping = false;
        //    }
        //}

        //if (Input.GetButtonUp("Jump"))
        //{
        //    isJumping = false;
        //    jumpTimer = 0;
        //}
        #endregion

        #region Sliding

        if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSlide();
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0f)
            {
                EndSlide();
            }
        }

        #endregion


        #region CROUCHING

        //if(isGrounded && Input.GetButton("Crouch"))
        //{
        //    GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);

        //    if (isJumping)
        //    {
        //        GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        //    }
        //}

        //if (Input.GetButtonUp("Crouch"))
        //{
        //    GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        //}

        #endregion

        #region Left/Right Movement

        float inputX = Input.GetAxisRaw("Horizontal"); //A/D/Arrows

        targetX += inputX * moveSpeed * Time.deltaTime;
        targetX = Mathf.Clamp(targetX, -horizontalLimit, horizontalLimit);
       
        float smoothX = Mathf.Lerp(transform.position.x, targetX, 10f * Time.deltaTime);
        transform.position = new Vector3(smoothX, transform.position.y, transform.position.z);

        #endregion
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        animator?.SetBool("Sliding", true);

    }

    private void EndSlide()
    {
        isSliding = false;
        animator?.SetBool("Sliding", false);
    }

    private void FixedUpdate()
    {
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }
    }
}
