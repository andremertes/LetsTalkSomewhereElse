using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_Controll : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)){
            if (!Change_Mode.mylock){
                Change_Mode.Change_Mode_Pressed = true;
            }else if ((Change_Mode.mode == 0)&& !(Build_Object.mylock)){
                Build_Object.Pressed_To_Place = true;
            }else if ((Change_Mode.mode == 1)&& !(Change_Material.mylock)){
                Change_Material.Change_Material_Pressed = true;
            }else if ((Change_Mode.mode == 2)&& !(Delete_Object.mylock)){
                Delete_Object.Delete_Pressed = true;
            }else if ((Change_Mode.mode == 3)&& !(Move_Object.mylock)){
                Move_Object.Move_Pressed = true;
            }

        }else if (Input.GetKeyDown(KeyCode.X)){
            if (!Change_Mode.mylock)
            {
                Change_Mode.Change_Left_Pressed = true;
            }
            else if ((Change_Mode.mode == 0) && !(Build_Object.mylock) && Build_Object.canRotate)
            {
                Build_Object.Change_Rotation_Left = true;
            }
            else if ((Change_Mode.mode == 0) && !(Build_Object.mylock) && Build_Object.canSpawn)
            {
                Change_Object_To_Place.Change_Left_Pressed = true;
            }
            else if ((Change_Mode.mode == 1) && !(Change_Material.mylock))
            {
                Change_Material.Change_Left_Pressed = true;
            }
            else if ((Change_Mode.mode == 3) && !(Move_Object.mylock) && Move_Object.canRotate)
            {
                Move_Object.Change_Rotation_Left = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.C)){
            if (!Change_Mode.mylock)
            {
                Change_Mode.Change_Right_Pressed = true;
            }
            else if ((Change_Mode.mode == 0) && !(Build_Object.mylock) && Build_Object.canRotate)
            {
                Build_Object.Change_Rotation_Right = true;
            }
            else if ((Change_Mode.mode == 0) && !(Build_Object.mylock) && Build_Object.canSpawn)
            {
                Change_Object_To_Place.Change_Right_Pressed = true;
            }
            else if ((Change_Mode.mode == 1) && !(Change_Material.mylock))
            {
                Change_Material.Change_Right_Pressed = true;
            }
            else if ((Change_Mode.mode == 3) && !(Move_Object.mylock) && Move_Object.canRotate)
            {
                Move_Object.Change_Rotation_Right = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape)){
            if (Change_Mode.canReturn){
            Change_Mode.Escape_Pressed = true;
            }else if ((Change_Mode.mode == 0)&& !(Build_Object.mylock)&& !Change_Mode.canReturn ){
                Build_Object.Pressed_To_Destroy = true;
            }else if ((Change_Mode.mode == 3)&& !(Move_Object.mylock)){
                Move_Object.Pressed_To_Destroy = true;
            }
        }else if (Input.GetKeyDown(KeyCode.G)){
            if (!Controll_Grid.mylock){
                Controll_Grid.Grid_Pressed = true;
            }
        }
        
    }
}
