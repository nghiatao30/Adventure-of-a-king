using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Vector2 distance;
    private float realSpeed;
    public Transform current;

    void Start()
    {
        
        distance = FindObjectOfType<PlayerControl>().transform.position - transform.position;
        realSpeed = (8f / distance.magnitude) * speed;
        current = this.transform;
    }

    void Update()
    {
        transform.Translate(distance * realSpeed * Time.deltaTime);
        if(Mathf.Abs(transform.position.x - current.position.x) >= 15f  || Mathf.Abs(transform.position.y - current.position.y) >= 15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerStatus>().TakeDamage(5);
            Destroy(gameObject);
        }
    }
}
