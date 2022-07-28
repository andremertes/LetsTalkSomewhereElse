using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll_Grid : MonoBehaviour
{
    [SerializeField] private Material myMaterial;
    [SerializeField] private Renderer myModel;

    private bool grid_online;
    public static bool mylock;
    public static bool Grid_Pressed;
    private bool[] all_locks;

    void Start()
    {
        grid_online = true;
        mylock = false;
        all_locks = new bool[6];
        Grid_Pressed = false;
    }

    private void Update(){
        if(Grid_Pressed){
            Grid_Pressed = false;
            if(grid_online){
                Color color = new Vector4(0,0,0,0);
                myModel.material.color = color;
                grid_online = false;
                all_locks[0] = Build_Object.mylock;
                all_locks[1] = Delete_Object.mylock;
                all_locks[2] = Change_Object_To_Place.mylock;
                all_locks[3] = Change_Material.mylock;
                all_locks[4] = Change_Mode.mylock;
                all_locks[5] = Move_Object.mylock;
                Build_Object.mylock = true;
                Delete_Object.mylock = true;
                Change_Object_To_Place.mylock = true;
                Change_Material.mylock = true;
                Change_Mode.mylock = true;
                Move_Object.mylock = true;
                Change_Mode.rend.enabled = false;
            }else{
                Color color = new Vector4(255,255,255,255);
                myModel.material.color = color;
                grid_online = true;
                Build_Object.mylock = all_locks[0];
                Delete_Object.mylock = all_locks[1];
                Change_Object_To_Place.mylock = all_locks[2];
                Change_Material.mylock = all_locks[3];
                Change_Mode.mylock = all_locks[4];
                Move_Object.mylock = all_locks[5];
                
                Change_Mode.rend.enabled = true;
            }
        }
    }
}
