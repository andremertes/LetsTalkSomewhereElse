/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Object : MonoBehaviour
{
    private Vector3 diff;

    [SerializeField] ControllerInputManager inputManager;

    private void OnMouseDown(){
        diff = transform.position - Build_Object.Get_Contr_Position();
    }

    private void OnMouseDrag(){
        Vector3 position = Build_Object.Get_Contr_Position() + diff;
        transform.position = Build_Object.current.Snap_To_Grid(position);
    }

    public void Update()
    {
        


    }

}*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Object : MonoBehaviour
{
    private Vector3 diff;

    [SerializeField] public ControllerInputManager inputManager;
    private bool isActiveObject = false;

    void Update()
    {
        if (inputManager.IsRightGripHold() && isActiveObject)
        {
            OnGripDown();
            OnGripDrag();
        }
    }

    public void OnGripDown()
    {
        diff = transform.position - Build_Object.Get_Contr_Position();
        Debug.Log("obj-pos: " + transform.position + "diff: " + diff);
        isActiveObject = true;
    }

    public void OnGripDrag()
    {
        
        Vector3 position = transform.position - diff;

        if (position.x < -4 || position.x > 4.5 || position.z < -3 || position.z > 3.5)
        {

        } else
        {
            transform.position = Build_Object.current.Snap_To_Grid(position);
        }
            
        
    }

    public void setActiveObj(bool status)
    {
        this.isActiveObject = status;
    }
}

