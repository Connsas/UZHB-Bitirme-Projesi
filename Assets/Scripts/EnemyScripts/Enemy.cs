using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    public enum Direction
    {
        FRONT,
        BACK
    }

    public enum EnemyType
    {
        RUNNER,
        WALKER
    }

    private int health;
    private int damage;
    private float speed;
    private float detectRange;
    private float attackRange;
    private float attackCooldown;
    private float currentCooldown;
    private int reward;
    private bool isKillable = true;
    private bool isAttacking = false;
    private bool isChasing = false;
    private bool isNoticed = true;

    private bool noticedAudioPlayed = false;

    [SerializeField] private float soundDistanceLimit = 10f;

    [SerializeField] private NavMeshAgent agent;

    private Vector3 walkPoint;
    [SerializeField] private float walkPointRange;
    private bool walkPointSet;
    [SerializeField] private LayerMask groundMask;
    private float patrolSpeed = 1f;

    [SerializeField] private EnemyType enemyType;
    private Direction direction = Direction.FRONT;
    private Transform target;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject _ammoBox;
    [SerializeField] private GameObject _damageWarning;
    [SerializeField] private AudioClip acZombieNoticed;
    [SerializeField] private AudioClip acZombieAttack;
    [SerializeField] private AudioClip acZombieDeath;
    [SerializeField] private AudioClip acZombieIdle;
    private string enemyName;
    private AudioSource audioSource;
    private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _playerGotHit;
    [SerializeField] private Animator animator;

    void Start()
    {
        enemyName = gameObject.name;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        target = GameObject.Find("PlayerObject").transform;
        //_damageWarning.SetActive(false);
        _playerAudioSource = GameObject.Find("PlayerObject").GetComponent<AudioSource>();
        if (enemyType == EnemyType.RUNNER)
        { 
            health = ZombieRunner.Health;
            damage = ZombieRunner.Damage;
            speed = ZombieRunner.Speed;
            reward = ZombieRunner.Reward;
            attackRange = ZombieRunner.AttackRange;
            detectRange = ZombieRunner.DetectRange;
            attackCooldown = ZombieRunner.AttackCooldown;
        }
        else
        {
            health = ZombieWalker.Health;
            damage = ZombieWalker.Damage;
            speed = ZombieWalker.Speed;
            reward = ZombieWalker.Reward;
            attackRange = ZombieWalker.AttackRange;
            detectRange = ZombieWalker.DetectRange;
            attackCooldown = ZombieWalker.AttackCooldown;
        }
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        SetAudioVolumeByDistance();
        if (health <= 0)
        {
            Death();
        }
        else if (target != null && isKillable && PlayerStats.playerHealth > 0)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange && isKillable)
            {
                animator.SetBool("isWalking", false);
                if(enemyType == EnemyType.WALKER) animator.SetBool("isMoving", false);
                Attack();
            }
            else if (distanceToTarget <= detectRange && isKillable)
            {
                if(enemyType == EnemyType.RUNNER) animator.SetBool("isWalking", false);
                MoveTowardsTarget();
                if (isNoticed)
                {
                    StartCoroutine(PlayNoticedAudio());
                }
                isChasing = true;
            }else if (distanceToTarget >= detectRange && isKillable)
            {
                Patroling();
            }
            else
            {
                animator.SetBool("isWalking", false);
                isChasing = false;
                animator.SetBool("isMoving", false);
                animator.SetBool("isAttacking", false);
            }
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
            else
            {
                if (!isNoticed) audioSource.clip = acZombieIdle;
                isAttacking = false;
            }
        }
        else if(PlayerStats.playerHealth <= 0)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
            audioSource.clip = acZombieIdle;
            audioSource.Play();
            GameState.GameCondition = GameState.GameConditions.GAME_LOST;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("GameEnded");
        }
    }

    private void Death()
    {
        
        if (isKillable)
        {
            agent.SetDestination(transform.position);
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", false);
            AmmoGenerate.ammoDropCount++;
            if (AmmoGenerate.ammoDropCount >= AmmoGenerate.ammodDropCountLimit)
            {
                Vector3 position = enemy.transform.position;
                position.y += 0.27f;
                Instantiate(_ammoBox, position, transform.rotation);
                AmmoGenerate.ammoDropCount = 0;
            }
            isKillable = false;
            PlayDeathAudio();
            if (direction == Direction.FRONT)
            {
                animator.SetBool("isDyingForward", true);
            }
            else if (direction == Direction.BACK)
            {
                animator.SetBool("isDyingBackward", true);
            }
            
            PlayerStats.reward += reward;
            EnemyCounter.EnemyCount--;
            Invoke("Destroy", 2.5f);
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

    private IEnumerator PlayNoticedAudio()
    {
        if (!noticedAudioPlayed)
        {
            audioSource.PlayOneShot(acZombieNoticed);
            noticedAudioPlayed = true;
        }
        yield return new WaitForSeconds(3);
        isNoticed = false;
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
        if (!isAttacking && PlayerStats.playerHealth > 0)
        {
            isAttacking = true;
            PlayerStats.playerHealth -= damage;
            currentCooldown = attackCooldown;
            if (PlayerStats.playerHealth > 0)
            {
                _playerAudioSource.clip = _playerGotHit;
                _playerAudioSource.Play();
                audioSource.clip = acZombieAttack;
                audioSource.Play();
                StartCoroutine(activeAndDeactiveDamageWarning());
            }
        }
    }

    private void MoveTowardsTarget()
    {
        agent.speed = speed;
        agent.SetDestination(target.position);
        if (!isAttacking)
        {
            animator.SetBool("isMoving", true);
            if (enemyType == EnemyType.WALKER)
            {
                animator.SetBool("isWalking", true);
            }
            animator.SetBool("isAttacking", false);
        }
    }

    IEnumerator activeAndDeactiveDamageWarning()
    {
        _damageWarning.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _damageWarning.SetActive(false);
    }

    private void SearchWalkPoint()
    {
        float randomZ = 2 * Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x , transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask)) walkPointSet = true;
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        animator.SetBool("isWalking",true);
        if (enemyType == EnemyType.WALKER)
        {
            animator.SetBool("isMoving", true);
        }

        agent.speed = patrolSpeed;

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;

    }

    public void EnemyGotHit(int damage, Direction direction)
    {
        health -= damage;
        this.direction = direction;
    }

    public void SetAudioVolumeByDistance()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance >= soundDistanceLimit)
        {
            audioSource.volume = 0f;
        }
        else
        {
            audioSource.volume = (soundDistanceLimit / soundDistanceLimit) - (distance / soundDistanceLimit);
        }

    }
}
