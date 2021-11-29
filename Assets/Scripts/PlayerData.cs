using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int baseHealth;
    public int health;
    public bool isAlive;
    public float[] position;

    public PlayerData(Player player)
    {
        baseHealth = player.baseHealth;
        health = player.health;
        isAlive = player.isAlive;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
