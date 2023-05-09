using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{


   public enum STATE
   {
        IDLE, CHASE, ATTACKING
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
   protected Transform[] waypoints;
   protected State nextState;
   
   float visDist = 2.0f;
   float visAngle = 180.0f;



   public State(GameObject _enemy, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints)
   {
     enemy = _enemy;
     agent = _agent;
     animator = _animator;
     player = _player;
     waypoints = _waypoints;
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


}



public class Idle : State
{

    public Idle(GameObject _enemy, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints) : base(_enemy, _agent, _animator, _player, _waypoints)
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
        Debug.Log("Rodando Idle");
        if(canSeePlayer())
        {
          nextState = new Chase(enemy, agent, animator, player, waypoints);
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
    public Chase(GameObject _enemy, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints) : base(_enemy, _agent, _animator, _player, _waypoints)
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
        Debug.Log("Esta perseguindo");
        agent.SetDestination(player.position);

        if(agent.hasPath)
        {
          if(!canSeePlayer())
          {
            Debug.Log("Esta parando de perseguir");
            nextState = new Idle(enemy, agent, animator, player, waypoints);
            stage = EVENT.EXIT;
          }

        }
          
          
        base.Update();
    }

    public override void Exit()
    {
        animator.ResetTrigger("Walking");
        base.Exit();
    }




}

