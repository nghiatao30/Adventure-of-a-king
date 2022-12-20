using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading;
using System.Collections.Specialized;
using System.Net.Mime;


public class EnemyAi : MonoBehaviour
{   


    // Die Animation 
    // Friction problem

    private Transform Enemy;
    public Transform player;
    private Animator enemyAnimation;
    public Transform AiParent;
    public float speed;



    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        enemyAnimation = GetComponent<Animator>();
        Enemy = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 distance = new Vector2(AiParent.position.x - Enemy.position.x,Enemy.position.y);
        rb.velocity = distance * speed ;

        enemyAnimation.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if (player.position.x > Enemy.position.x)
        {
            Enemy.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (player.position.x < Enemy.position.x)
        {
            Enemy.localScale = new Vector3(-1f, 1f, 1f);
        }

        

    }
    
}

