using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTankController : MonoBehaviour
{
    CapsuleCollider character;
    private bool ismoving;
    private float rotationMove;
    private float verticalMove;
    public float speedRotation;
    public float speed;

    void Start()
    {
        character = GetComponent<CapsuleCollider>();
    }
    
    void Update()
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
