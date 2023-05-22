using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTankController : MonoBehaviour
{
    Animator animator;
    private CapsuleCollider character;
    private bool isWalking;
    private bool isAiming;
    private bool isShooting;
    private bool isReverse;
    private bool isDamaged;
    private float rotationMove;
    private float verticalMove;
    [SerializeField] private float speedRotation;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    

    private void IsMoving()
    {
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && isAiming == false)
        {
            isWalking = true;
            rotationMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speedRotation;
            verticalMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            character.transform.Rotate(0f, rotationMove, 0f);
            character.transform.Translate(0f, 0f, verticalMove);
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
            rotationMove = Input.GetAxis("Horizontal") * Time.deltaTime * speedRotation;
            character.transform.Rotate(0f, rotationMove, 0f);
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
        yield return new WaitForSeconds (fireRate);
        isShooting = false;
    }


    

    void Start()
    {
        character = GetComponent<CapsuleCollider>();
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
        animator.SetBool("isDamaged", isDamaged);
        animator.SetBool("isReverse", isReverse);
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Spider")
        {

            isDamaged = true;


        }

    }

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "Spider")
        {

            isDamaged = false;

        }
        
    }



}
