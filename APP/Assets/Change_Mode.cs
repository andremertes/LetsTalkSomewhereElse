using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Mode : MonoBehaviour
{
    public Material[] material;
    public static int mode;
    public static bool mylock;
    public static bool canReturn;
    public static bool Change_Left_Pressed;
    public static bool Change_Right_Pressed;
    public static bool Change_Mode_Pressed;
    public static bool Escape_Pressed;
    private int max_modes;
    
    private bool delay_done;
    public static Renderer rend;
    
    void Start()
    {
        mylock = false;
        mode = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[mode];
        delay_done = false;
        canReturn = true;
        max_modes = material.Length;

        Change_Left_Pressed = false;
        Change_Right_Pressed = false;
        Change_Mode_Pressed = false;
        Escape_Pressed = false;
    }
    
    void Update()
    {
        if (delay_done){
            if (mode == 0){ // bauen
                Change_Object_To_Place.mylock = false;
                Change_Material.mylock = true;
                Delete_Object.mylock = true;
                Move_Object.mylock = true;
                Build_Object.mylock = false;

                Build_Object.canSpawn = true;
                Move_Object.canMove = false;
            }else if (mode == 1){   // farbe
                Change_Object_To_Place.mylock = true;
                Change_Material.mylock = false;
                Delete_Object.mylock = true;
                Move_Object.mylock = true;
                Build_Object.mylock = true;

                Build_Object.canSpawn = false;
                Move_Object.canMove = false;
            }else if (mode == 2){   // delete
                Change_Object_To_Place.mylock = true;
                Change_Material.mylock = true;
                Delete_Object.mylock = false;
                Move_Object.mylock = true;
                Build_Object.mylock = true;

                Build_Object.canSpawn = false;
                Move_Object.canMove = false;
            }else if (mode == 3){   // move
                Change_Object_To_Place.mylock = true;
                Change_Material.mylock = true;
                Delete_Object.mylock = true;
                Move_Object.mylock = false;
                Build_Object.mylock = true;

                Build_Object.canSpawn = false;
                Move_Object.canMove = true;
            }
            delay_done = false;
            mylock = true;
        }
        /*if (Change_Right_Pressed){
            Change_Right_Pressed = false;
            if (mode < max_modes-1){
                mode++;
            }else{
                mode = 0;
            }
            rend.sharedMaterial = material[mode];
            
        }else if(Change_Left_Pressed){
            Change_Left_Pressed  = false;
            if (mode > 0){
                mode--;
            }else{
                mode = max_modes - 1;
            }
            rend.sharedMaterial = material[mode];

        }*/
        

        if (Change_Mode_Pressed){
            Change_Mode_Pressed = false;
            delay_done = true;
        }

        if(Escape_Pressed){
            Escape_Pressed = false;
            Debug.Log(canReturn);
            rend.sharedMaterial = material[mode];
            mylock = false;
            Change_Object_To_Place.mylock = true;
            Change_Material.mylock = true;
            Delete_Object.mylock = true;
            Move_Object.mylock = true;
            Build_Object.mylock = true;

            Build_Object.canSpawn = false;
            Move_Object.canMove = false;
        }

        
            
        
    }
}
