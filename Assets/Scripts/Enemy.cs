using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int health;
    private int damage;
    private int speed;
    private int reward;
    private bool isDead = true;

    [SerializeField] private GameObject enemy;
    [SerializeField] private AudioClip acZombieNoticed;
    [SerializeField] private AudioClip acZombieAttack;
    [SerializeField] private AudioClip acZombieDeath;
    [SerializeField] private AudioClip acZombieIdle;
    private string enemyName;
    private AudioSource audioSource;
    [SerializeField] private Animator animator;

    public void hit(int damage)
    {
        health -= damage;
    }

    void Start()
    {
        enemyName = gameObject.name;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        
        if (isDead)
        {
            isDead = false;
            PlayDeathAudio();
            animator.SetBool("isDying", true);
            Invoke("Destroy", 3);
        }
    }

    private void Destroy()
    {
        Destroy(enemy);
    }

    private void PlayDeathAudio()
    {
        audioSource.clip = acZombieDeath;
        audioSource.Play();
    }
}
