using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Placeable_Object : MonoBehaviour
{
    public bool Placed {
        get;
        private set;
    }

    public Vector3Int Size {
        get;
        private set;
    }

    public Vector3Int Rotation;

    public Material[] material;

    Vector3[] Vertices;

    private void Get_Collider_Vertex_Positions(){
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z)*0.5f;
        Vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z)*0.5f;
        Vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z)*0.5f;
        Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z)*0.5f;
    }

    private void Calculate_Size(){
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for(int i=0;i< vertices.Length; i++){
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i]= Build_Object.current.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Math.Abs((vertices[0]-vertices[1]).x),Math.Abs((vertices[0]-vertices[3]).y),1);
    }

    public Vector3 Get_Position(){
        return transform.TransformPoint(Vertices[0]);
    }

    private void Start(){
        Get_Collider_Vertex_Positions();
        Calculate_Size();
        Rotation = new Vector3Int(0,0,0);
    }

    public virtual void Place(){
        Drag_Object drag = gameObject.GetComponent<Drag_Object>();
        drag.setActiveObj(false);

        Destroy(drag);

        Placed = true;

        
    }

    public void Rotate(){
        transform.Rotate(new Vector3(0,90,0));
        Rotation.y  = Rotation.y + 90;

        Vector3[] vertices = new Vector3[Vertices.Length];
        
        Size = new Vector3Int(Size.y,Size.x,1);
            
        for(int i=0; i < vertices.Length; i++){
            vertices[i] = Vertices[(i+1) % Vertices.Length];
        }
        
        
            
        

        Vertices = vertices;
    }


}
