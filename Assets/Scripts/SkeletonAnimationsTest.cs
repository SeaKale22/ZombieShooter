using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAnimationsTest : MonoBehaviour
{
    // public GameObject player;
    // private NavMeshAgent _agent;
    //
    // [SerializeField] LayerMask groundLayer, playerLayer;

    private Animator _animator;
    
    // //state change
    // [SerializeField] private float sightRange, attackRange;
    // bool playerInSight, playerInAttackRange;

    // patrolling
    // Vector3 destinationPoint;
    // bool walkPointSet;
    // [SerializeField] float walkingRange;

void Start()
    {
        _animator = GetComponent<Animator>();
        // _agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    // void Update()
    // {
    //     playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
    //     playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
    //     if (!playerInSight && !playerInAttackRange)
    //     {
    //         Patrol();
    //     }
    //     if (playerInSight && !playerInAttackRange)
    //     {
    //         Chase();
    //     }
    //     if (playerInSight && playerInAttackRange)
    //     {
    //         Attack();
    //     }
    // }

    // void Patrol()
    // {
    //     if (!walkPointSet)
    //     {
    //         SearchForDestination();
    //     }
    //
    //     if (walkPointSet)
    //     {
    //         _agent.SetDestination(destinationPoint);
    //     }
    //
    //     if (Vector3.Distance(transform.position, destinationPoint) < 10)
    //     {
    //         walkPointSet = false;
    //     }
    //     WalkAnimate(true);
    //     IdleAnimate(false);
    // }
    //
    // void Chase()
    // {
    //     _agent.SetDestination(player.transform.position);
    //     WalkAnimate(true);
    //     IdleAnimate(false);
    // }
    //
    // void Attack()
    // {
    //     ;
    // }
    //
    // void SearchForDestination()
    // {
    //     float z = Random.Range(-walkingRange, walkingRange);
    //     float x = Random.Range(-walkingRange, walkingRange);
    //
    //     destinationPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
    //
    //     if (Physics.Raycast(destinationPoint, Vector3.down, groundLayer))
    //     {
    //         walkPointSet = true;
    //     }
    // }
    
    // Animation Functions
    public void WalkAnimate(bool value)
    {
        _animator.SetBool("isWalking", value);
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
