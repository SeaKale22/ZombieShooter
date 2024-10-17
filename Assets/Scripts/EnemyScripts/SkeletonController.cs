using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SkeletonController : MonoBehaviour
{
    private GameObject player;
    private PlayerController _playerController;
    
    public float maxHealth;
    private float _currentHealth;
    private TMP_Text _heathText;
    private bool canBeHit = true;
    
    private NavMeshAgent _agent;
    
    public float walkspeed;
    public float chasespeed;

    private float _startingWalkspeed;
    private float _startingChasespeed;

    [SerializeField] LayerMask groundLayer, playerLayer;

    private Animator _animator;

    private BoxCollider _weaponCollider;
    
    //state change
    [SerializeField] private float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;

    // patrolling
    Vector3 destinationPoint;
    bool walkPointSet;
    [SerializeField] float walkingRange;

void Start()
    {
        player = GameObject.FindWithTag("Player");
        _playerController = player.GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _weaponCollider = GetComponentInChildren<BoxCollider>();
        _currentHealth = maxHealth;
        _heathText = GetComponentInChildren<TMP_Text>();
        _startingChasespeed = chasespeed;
        _startingWalkspeed = walkspeed;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (!playerInSight && !playerInAttackRange)
        {
            Patrol();
        }
        if (playerInSight && !playerInAttackRange)
        {
            Chase();
        }
        if (playerInSight && playerInAttackRange)
        {
            Attack();
        }
        
        // Track current health in canvus
        _heathText.text = $"HP: {_currentHealth}";
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.LooseHealth(1);
        }
    }

    private void OnDestroy()
    {
        _playerController.ScorePoint();
    }

    void Patrol()
    {
        if (!walkPointSet)
        {
            SearchForDestination();
        }

        if (walkPointSet)
        {
            _agent.SetDestination(destinationPoint);
        }

        if (Vector3.Distance(transform.position, destinationPoint) < 10)
        {
            walkPointSet = false;
        }

        _agent.speed = walkspeed;
        RunAnimate(false);
        IdleAnimate(false);
        WalkAnimate(true);
    }

    void Chase()
    {
        _agent.SetDestination(player.transform.position);
        _agent.speed = chasespeed;
        WalkAnimate(false);
        IdleAnimate(false);
        RunAnimate(true);
    }

    void Attack()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("1HandedAttack1"))
        {
            AttackAnimate();
            _agent.SetDestination(transform.position);
        }
    }

    // void EnableAttack()
    // {
    //     _weaponCollider.enabled = true;
    // }
    // void DisableAttack()
    // {
    //     _weaponCollider.enabled = false;
    // }
    void SearchForDestination()
    {
        float z = Random.Range(-walkingRange, walkingRange);
        float x = Random.Range(-walkingRange, walkingRange);

        destinationPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destinationPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }

    public void LooseHealth(float value)
    {
        _currentHealth -= value;
        if (value > 0 && canBeHit)
        {
            walkspeed = 0;
            chasespeed = 0;
            if (_currentHealth > 0)
            {
                DamageAnimate();
                StartCoroutine(ResetSpeed());
            }
        }
        HealthCheck();
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(1);
        walkspeed = _startingWalkspeed;
        chasespeed = _startingChasespeed;
    }

    void HealthCheck()
    {
        if (_currentHealth <= 0)
        {
            canBeHit = false;
            walkspeed = 0f;
            chasespeed = 0f;
            _agent.speed = 0f;
            DeathAnimate();
            Destroy(this.gameObject, 3f);
        }
    }
    // Animation Functions
    public void WalkAnimate(bool value)
    {
        _animator.SetBool("isWalking", value);
    }

    public void RunAnimate(bool value)
    {
        _animator.SetBool("isRunning", value);
    }
    public void IdleAnimate(bool value)
    {
        _animator.SetBool("isIdle", value);
    }
    public void AttackAnimate()
    {
        _animator.SetTrigger("isAttack");
    }
    public void DamageAnimate()
    {
        _animator.SetTrigger("isDamaged");
    }
    public void DeathAnimate()
    {
        _animator.SetTrigger("isKilled");
    }
}
