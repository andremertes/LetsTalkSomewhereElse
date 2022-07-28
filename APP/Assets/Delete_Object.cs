using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR.Interaction.Toolkit;

public class Delete_Object : MonoBehaviour
{

    public GridLayout gridLayout;
    private Placeable_Object object_To_Be_Placed;
    [SerializeField] private Tilemap tilemap;
    public static bool mylock;
    public static bool Delete_Pressed;
    public Material[] material;
    Renderer rend;

    GameObject find;

    private static XRRayInteractor interactor;

    void Start()
    {
        mylock = true;
        rend = gameObject.GetComponent<Renderer>();
        rend.enabled = true;
        Delete_Pressed = false;

        interactor = GameObject.FindGameObjectWithTag("GameController").GetComponent<XRRayInteractor>();
    }
    
     void Update()
     { 
        if (mylock){
                return;
        }
        rend.sharedMaterial = material[0];

        if(Delete_Pressed){
            Delete_Pressed = false;

            if (interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {

                if (hit.transform.CompareTag("No")) return;
                find = GameObject.FindGameObjectWithTag(hit.transform.tag);

                int tmp = int.Parse(hit.transform.tag.Substring(3, hit.transform.tag.Length - 3));
                object_To_Be_Placed = find.GetComponent<Placeable_Object>();
                Vector3Int start = gridLayout.WorldToCell(object_To_Be_Placed.Get_Position());
                Remove_Area(start, object_To_Be_Placed.Size);
                Destroy(find);
                Build_Object.set[tmp] = false;

            }

        }
    } 

    public void Remove_Area(Vector3Int start, Vector3Int size){
        tilemap.BoxFill(start, null, start.x, start.y, start.x + size.x, start.y + size.y);
    }
}
