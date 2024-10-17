using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEditor.U2D;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public AudioSource coinSound;

    public float speed = 15.0f;
    public float jumpForce = 15.0f;
    public float airControlForce = 10.0f; 
    public float airControlMax = 1.5f;
    public float blinkChance = 300.0f;
    public float fastFallAmount = 30.0f;
    public float sprintMultiplier = 1.5f;
    float baseSpeed;
    float baseAnimSpeed;
    bool jumpReady;

    public TextMeshProUGUI uiText;
    public TextMeshPro levelEndText;
    int coinsCollected;
    int coinsInLevel;

    Vector2 boxExtents;      // Variable to contain the vector info for the outer bounds of the BoxCollider
    Animator animator;



    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxExtents = GetComponent<BoxCollider2D>().bounds.extents; // sets the boxExtents variable
        animator = GetComponent<Animator>();
        baseSpeed = speed;
        baseAnimSpeed = animator.speed;
        jumpReady = false;

        coinsCollected = 0;
        coinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    void Update()
    {
        float xSpeed = Mathf.Abs(rigidBody.velocity.x);
        animator.SetFloat("xSpeed", xSpeed);
        float ySpeed = Mathf.Abs(rigidBody.velocity.y);
        animator.SetFloat("ySpeed", ySpeed);

        if (rigidBody.velocity.x * transform.localScale.x < 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

     //Blinking animation handling
        float blinkVal = Random.Range(0.0f, blinkChance);
        if (blinkVal < 1.0f)
        {
            animator.SetTrigger("blinkTrigger");
        }
            else
            {
                animator.ResetTrigger("blinkTrigger");
            }

     // Coin UI handling
        string coinUI = coinsCollected + "/" + coinsInLevel;
        uiText.text = coinUI;

        if (coinsCollected != coinsInLevel) { levelEndText.text = "Collect all coins first!";  }
        else { levelEndText.text = ""; }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        // Sprint Control
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = baseSpeed * sprintMultiplier;
            animator.speed = baseAnimSpeed * sprintMultiplier;
        }
        else
        { 
            speed = baseSpeed; 
            animator.speed = baseAnimSpeed; 
        }

        // check if we are on the ground
        Vector2 bottom =
        new Vector2(transform.position.x, transform.position.y - boxExtents.y);

        Vector2 hitBoxSize = new Vector2(boxExtents.x * 2.0f, 0.05f);

        RaycastHit2D result = Physics2D.BoxCast(bottom, hitBoxSize, 0.0f,
        new Vector3(0.0f, -1.0f), 0.0f, 1 << LayerMask.NameToLayer("Ground"));

        bool grounded = result.collider != null && result.normal.y > 0.9f;
        if (grounded)
        { 
            if (Input.GetAxis("Jump") > 0.0f && jumpReady)
            {
                rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
                jumpReady = false;
            }
            else 
            {
                if (Input.GetAxis("Jump") == 0.0f)
                {
                    jumpReady = true;
                }
                rigidBody.velocity = new Vector2(speed * h, rigidBody.velocity.y);
            }
        }
        else
        {
            // allow a small amount of movement in the air
            float vx = rigidBody.velocity.x;
            if (h * vx < airControlMax)
                rigidBody.AddForce(new Vector2(h * airControlForce, 0));

            //Fast fall when Jump button is released
            if (Input.GetAxis("Jump") == 0.0f || rigidBody.velocity.y < 0)
            {
                rigidBody.AddForce(new Vector2(0.0f, -fastFallAmount), ForceMode2D.Force);
            }
        }

    }
    void OnTriggerEnter2D( Collider2D coll )
    {
        if (coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
            coinsCollected++;
            coinSound.Play();
        }
        if (coll.gameObject.tag == "KillPlayer")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // reset the level
        }
        if (coll.gameObject.tag == "LevelEnd")
        {
            if (coinsCollected == coinsInLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  // next level
            }
        }
    }
} 


