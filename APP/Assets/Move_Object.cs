using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR.Interaction.Toolkit;

public class Move_Object : MonoBehaviour
{
    public GridLayout gridLayout;
    
    private Grid grid;
    private Placeable_Object object_To_Be_Placed;

    
    public Material[] mat;
    private Material[] prev_mat;
    public AudioSource Wrong;
    
    private int rotation;

    
    private Placeable_Object object_To_Be_Deleted;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tilebase;
    public static bool mylock;
    public Material[] material;
    Renderer rend;
    
    Renderer rend2;
    private bool delay_done;
    private bool delay_before_done;
    public static bool canMove;
    public static bool Move_Pressed;
    public static bool Change_Rotation_Left;
    public static bool Change_Rotation_Right;
    public static bool Pressed_To_Destroy;

    Vector3 pos;
    Vector3 hiding_pos;

    private Vector3Int Size;

    public static bool canRotate =true;
    private bool canDo = true;
    
    Vector3Int start;
    GameObject find;
    string mytag;

    private static XRRayInteractor interactor;
    [SerializeField] ControllerInputManager inputManager;

    void Start()
    {
        mylock = true;
        rend = gameObject.GetComponent<Renderer>();
        rend.enabled = true;
        delay_done = false;
        delay_before_done = false;
        canMove = false;
        Move_Pressed = false;
        Change_Rotation_Left = false;
        Change_Rotation_Right = false;
        Pressed_To_Destroy = false;

        interactor = GameObject.FindGameObjectWithTag("GameController").GetComponent<XRRayInteractor>();
    }


     void Update()
     {
        if (mylock){
                return;
        }
        rend.sharedMaterial = material[0];
         
            
        if(canMove && Move_Pressed){
            Move_Pressed = false;
            
            if(interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                if(hit.transform.CompareTag("No")) return;
                
                Controll_Grid.mylock = true;
                canMove = false;
                Change_Mode.canReturn = false;
                delay_before_done = true;
                canRotate = true;
                find= GameObject.FindGameObjectWithTag(hit.transform.tag);
                pos  = hit.transform.position;
                hiding_pos = new Vector3(pos.x,pos.y-30,pos.z);
                mytag = hit.transform.tag;
                object_To_Be_Deleted = find.GetComponent<Placeable_Object>();

                rotation = object_To_Be_Deleted.Rotation.y;

                start = gridLayout.WorldToCell(object_To_Be_Deleted.Get_Position());
                Size = find.GetComponent<Placeable_Object>().Size;

                Take_Area(start, Size, null);
                find.transform.position = hiding_pos;

                Initialize_With_Object(find);                
                
            }
        }

        if(canMove){
            return;
        }

        

        if (canRotate && delay_done && (rotation != 0)){
            int value = (int)(rotation % 360)/90;
            Debug.Log(rotation + " Rotation; Mit "+ value);
            for (int i=0; i<value; i++){
                object_To_Be_Placed.Rotate();
            }
            rotation = 0;
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
        else if (Move_Pressed&&delay_done){
            Move_Pressed = false;
            if(Can_Be_Placed(object_To_Be_Placed)){
                canMove = true;
                delay_done = false;
                canRotate = false;
                Controll_Grid.mylock = false;
                
                Destroy(find);
                
                Vector3Int start2 = gridLayout.WorldToCell(object_To_Be_Placed.Get_Position());
                Take_Area(start2, object_To_Be_Placed.Size, tilebase);
                
                object_To_Be_Placed.tag = mytag;
                object_To_Be_Placed.Place();
                int z = 0;
                foreach (Transform my_object in object_To_Be_Placed.gameObject.transform)
                {
                    foreach (Transform child in my_object.transform)
                    {
                        rend2 = child.gameObject.GetComponent<Renderer>();
                        rend2.enabled = true;
                        rend2.sharedMaterial = prev_mat[z];
                        z++;  
                    }
                }

                Change_Mode.canReturn = true;
            }else{
                PlayWrong();
            }
        }else if(delay_done && Can_Be_Placed(object_To_Be_Placed) ){
            //Debug.Log("Canbeplaced________________________");
            
            foreach (Transform my_object in object_To_Be_Placed.gameObject.transform)
            {
                foreach (Transform child in my_object.transform)
                {
                    rend2 = child.gameObject.GetComponent<Renderer>();
                    rend2.enabled = true;
                    rend2.sharedMaterial = mat[0];  
                }
            }
        }else if (delay_done){
            foreach (Transform my_object in object_To_Be_Placed.gameObject.transform)
                    {
                        foreach (Transform child in my_object.transform)
                        {
                            rend2 = child.gameObject.GetComponent<Renderer>();
                            rend2.enabled = true;
                            rend2.sharedMaterial = mat[1];
                            
                        }
                    }
        }
        if (delay_done && Pressed_To_Destroy){
            Pressed_To_Destroy = false;
            canMove = true;
                delay_done = false;
                canRotate = false;
                Controll_Grid.mylock = false;
            Destroy(object_To_Be_Placed.gameObject);
            find.transform.position = pos;
            
                Change_Mode.canReturn = true;
                Take_Area(start, Size, tilebase);
        }
        

        if (delay_before_done){
            delay_done = true;
            delay_before_done = false;
        }
        
        
     } 

     public void PlayWrong(){
        Wrong.Play();
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

    Renderer rend3;
    public void Initialize_With_Object(GameObject prefab){

        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        object_To_Be_Placed = obj.GetComponent<Placeable_Object>();
        
        prev_mat = new Material[object_To_Be_Placed.material.Length];
        int z = 0;
        foreach (Transform my_object in object_To_Be_Placed.gameObject.transform)
        {
            foreach (Transform child in my_object.transform)
            {
                rend3 = child.gameObject.GetComponent<Renderer>();
                prev_mat[z] = rend3.sharedMaterial;
                Debug.Log(prev_mat[z]);
                z++;  
            }
        }

        Drag_Object d_o = obj.AddComponent<Drag_Object>();
        d_o.inputManager = inputManager;
        d_o.setActiveObj(true);

    }

    public void Take_Area(Vector3Int start, Vector3Int size, TileBase tb){
        tilemap.BoxFill(start, tb, start.x, start.y, start.x + size.x, start.y + size.y);
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

    
}
