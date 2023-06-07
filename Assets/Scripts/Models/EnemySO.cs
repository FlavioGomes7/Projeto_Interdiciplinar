using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create New Enemy" )]

public class EnemySO : ScriptableObject
{
    [SerializeField] public string id;
    [SerializeField] public bool isStun;
    [SerializeField] public float visDist;
    [SerializeField] public float attackDist;
    [SerializeField] public float attackAngle;
    [SerializeField] public float attackRate;
    [SerializeField] public int attack;
    [SerializeField] public int health;
    [SerializeField] public float speed;


}
