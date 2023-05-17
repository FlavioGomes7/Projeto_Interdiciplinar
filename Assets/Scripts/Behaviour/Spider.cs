using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    [SerializeField] private EnemySO enemyInfo;
    Animator animator;
    NavMeshAgent agent;
    public Transform player;
    public Transform spawnpoint;
    private Transform enemy;
    public bool canSeePlayer()
   {
      float distance = Vector3.Distance(player.position, enemy.position);
      if(distance < enemyInfo.visDist )
      {
        return true;
      }
      return false;
      
   }
    
   public bool canAttackPlayer()
   {
      Vector3 direction = player.position - enemy.transform.position;
      float angle = Vector3.Angle(direction, enemy.transform.forward);
      if(direction.magnitude < enemyInfo.attackDist && angle < enemyInfo.attackAngle)
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
    void Update()
    {

        agent.SetDestination(player.position);
        agent.speed = enemyInfo.speed;
        if(agent.hasPath)
        {
            if (!canSeePlayer() || canSeePlayer() && canAttackPlayer())
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
            animator.SetBool("isVisible", canSeePlayer());
            animator.SetBool("isNear", canAttackPlayer());
        }
        
    }
}
