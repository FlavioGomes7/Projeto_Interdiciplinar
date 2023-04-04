using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Controller : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;

    void Update()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(X, 0f, Z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }

    }
}
