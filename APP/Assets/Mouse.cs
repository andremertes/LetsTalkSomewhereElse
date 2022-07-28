using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float mousesense;
    public Transform playerBody;

    void Start()
    {
        mousesense =1000f;
    }

    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Q)){
            playerBody.Rotate(new Vector3(0,-10,0));
        }else if(Input.GetKeyDown(KeyCode.E)){
            playerBody.Rotate(new Vector3(0,10,0));
        }

        
    }
}
