using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR.Interaction.Toolkit;

public class Build_Object : MonoBehaviour
{
    public static bool canSpawn;
    public static bool canRotate;
    public static bool[] set = new bool[30];
    public static Build_Object current;
    public static int Optionindex;
    private bool delay_done;
    private bool delay_before_done;
    public static bool mylock;
    public static bool Change_Rotation_Left;
    public static bool Change_Rotation_Right;
    public static bool Pressed_To_Place;
    public static bool Pressed_To_Destroy;

    public Material[] mat;
    public AudioSource Wrong;

    Renderer rend;
    Renderer rend2;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tilebase;
    public static int Max_Options;

    public GameObject[] prefabs;

    private Placeable_Object object_To_Be_Placed;

    [SerializeField] ControllerInputManager inputManager;
    private static XRRayInteractor interactor;
    private int i;

    private void Start(){
        mylock = true;
        Max_Options = prefabs.Length;
        Optionindex = 0;
        
        for (int j= 0; j<set.Length; j++){
            set[j] = false;
        }
        canSpawn = false;
        delay_done = false;
        delay_before_done = false;
        Change_Rotation_Left = false;
        Change_Rotation_Right = false;
        Pressed_To_Place = false;
        Pressed_To_Destroy = false;

        interactor = GameObject.FindGameObjectWithTag("GameController").GetComponent<XRRayInteractor>();
        i = 0;
    }

    private void Awake(){
        current  = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update(){

        if (mylock){
                return;
        }

        bool found = false;

        if(canSpawn && Pressed_To_Place){
            Pressed_To_Place = false;
            for (i = 0; i < set.Length; i++)
            {
                if (!(set[i]))
                {
                    found = true;
                    break;
                }

            }
            if (!found)
            {
                return;
            }
            Change_Mode.canReturn = false;
            canSpawn = false;
            canRotate = true;
            Change_Object_To_Place.mylock = true;
            Initialize_With_Object(prefabs[Optionindex]);
            delay_before_done = true; 
            Controll_Grid.mylock = true;
        }


        if (canRotate && Change_Rotation_Left && delay_done)
        {
            Change_Rotation_Left = false;
            object_To_Be_Placed.Rotate();
            object_To_Be_Placed.Rotate();
            object_To_Be_Placed.Rotate();
        }
        else
        if (canRotate && Change_Rotation_Right && delay_done)
        {
            Change_Rotation_Right = false;
            object_To_Be_Placed.Rotate();
        }
        else if (delay_done && Pressed_To_Place){
            Pressed_To_Place = false;
            
            if(Can_Be_Placed(object_To_Be_Placed)){
                delay_done = false;
                canRotate = false;
                canSpawn = true;
                Change_Object_To_Place.mylock = false;
                Controll_Grid.mylock = false;;
                object_To_Be_Placed.Place();
                Vector3Int start = gridLayout.WorldToCell(object_To_Be_Placed.Get_Position());
                Take_Area(start, object_To_Be_Placed.Size);
                
                object_To_Be_Placed.tag = "tag" + i;
                set[i] = true;
                        
                int z = 0;
                foreach (Transform my_object in object_To_Be_Placed.gameObject.transform)
                {
                    foreach (Transform child in my_object.transform)
                    {
                        rend = child.gameObject.GetComponent<Renderer>();
                        rend.enabled = true;
                        rend.sharedMaterial = object_To_Be_Placed.material[z];
                        z++;  
                    }
                }
                Change_Mode.canReturn = true; // test if needed
            }else{
                PlayWrong();
            }
            
            
        }
        else if(delay_done && Can_Be_Placed(object_To_Be_Placed) ){
            //Debug.Log("Canbeplaced________________________");
            
            foreach (Transform my_object in object_To_Be_Placed.gameObject.transform)
            {
                foreach (Transform child in my_object.transform)
                {
                    rend = child.gameObject.GetComponent<Renderer>();
                    rend.enabled = true;
                    rend.sharedMaterial = mat[0];  
                }
            }
        }else if (delay_done){
            foreach (Transform my_object in object_To_Be_Placed.gameObject.transform)
                    {
                        foreach (Transform child in my_object.transform)
                        {
                            rend = child.gameObject.GetComponent<Renderer>();
                            rend.enabled = true;
                            rend.sharedMaterial = mat[1];
                            
                        }
                    }
        }

        
        
        if(delay_done && Pressed_To_Destroy&&(!object_To_Be_Placed.Placed) ){
            Pressed_To_Destroy = false;
            canRotate = false;
            canSpawn = true;
            Change_Object_To_Place.mylock = false;
            Destroy(object_To_Be_Placed.gameObject);
            
            Change_Mode.canReturn = true;
            delay_done = false;
            Controll_Grid.mylock = false;
        }
        

        if (delay_before_done){
            delay_done = true;
            delay_before_done = false;
        }

        
    }

    public void PlayWrong(){
        Wrong.Play();
    }

    public static Vector3 Get_Contr_Position()
    {

        if (interactor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget)) {
            Debug.Log(position);
            return position;
        } else
        {

        }

        return Vector3.zero;

    }

    public Vector3 Snap_To_Grid(Vector3 position){
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] Get_Tiles(BoundsInt area, Tilemap tilemap){
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin){
            Vector3Int pos = new Vector3Int(v.x,v.y,0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }



    

   

    public void Initialize_With_Object(GameObject prefab){
        Vector3 position = Snap_To_Grid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        object_To_Be_Placed = obj.GetComponent<Placeable_Object>();
        
        Drag_Object d_o = obj.GetComponent<Drag_Object>();
        d_o.inputManager = inputManager;
        d_o.setActiveObj(true);
    }

    private bool Can_Be_Placed(Placeable_Object object_To_Be_Placed){
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(object_To_Be_Placed.Get_Position());
        area.size = object_To_Be_Placed.Size;
        area.size = new Vector3Int(area.size.x+1,area.size.y+1,area.size.z);

        TileBase[] baseArray = Get_Tiles(area, tilemap);

        foreach(var b in baseArray){
            if(b==tilebase){
                return false;
            }
        }

        return true;
    }

    public void Take_Area(Vector3Int start, Vector3Int size){
        tilemap.BoxFill(start, tilebase, start.x, start.y, start.x + size.x, start.y + size.y);
    }

    

    

}
