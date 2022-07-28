using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Change_Material : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    Renderer rend2;
    public static int index; 
    public static bool mylock;
    public static bool Change_Left_Pressed;
    public static bool Change_Right_Pressed;
    public static bool Change_Material_Pressed;

    private static XRRayInteractor interactor;

    void Start()
    {
        mylock = true;
        index = 0;
        rend = gameObject.GetComponent<Renderer>();
        rend.enabled = true;
        Change_Left_Pressed = false;
        Change_Right_Pressed = false;
        Change_Material_Pressed = false;

        interactor = GameObject.FindGameObjectWithTag("GameController").GetComponent<XRRayInteractor>();
    }

    GameObject find;
    
     void Update()
     {
            if (mylock){
                return;
            }
           
            rend.sharedMaterial = material[index];
            
            if(Change_Material_Pressed){
                Change_Material_Pressed = false;
                
                if(interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
                {
                    Debug.Log(hit.transform.tag);
                    if(hit.transform.CompareTag("No")) return;

                    find= GameObject.FindGameObjectWithTag(hit.transform.tag);
                    Debug.Log(find.tag);
                    foreach (Transform my_object in find.transform)
                    {
                        foreach (Transform child in my_object.transform)
                        {
                            rend2 = child.gameObject.GetComponent<Renderer>();
                            rend2.enabled = true;
                            if(child.gameObject.CompareTag("Yes")){
                                rend2.sharedMaterial = material[index];
                            }
                        }
                    }
        
                }
        }else if(Change_Right_Pressed){
            Change_Right_Pressed = false;
            if (index < material.Length-1){
                index ++;
            }else {
                index = 0;
            }
        }else if(Change_Left_Pressed){
            Change_Left_Pressed = false;
            if (index > 0){
                index --;
            }else {
                index = material.Length-1;
            }
        }
        
     } 
}