using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Object_To_Place : MonoBehaviour
{
    public Material[] materials;
    public static bool mylock;
    
    public static bool Change_Left_Pressed;
    public static bool Change_Right_Pressed;

    Renderer rend;
    void Start()
    {
        mylock = true;;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        Change_Left_Pressed = false;
        Change_Right_Pressed = false;
    }

    void Update()
    {
        if (mylock){
            return;
        }
        
        rend.sharedMaterial = materials[Build_Object.Optionindex];

        if(Change_Right_Pressed){
            Change_Right_Pressed = false;
            Build_Object.Optionindex++;
            if (Build_Object.Optionindex >= Build_Object.Max_Options){
                Build_Object.Optionindex = 0;
            }
        }else if(Change_Left_Pressed){
            Change_Left_Pressed = false;
            Build_Object.Optionindex--;
            if (Build_Object.Optionindex < 0){
                Build_Object.Optionindex = Build_Object.Max_Options-1;
            }
        }  
    }
}
