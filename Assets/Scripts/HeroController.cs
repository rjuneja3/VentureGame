////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: HeroController.cs
//FileType: Visual C# Source file
//Author : Rohan Juneja - 300987725
//Created On : 11/02/2019
//Last Modified On : 11/04/2019
//Copy Rights : Artificial Order
//Description : Class for defining Player Movements, Scoring and Lives counter
////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{

    [Header("Object Properties")]
    public Animator animator;
    public HeroAnimState heroAnimState;
    public SpriteRenderer spriteRenderer;

    [Header("Physics Related")]
    public Transform groundTarget;
    public Rigidbody2D playerRigidBody;
    public bool isGrounded;
    public float jumpForce = 200.0f;
    public Vector2 maximumVelocity = new Vector2(6.0f, 12.0f);
    public Transform spawnPoint;

    [Header("Sounds")]
    public AudioSource jumpSound;
    public AudioSource coinSound;


    [Header("ScoreBoard")]
    public Text scoreText;
    public Text livesText;
   
    public float scoreCount ;
    public static float cointPoints = 100.0f;
    public static float livesCount = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        heroAnimState = HeroAnimState.IDLE;
        isGrounded = false;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            0.0f);

        //bool hit = Physics2D.Linecast(
        //        transform.position,
        //        groundTarget.position,
        //        1 << LayerMask.NameToLayer("Ground"));


        isGrounded = Physics2D.BoxCast(
            transform.position,
            new Vector2(2.0f, 1.0f), 0.0f,
            Vector2.down, 1.0f,
            1 << LayerMask.NameToLayer("Ground"));


        // Idle
        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            heroAnimState = HeroAnimState.IDLE;
        }


        // moving to the right
        if ((Input.GetAxis("Horizontal") > 0) && (isGrounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroAnimState = HeroAnimState.WALK;
            spriteRenderer.flipX = false;
            playerRigidBody.AddForce(Vector2.right * 30.0f);
        }

        // moving to the left
        if ((Input.GetAxis("Horizontal") < 0) && (isGrounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroAnimState = HeroAnimState.WALK;
            spriteRenderer.flipX = true;
            playerRigidBody.AddForce(Vector2.left * 30.0f);
        }

        // jumping
        if ((Input.GetAxis("Jump") > 0) && (isGrounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.JUMP);
            heroAnimState = HeroAnimState.JUMP;
            playerRigidBody.AddForce(Vector2.up * jumpForce);
            jumpSound.Play();
            isGrounded = false;
        }

        // not jumping
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            heroAnimState = HeroAnimState.IDLE;
        }

        playerRigidBody.velocity =
            new Vector2(
                Mathf.Clamp(playerRigidBody.velocity.x,
                    -maximumVelocity.x, maximumVelocity.x),
                Mathf.Clamp(playerRigidBody.velocity.y,
                    -maximumVelocity.y, maximumVelocity.y));


        scoreText.text = "Score: " + scoreCount;
        livesText.text = "Lives: " + livesCount;

        if(livesCount == 0)
        {
            livesCount = 5;
            scoreCount = 0;

            SceneManager.LoadScene("Main");
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        //Coins Score 
        if (other.gameObject.CompareTag("Coin"))
        {
            // update the scoreboard - add points
            coinSound.Play();
            scoreCount = scoreCount + cointPoints;
            Destroy(other.gameObject);
        }
        //Lives Counter when the player touches the enemies
        if (other.gameObject.CompareTag("Enemy"))
        {
            livesCount -= 1;
            // update the scoreboard - add points
            gameObject.transform.position = spawnPoint.transform.position;
        }
    }


}
