using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int health;
    private int damage;
    private int speed;
    private int reward;

    [SerializeField] private GameObject enemy;
    private string enemyName;

    public void hit(int damage)
    {
        health -= damage;
    }

    void Start()
    {
        enemyName = gameObject.name;
        if (enemyName.Equals("ZombieRunner"))
        { 
            health = ZombieRunner.Health;
            damage = ZombieRunner.Damage;
            speed = ZombieRunner.Speed;
            reward = ZombieRunner.Reward;
        }
        else
        {
            health = ZombieWalker.Health;
            damage = ZombieWalker.Damage;
            speed = ZombieWalker.Speed;
            reward = ZombieWalker.Reward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    private void Death()
    {
        if (health <= 0)
        {
            Invoke("Destroy", 3);
        }
    }

    private void Destroy()
    {
        Destroy(enemy);
    }
}
