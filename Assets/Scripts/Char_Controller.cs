using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Controller : MonoBehaviour
{
    //Declaração do CharacterController ele que é usado para mover o modelo
    CharacterController controller;
    //Declaração da velocidade que o modelo se movera
    public float velocity;
    //Declaração utilizada para definir o valor de atuação da gravidade
    public float yvelocity;
    float smoothTime = 0.1f;
    float turnSmoothVelocity;
    void Start()
    {
        //Chamando o componente no personagem
        controller = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        // Captura inputs de teclas cadastradas em -1,0,1
        // teclas cadastradas(WASD,UpArrow,DownArrow,LeftArrow,RightArrow)
        float X = Input.GetAxisRaw("Horizontal");
        float Z = Input.GetAxisRaw("Vertical");
        //Declaração para armazenar a direção do personagem nos eixos, normalized para não aumentar a velocidade quando duas teclas estiverem pressionadas
        Vector3 direction = new Vector3(Z, yvelocity, X).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref turnSmoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * velocity * Time.deltaTime);
            
        }

    }
}
