using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Template : MonoBehaviour
{

    public GameObject[] prefabs;
    public int[] xpos;
    public int[] zpos;
    public Material[] mat;
    public int[] rotation_right;
    
    public static Template current;

    private Placeable_Object object_To_Be_Placed;

     Renderer rend;
     public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tilebase;
    int i = 0;
    bool delay_done = false;

    private void Awake(){
        current  = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    

    private void Update(){
        
        if ((prefabs.Length != xpos.Length)||(prefabs.Length != zpos.Length)||(prefabs.Length != mat.Length)||(prefabs.Length != rotation_right.Length)||(prefabs.Length > Build_Object.set.Length)){
            return;
        }
        if(i< prefabs.Length){
            if (!delay_done){
                Initialize_With_Object(prefabs[i],xpos[i],zpos[i]); 
                delay_done = true;
            }else{
            for (int j = 0; j<rotation_right[i]; j++){
                object_To_Be_Placed.Rotate();
            }
            
            foreach (Transform my_object in object_To_Be_Placed.gameObject.transform)
            {
                foreach (Transform child in my_object.transform)
                {
                    rend= child.gameObject.GetComponent<Renderer>();
                    rend.enabled = true;
                    if(child.gameObject.CompareTag("Yes")){
                        rend.sharedMaterial = mat[i];
                    }
                }
            }

            for (int k = 0; k< Build_Object.set.Length;k++){
                    if (!(Build_Object.set[k])){
                        object_To_Be_Placed.tag = "tag" + k;
                        Build_Object.set[k] = true;
                        break; // überarbeitet
                    }   

            }

            object_To_Be_Placed.Place();
            Vector3Int start = gridLayout.WorldToCell(object_To_Be_Placed.Get_Position());
            Take_Area(start, object_To_Be_Placed.Size);
            i++;
            delay_done = false;
            }
            
        }
    }

    

    public void Initialize_With_Object(GameObject prefab, int x, int z){
        
        //Vector3 position = Snap_To_Grid(Vector3.zero);
        Vector3 pos = new Vector3(x, 0 , z);
        Vector3 position = Snap_To_Grid(pos);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        object_To_Be_Placed = obj.GetComponent<Placeable_Object>();
        
    }

    public Vector3 Snap_To_Grid(Vector3 position){
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    public void Take_Area(Vector3Int start, Vector3Int size){
        tilemap.BoxFill(start, tilebase, start.x, start.y, start.x + size.x, start.y + size.y);
    }
}
