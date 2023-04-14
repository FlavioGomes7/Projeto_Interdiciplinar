using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTankController : MonoBehaviour
{
    private CapsuleCollider character;
    private bool ismoving;
    private float rotationMove;
    private float verticalMove;
    [SerializeField] private float speedRotation;
    [SerializeField] private float speed;

    void Start()
    {
        character = GetComponent<CapsuleCollider>();
    }
    
    void FixedUpdate()
    {
        if(Input.GetButton("Horizontal") || Input.GetButton ("Vertical"))
        {
            ismoving = true;
            rotationMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speedRotation;
            verticalMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            character.transform.Rotate(0f, rotationMove, 0f);
            character.transform.Translate(0f, 0f, verticalMove);
        }
        
    }

    
}
