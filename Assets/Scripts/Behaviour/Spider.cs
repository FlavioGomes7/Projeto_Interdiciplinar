using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    [SerializeField] private EnemySO enemyInfo;
    Animator animator;
    NavMeshAgent agent;
    private GameObject player;
    public Transform spawnpoint;
    private Transform enemy;
    private bool isAttacking;
    public int hp;


    public bool canSeePlayer()
   {
      float distance = Vector3.Distance(player.transform.position, enemy.position);
      if(distance < enemyInfo.visDist )
      {
        return true;
      }
      return false;
      
   }
    
   public bool canAttackPlayer()
   {
      Vector3 direction = player.transform.position - enemy.transform.position;
      float angle = Vector3.Angle(direction, enemy.transform.forward);
      if(direction.magnitude < enemyInfo.attackDist && angle < enemyInfo.attackAngle)
      {
        return true;
      }
      return false;

   }

   IEnumerator AttackPlayer()
    {
        animator.Play("Spider_Attack");
        player.GetComponent<Animator>().Play("Damage_Pose");
        player.GetComponent<CharTankController>().health -= enemyInfo.attack;
        yield return new WaitForSeconds(enemyInfo.attackRate);
        isAttacking = false;

    }

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        enemy = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hp = enemyInfo.health;
        agent.speed = enemyInfo.speed;
        
    }

    // Update is called once per frame
    void Update()
    {

        agent.SetDestination(player.transform.position);
        if(agent.hasPath)
        {

            if(canAttackPlayer() && !isAttacking)
            {
                agent.isStopped = true;
                isAttacking = true;
                StartCoroutine(AttackPlayer());
            }
            else if((canSeePlayer() && isAttacking) || (!canSeePlayer()) || (hp <= 0))
            {
              agent.isStopped = true;

            }
            else
            {
                agent.isStopped = false;
            }
            animator.SetBool("isVisible", canSeePlayer());
  
        }
        
    }
    


}
