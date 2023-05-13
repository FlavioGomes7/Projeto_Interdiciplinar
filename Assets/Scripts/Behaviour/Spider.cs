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
    [SerializeField]private float attackDist = 0.5f;
    [SerializeField] private float attackAngle = 180.0f;
    public bool canSeePlayer()
   {
      float distance = Vector3.Distance(player.position, enemy.position);
      //float angle = Vector3.Angle(distance, enemy.transform.forward);
      if(distance < visDist )
      {
        return true;
      }
      return false;
      
   }
    
   public bool canAttackPlayer()
   {
      Vector3 direction = player.position - enemy.transform.position;
      Debug.Log("Distancia:" + direction.magnitude);
      float angle = Vector3.Angle(direction, enemy.transform.forward);
      Debug.Log("angulo:" + angle);
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
    void Update()
    {

        agent.SetDestination(player.position);
        agent.speed = 1.8f;
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
