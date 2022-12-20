using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustShootingEnemy : MonoBehaviour
{
    private Animator aniEnemy;
    public Transform player;
    public float attackRange;
    private float nextShotTime;
    public GameObject projectile;
    private Rigidbody2D rb;
    private bool isRightDir = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        aniEnemy = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            Attack();           
        }
        if (player.position.x > transform.position.x)
        {
            if (isRightDir) Flip();
        }
        else if (player.position.x < transform.position.x)
        {
            if (!isRightDir) Flip();
        }
    }

    private void Attack()
    {
        if (Time.time > nextShotTime)
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
