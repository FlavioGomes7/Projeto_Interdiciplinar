using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;
    public Transform player;
    State currentState;
    public Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = new Idle(gameObject, agent, animator, player, spawnpoint);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
