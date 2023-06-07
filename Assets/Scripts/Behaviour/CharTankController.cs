using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CharTankController : MonoBehaviour
{
    private Animator animator;
    private GameObject character;
    private bool isWalking;
    private bool isAiming;
    private bool isShooting;
    private bool isReverse;
    private bool isDamaged;
    private float rotationMove;
    private float verticalMove;
    [SerializeField] public float speedRotation;
    [SerializeField] public float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireRange;
    [SerializeField] public int health;
    [SerializeField] private int attack;
    

    private void IsMoving()
    {
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && isAiming == false)
        {
            isWalking = true;
            rotationMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speedRotation;
            verticalMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            transform.Rotate(0f, rotationMove, 0f);
            transform.Translate(0f, 0f, verticalMove);
            if(Input.GetAxisRaw("Vertical") == -1)
            {
                isReverse = true;
            }
            else
            {
                isReverse = false;
            }
        }

        else
        {
            isReverse = false;
            isWalking = false;
        }

        /*if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && isAiming == false)
        {
            isWalking = true;
            Vector3 direction = new Vector3( Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Horizontal"));
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            if(direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speedRotation * Time.deltaTime);
            }
           
        }

        else
        {
            isReverse = false;
            isWalking = false;
        }*/
    }
    private void IsAiming()
    {
        if(Input.GetButton("Fire2"))
        {
    
            Vector3 targetposition = GameManager.instance.AutoAim().transform.position;
            Vector3 forward = new Vector3(targetposition.x - transform.position.x, targetposition.y - transform.position.y, targetposition.z - transform.position.z);
            transform.forward = forward;
            isAiming = true;
            isWalking = false;
        }
        else
        {
            isAiming = false;
        }
  
    }
    IEnumerator FireShoot()
    {
        animator.Play("Fire_Shooter_Anim");
        GameObject target = GameManager.instance.AutoAim();
        target.GetComponent<Spider>().hp -= attack;
        yield return new WaitForSeconds (fireRate);
        isShooting = false;
    }

   /*public bool CanAttackEnemy()
    {
        Vector3 direction = player.transform.position - enemy.transform.position;
        float angle = Vector3.Angle(direction, enemy.transform.forward);
        if (direction.magnitude < enemyInfo.attackDist && angle < enemyInfo.attackAngle)
        {
            return true;
        }
        return false;

    }*/




    void Start()
    {
        character = GetComponent<GameObject>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2") && isShooting == false)
        {
          isShooting = true;
          StartCoroutine(FireShoot());

        }
        IsMoving();
        IsAiming();
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isAiming", isAiming);
        animator.SetBool("isReverse", isReverse);
        if(health <= 0)
        {
            GameManager.instance.BackToMenu();
        }
    }

   



}
