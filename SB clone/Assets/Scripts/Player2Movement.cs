using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    public StageScript stage;

    public int stocks = 3;

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

    public Text player2PercentText;

    public float KBForce2;
    public float KBCounter2;
    public float KBTotalTime2 = 0.1f;

    public bool Knockfromright2;

    public PlayerMovement playerMovement; 
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
        player2PercentText.text = (percent).ToString() + "%";
    }

    private void Move()
    {
        if (KBCounter2 <= 0)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }
        else
        {
            if (Knockfromright2 == true)
            {
                rb.velocity = new Vector2(-KBForce2, KBForce2);
            }
            if (Knockfromright2 == false)
            {
                rb.velocity = new Vector2(KBForce2, KBForce2);
            }
            KBCounter2 -= Time.deltaTime;
        }
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
        isJumping = false;
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
        moveDirection = Input.GetAxis("HorizontalTwo");
        if (Input.GetButtonDown("Jump2") && (isGrounded || jumpCount > 1))
        {
            isJumping = true;
        }
        if (Input.GetButtonDown("Fire2"))
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
            if (KBCounter2 <= 0)
            {
                rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            }
            else
            {
                if (Knockfromright2 == true)
                {
                    rb.velocity = new Vector2(-KBForce2, KBForce2);
                }
                if (Knockfromright2 == true)
                {
                    rb.velocity = new Vector2(KBForce2, KBForce2);
                }
                KBCounter2 -= Time.deltaTime;
            }
        }
    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {

            if (playerMovement.percent == 0)
            {
                playerMovement.KBCounter = playerMovement.KBTotalTime;
            }

            else
            {
                playerMovement.KBCounter = playerMovement.KBTotalTime * playerMovement.percent * 0.1f;
            }


            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.Knockfromright = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.Knockfromright = false;
            }
            playerMovement.percent += 1.5f;
        }
        
        if (collision.gameObject.tag == "BlastZone")
        {
            if (stocks > 1)
            {
                transform.position = new Vector2(0, 4);
                percent = 0;
                stocks--;
            }
            else
            {
                stage.game = true;
            }
                
        }
    }
}

