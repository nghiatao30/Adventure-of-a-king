using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStatus : MonoBehaviour
{
    public event EventHandler healthChanged;

    public int maxHealth = 100;

    public HealthSystem healthBar; // refer to Healthbar 

    public int health;

    private PlayerControl playerControl;

    private Animator aniPlayer;

    public SpriteRenderer sprite;
    public bool canTakeDamage;
    public int flickerAmnt;
    public float flickerDuration;

    public int keys;

    void Start()
    {
        keys = 0;
        aniPlayer = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
        health = maxHealth;
        healthBar.playerStatus = this; // (1)
        SaveSystem.SavePlayer(this);
        canTakeDamage = true;
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage == true)
        {
            //aniPlayer.SetTrigger("Hurt");
            health -= damage;
            if (health < 0)
                health = 0;
            if (healthChanged != null) healthChanged(this, EventArgs.Empty); // active the event
            if (health == 0)
            playerControl.Die();
            StartCoroutine(DamageFlicker());
        }
    }

    IEnumerator DamageFlicker()
    {
        canTakeDamage = false;
        for (int i = 0; i < flickerAmnt; i++)
        {
            sprite.color = new Color(1f, 1f, 1f, .5f);
            yield return new WaitForSeconds(flickerDuration);
            sprite.color = Color.white;
            yield return new WaitForSeconds(flickerDuration);
            if (i == flickerAmnt - 1) canTakeDamage = true;
        }
    }

    public void RecoverHealth(int value)
    {
        health += value;
        if (health > maxHealth)
            health = maxHealth;
        if (healthChanged != null) healthChanged(this, EventArgs.Empty); // active event
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            TakeDamage(34);
        }
    }

    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        maxHealth = data.maxHealth;
        health = data.health;

        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1] + 1;
        position.z = data.position[2];
        transform.position = position;

        if (healthChanged != null) healthChanged(this, EventArgs.Empty);
        playerControl.enabled = true;

        keys = data.keys;
    }

    public void GetKeys()
    {
        ++keys;
    }
}
  
