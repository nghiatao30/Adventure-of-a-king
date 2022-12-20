using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PatrollEnemy : MonoBehaviour
{
    public float speed;
    public float leftBound;
    public float rightBound;
    
    void Start()
    {
        speed = 4f;
    }

    
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if(transform.position.x <= leftBound || transform.position.x >= rightBound)
        {
            speed *= -1;
            Flip();
        }
    }

    private void Flip()
    { 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerStatus>().TakeDamage(15);
        }
    }
}
