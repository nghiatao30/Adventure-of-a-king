using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStatus player = collider.gameObject.GetComponent<PlayerStatus>();
            player.TakeDamage(5);
        }
    }
}
