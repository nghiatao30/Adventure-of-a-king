using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int maxHealth ;
    public int health;
    public float[] position;
    public int keys;

    public PlayerData(PlayerStatus Player)
    {
        this.maxHealth = Player.maxHealth;
        this.health = Player.health;
        position = new float[3];
        position[0] = Player.transform.position.x;
        position[1] = Player.transform.position.y;
        position[2] = Player.transform.position.z;
        keys = Player.keys;
    }

}
