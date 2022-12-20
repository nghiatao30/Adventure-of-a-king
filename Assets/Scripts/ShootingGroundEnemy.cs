using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class ShootingGroundEnemy : MonoBehaviour
{   

    // add time interval between attacking and running 
    // add health system and animations

    private Animator aniEnemy;
    public Transform player;
    public float attackRange;
    public float miniunDistance;
    public float leftBound;
    public float rightBound;
    public float speed;
    private float nextShotTime;
    public GameObject projectile;
    private bool isRightDir = false;
    private Rigidbody2D rb;
    Vector3 currentPos;


    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        aniEnemy = GetComponent<Animator>();
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            if (Vector2.Distance(transform.position, player.position) < miniunDistance)
            {   
                if(transform.position.x < rightBound || transform.position.x > leftBound)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                    aniEnemy.SetBool("isMoving", true);
                }
                else
                {
                    aniEnemy.SetBool("isMoving", false);
                }
                if (player.position.x > transform.position.x)
                {
                    if (isRightDir) Flip();
                }
                else if (player.position.x < transform.position.x)
                {
                    if (!isRightDir) Flip();
                }
                Attack();
                
            }
            else
            {   
                    aniEnemy.SetBool("isMoving", false);
                    Attack();
            }
        }
    }

    private void Attack()
        {
            if(Time.time > nextShotTime)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                nextShotTime = Time.time + 4f;
                aniEnemy.SetTrigger("attack");
                if (player.position.x > transform.position.x && !isRightDir)
                {
                    Flip();
                }
                else if (player.position.x < transform.position.x && isRightDir)
                {
                    Flip();
                }
            }
       }

    private void Flip()
    {
            isRightDir = !isRightDir;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
    }

}
