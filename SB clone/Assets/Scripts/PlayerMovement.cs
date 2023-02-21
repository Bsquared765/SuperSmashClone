using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    
    private bool facingRight = true;
    private bool isJumping = false;
    private bool isGrounded;

    private float moveDirection;
    public float jumpForce;
    public float moveSpeed;
    public float checkRadius;
    public float percent = 0;

    private int jumpCount;
    public int maxJumpCount;

    public Transform ceilingCheck;
    public Transform groundCheck;
    
    public LayerMask groundObjects;

    public bool gameOver = false;
    public GameObject blockPrefab;
    float worldHeight;
    float worldWidth;

    public Text player1PercentText;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime = 0.01f;

    public bool Knockfromright;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        jumpCount = maxJumpCount;
        //createBlock();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        worldHeight = Camera.main.orthographicSize * 2.0F;
        worldWidth = worldHeight * Camera.main.aspect;

        Animate();
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }
    }

    //Better for handling physics. Can be called multiple times per update frame
    private void FixedUpdate()
    {
        //Checks if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);


        Move();
        player1PercentText.text = (percent).ToString() + "%";
    }

    private void Move()
    {
        if(KBCounter <= 0)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }
        else
        {
            if(Knockfromright == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if (Knockfromright == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }
            KBCounter -= Time.deltaTime;
        }
        if (isJumping)
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                jumpCount--;
            }
            else
            {
                Physics2D.gravity = new Vector2(0, 18f);
                rb.AddForce(new Vector2(0f, jumpForce * 1.5f));
                
                jumpCount--;
                //StartCoroutine(ExampleCoroutine());
                Physics2D.gravity = new Vector2(0, -9f);
            }
        }
        isJumping = false;
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount > 1))
        {
            isJumping = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            /*if (!facingRight)
            {
                rb.AddForce(new Vector2(-50f * percent, 25f * percent));
            }
            if (facingRight)
            {
                rb.AddForce(new Vector2(50f * percent, 25f * percent));
            }
            percent += 15;*/
            
            
            /*if (Knockfromright == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if (Knockfromright == true)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }
            KBCounter -= Time.deltaTime;*/
            
        }
    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        
        transform.Rotate(0f, 180f, 0f);
        
    }

    
}
