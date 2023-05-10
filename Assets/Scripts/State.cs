using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{


   public enum STATE
   {
        IDLE, CHASE, ATTACK
   }
   public enum EVENT
   {
        ENTER, UPDATE, EXIT
   }
   public STATE stateName;
   protected EVENT stage;
   protected GameObject enemy;
   protected NavMeshAgent agent;
   protected Animator animator;
   protected Transform player;
   protected Transform spawnPoint;
   protected State nextState;
   
   private float visDist = 5.0f;
   private float visAngle = 180.0f;

   private float attackDist = 1.0f;
   private float attackAngle = 20.0f;



   public State(GameObject _enemy, NavMeshAgent _agent, Animator _animator, Transform _player, Transform _spawnPoint)
   {
     enemy = _enemy;
     agent = _agent;
     animator = _animator;
     player = _player;
     spawnPoint = _spawnPoint;
   }

   public virtual void Enter()
   {
     stage = EVENT.UPDATE;
   }
   public virtual void Update()
   {
     stage = EVENT.UPDATE;
   }
   public virtual void Exit()
   {
     stage = EVENT.EXIT;
   }

   public State Process()
   {
     if(stage == EVENT.ENTER)
     {
          Enter();
     }
     else if(stage == EVENT.UPDATE)
     {
          Update();
     }
     else
     {
        Exit();
        return nextState;
     }

     return this;
   }

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


}



public class Idle : State
{

    public Idle(GameObject _enemy, NavMeshAgent _agent, Animator _animator, Transform _player, Transform _spawnPoint) : base(_enemy, _agent, _animator, _player, _spawnPoint)
    {
        stateName = STATE.IDLE;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        animator.SetTrigger("Idle");
        Debug.Log("entrou em idle");
        base.Enter();
    }
    public override void Update()
    {
        agent.isStopped = true;
        if(canSeePlayer() && !canAttackPlayer())
        {
          nextState = new Chase(enemy, agent, animator, player, spawnPoint);
          stage = EVENT.EXIT;
        }
        else if(canAttackPlayer() && canSeePlayer())
        {
          Debug.Log("Esta preparando para atacar");
          nextState = new Attack(enemy, agent, animator, player, spawnPoint);
          stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }

}

public class Chase : State
{
    public Chase(GameObject _enemy, NavMeshAgent _agent, Animator _animator, Transform _player, Transform _spawnPoint) : base(_enemy, _agent, _animator, _player, _spawnPoint)
    {

      stateName = STATE.CHASE;

    }

    public override void Enter()
    {
      agent.speed = 1.5f;
      agent.isStopped = false;
      animator.SetTrigger("Walking");
      base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        if(agent.hasPath)
        {
          if(!canSeePlayer())
          {
        
              Debug.Log("Esta parando de perseguir");
              nextState = new Idle(enemy, agent, animator, player, spawnPoint);
              stage = EVENT.EXIT;

          }
          else if(canAttackPlayer())
          {
            Debug.Log("Esta preparando para atacar");
            nextState = new Attack(enemy, agent, animator, player, spawnPoint);
            stage = EVENT.EXIT;
          }

        }
 
    }

    public override void Exit()
    {
        animator.ResetTrigger("Walking");
        base.Exit();
    }




}

public class Attack : State
{

  public Attack(GameObject _enemy, NavMeshAgent _agent, Animator _animator, Transform _player, Transform _spawnPoint) : base(_enemy, _agent, _animator, _player, _spawnPoint)
    {

      stateName = STATE.ATTACK;

    }

     public override void Enter()
    {
      agent.isStopped = true;
      animator.SetTrigger("Attacking");
      base.Enter();
    }

     public override void Update()
    {

        if(canAttackPlayer())
        {
          Debug.Log("Esta atacando de novo");
          nextState = new Idle(enemy, agent, animator, player, spawnPoint);
          stage = EVENT.EXIT;
        }
        else if(!canAttackPlayer())
        {
          Debug.Log("Esta parando de atacar");
          nextState = new Chase(enemy, agent, animator, player, spawnPoint);
          stage = EVENT.EXIT;
        }

    }

     public override void Exit()
    {
        animator.ResetTrigger("Attacking");
        base.Exit();
    }




}

