using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public Transform player;
    public Transform spawnpoint;
    private Transform enemy;
    [SerializeField] private float visDist = 5.0f;
    [SerializeField]private float visAngle = 180.0f;

    [SerializeField]private float attackDist = 1.0f;
    [SerializeField] private float attackAngle = 20.0f;
    public bool canSeePlayer()
   {
      Vector3 direction = player.position - enemy.transform.position;
      float angle = Vector3.Angle(direction, enemy.transform.forward);
      if(direction.magnitude < visDist && angle < visAngle )
      {
        return true;
      }
      return false;

   }

   public bool canAttackPlayer()
   {
      Vector3 direction = player.position - enemy.transform.position;
      float angle = Vector3.Angle(direction, enemy.transform.forward);
      if(direction.magnitude < attackDist && angle < attackAngle )
      {
        return true;
      }
      return false;

   }
    void Start()
    {

        enemy = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.SetDestination(player.position);
        Debug.Log(agent.hasPath);
        if(agent.hasPath)
        {
            agent.SetDestination(player.position);
            Debug.Log("Tem caminho?");
            if(canSeePlayer() && !canAttackPlayer())
            {
            agent.isStopped = false;
            animator.SetTrigger("Walking");
            Debug.Log("Esta andando");
            }
            else if(canSeePlayer() && canAttackPlayer())
            {
            agent.isStopped = false;
            animator.SetTrigger("Attacking");
            Debug.Log("Esta atacando");
            
            }

        }
        else
        {
            agent.isStopped = true;
            animator.SetTrigger("Idle");

        }
        
        
        
        
    }
}
