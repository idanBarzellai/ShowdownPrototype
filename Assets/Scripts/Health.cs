using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int health;

    public int numOfHp;
    public Sprite fullHealthPoint;
    public Sprite emptyHealthPoint;
    public GameObject[] hpGameObjects;

    List<Image> images = new List<Image>();

    private void Start()
    {
        health = GameManager.instance.baseHealth;
        Debug.Log(health);
        for (int i = 0; i < health; i++)
        {
            images.Add(hpGameObjects[i].GetComponent<Image>());
        }
        for (int i = health; i < hpGameObjects.Length; i++)
        {
            hpGameObjects[i].SetActive(false);
        }
    }
    private void Update()
    {
        health = GetComponent<Player>().health;

        if (health > numOfHp)
        {
            health = numOfHp;        }
        for (int i = 0; i < images.Count; i++)
        {
            if (i < health)
            {
                images[i].sprite = fullHealthPoint;
            }
            else
            {
                images[i].sprite = emptyHealthPoint;
            }
            if (i < numOfHp)
            {
                hpGameObjects[i].SetActive(true);
            }
            else
            {
                hpGameObjects[i].SetActive(false);
            }
        }
    }
}
