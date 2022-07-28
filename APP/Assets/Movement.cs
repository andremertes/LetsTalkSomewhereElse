using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController contr;
    
    public float speed;
    void Start(){
        speed = 5f;
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right  * x + transform.forward * z;
        
        contr.Move(move * speed * Time.deltaTime);
    }
}
