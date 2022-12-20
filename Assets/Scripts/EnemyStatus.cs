using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private int enemyMaxHealth = 100;
    public int enemyHealth = 0;
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    public void takeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth < 0) enemyHealth = 0;
        if (enemyHealth == 0) Die();
    }

    void Die()
    {
        //GetComponent<CapsuleCollider2D>().enabled = false;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
