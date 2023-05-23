using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;




public class PlayerControl : MonoBehaviour
{
    private float speedMovement = 8.0f;
    private float horizontalInput;
    private float jumpForce = 8.5f;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private bool isRightDir = true;
    private bool isAttacking;
    public Camera mainCamera;
    Vector3 cameraPos;
    private static PlayerControl Instance;


    private bool isOnGround ;
    //public Transform groundCheck;
    //public float checkRadius;
    public LayerMask whatisGround;
    private CapsuleCollider2D capCollider2d;


    private int countJump;
    private int extraNum;
    public int extraJumpValues;

    private float fallMul = 3.5f; // for better jump
    private float lowJump = 3f;   // for better jump

    public float attackRate = 4f;
    float nextAttack = 0f;


    public float attackRange;
    public Transform attackPoint;
    public LayerMask enemyLayers;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        countJump = 0;
        capCollider2d = GetComponent<CapsuleCollider2D>();
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
        
    } 


    void Update()
    {   


        ExtraJump();
        if(isOnGround)
        {
            playerAnimator.SetBool("Fall", false);
            playerAnimator.SetBool("isJumping", false);  
        }

        if (playerRb.velocity.y == 0)
        {
            playerAnimator.SetBool("jumpAttack", false);
        }

       if(transform.position.y < -19)
        {
            Die();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        isOnGround = IsGrounded();

        // move left and right
        horizontalInput = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("MoveSpeed", Mathf.Abs(horizontalInput));
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speedMovement);
        
        // change direction
        if(horizontalInput < 0 && isRightDir)
        {
            flip();
        }
        else if(horizontalInput > 0 && !isRightDir)
          {
            flip();
          }

        // Better jump
        if(playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector2.up * Physics2D.gravity * (fallMul - 1) * Time.deltaTime;
            playerAnimator.SetBool("Fall", true);
            playerAnimator.SetBool("isJumping", false);

           
        } else if (playerRb.velocity.y > 0 )
            {
            playerAnimator.SetBool("isJumping", true);
            playerAnimator.SetBool("Fall", false);
            if (!Input.GetKey(KeyCode.Z))
            playerRb.velocity += Vector2.up * Physics2D.gravity * (lowJump - 1) * Time.deltaTime;
        }

        //Jump faster



        // Jump attack and Normal attack
        if (Input.GetKeyDown(KeyCode.C) && isOnGround)
        {
            if (Time.time >= nextAttack)
            {
                Attack();
                isAttacking = false;
                nextAttack = Time.time + 1f / attackRate;
            }
        }
        else if (!isOnGround && Input.GetKeyDown(KeyCode.C) && playerRb.velocity.y > 0 && countJump > 0)
            {
            playerAnimator.SetBool("jumpAttack", true);
            countJump--;
        }
       if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump Attack"))
        {
            playerAnimator.SetBool("jumpAttack", false);
        }

        

        //Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, cameraPos.z);
        }
    }

    private bool IsGrounded()
    {   
        float height = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(capCollider2d.bounds.center, capCollider2d.bounds.size, 0f, Vector2.down, height, whatisGround);
        return raycastHit.collider != null;

    }


    private void ExtraJump()
    {
    
        // extra jump
        if (isOnGround == true)
        {
            extraNum = extraJumpValues;
            countJump = 1;
        }
        if (Input.GetKeyDown(KeyCode.Z) && extraNum > 0 )
        {
            if (countJump == 0) countJump++;
            playerRb.velocity = Vector2.up * jumpForce;
            --extraNum;
        }
        else if (isOnGround == true && Input.GetKeyDown(KeyCode.Z) && extraNum == 0)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }
    private void Attack()
    {   
        playerAnimator.SetTrigger("Attacking");
        isAttacking = true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {   if(enemy.gameObject.CompareTag("Boss"))
            {
                enemy.GetComponent<BossHealth>().TakeDamage(75);
            }
            else
            {
                enemy.GetComponent<EnemyStatus>().takeDamage(25);
            }
        }
    }

    private void flip()
    {
        if (!isAttacking)
        {
            isRightDir = !isRightDir;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void Die()
    {
        playerAnimator.SetBool("isJumping",false);
        playerAnimator.SetBool("Fall", false);
        playerAnimator.SetTrigger("Die");
        this.enabled = false;
        StartCoroutine(WaitToOpenMenu());
    }

    IEnumerator WaitToOpenMenu()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Game Handler").GetComponent<GameHandler>().OpenMenu();
    }


}
