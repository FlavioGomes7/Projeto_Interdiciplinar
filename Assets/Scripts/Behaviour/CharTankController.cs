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
    private bool isDamaged;
    private bool isReverse;
    private float rotationMove;
    private float verticalMove;
    [SerializeField] private float speedRotation;
    [SerializeField] private float speed;

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
    private void IsShooting()
    {
        if (Input.GetButton("Fire1"))
        {
            isShooting = true;
            isWalking = false;
        }
        else
        {
            isShooting = false;
        }
    }
    private void IsDamaged()
    {
        isDamaged = true;
    }

    void Start()
    {
        character = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {

        IsMoving();
        IsAiming();
        IsShooting();
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isAiming", isAiming);
        animator.SetBool("isShooting", isShooting);
        animator.SetBool("isReverse", isReverse);
    }
    

}
