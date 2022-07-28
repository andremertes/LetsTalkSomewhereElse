using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(ActionBasedController))]
public class Controll_VR : MonoBehaviour
{
    [SerializeField] ActionBasedController l_ActionBasedController;
    [SerializeField] ActionBasedController r_ActionBasedController;
    [SerializeField] Animator leftAnimator;
    [SerializeField] Animator rightAnimator;
    public  float speed;
    private string animatorGripParameter = "Grip";
    private string animatorTriggerParameter = "Trigger";

    [SerializeField] private XRInteractorLineVisual lineVisual;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    [SerializeField] private ControllerInputManager inputManager;
    [SerializeField] private GameObject tablet; // gives a visual feedback to player input

    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject mode0;
    [SerializeField] private GameObject mode1;
    [SerializeField] private GameObject mode2;
    [SerializeField] private GameObject mode3;

    [SerializeField] private GameObject buildCategories;
    [SerializeField] private GameObject seatsBtn;
    [SerializeField] private GameObject lampsBtn;
    [SerializeField] private GameObject racksBtn;
    [SerializeField] private GameObject tablesBtn;

    [SerializeField] private GameObject colors;
    [SerializeField] private GameObject clothBtn;
    [SerializeField] private GameObject woodBtn;
    [SerializeField] private GameObject colorsBtn;

    [SerializeField] private GameObject categorySeats;
    [SerializeField] private GameObject seat1;
    [SerializeField] private GameObject seat2;
    [SerializeField] private GameObject seat3;
    [SerializeField] private GameObject seat4;
    [SerializeField] private GameObject seat5;
    [SerializeField] private GameObject seat6;
    [SerializeField] private GameObject seat7;
    [SerializeField] private GameObject seat8;

    [SerializeField] private GameObject categoryLamps;
    [SerializeField] private GameObject lamp1;
    [SerializeField] private GameObject lamp2;
    [SerializeField] private GameObject lamp3;
    [SerializeField] private GameObject lamp4;
    [SerializeField] private GameObject lamp5;
    [SerializeField] private GameObject lamp6;
    [SerializeField] private GameObject lamp7;
    [SerializeField] private GameObject lamp8;

    [SerializeField] private GameObject categoryRacks;
    [SerializeField] private GameObject rack1;
    [SerializeField] private GameObject rack2;
    [SerializeField] private GameObject rack3;
    [SerializeField] private GameObject rack4;
    [SerializeField] private GameObject rack5;
    [SerializeField] private GameObject rack6;
    [SerializeField] private GameObject rack7;
    [SerializeField] private GameObject rack8;

    [SerializeField] private GameObject categoryTables;
    [SerializeField] private GameObject table1;
    [SerializeField] private GameObject table2;
    [SerializeField] private GameObject table3;
    [SerializeField] private GameObject table4;
    [SerializeField] private GameObject table5;
    [SerializeField] private GameObject table6;
    [SerializeField] private GameObject table7;
    [SerializeField] private GameObject table8;

    [SerializeField] private GameObject categoryCloth;
    [SerializeField] private GameObject cloth1;
    [SerializeField] private GameObject cloth2;
    [SerializeField] private GameObject cloth3;
    [SerializeField] private GameObject cloth4;
    [SerializeField] private GameObject cloth5;
    [SerializeField] private GameObject cloth6;
    [SerializeField] private GameObject cloth7;
    [SerializeField] private GameObject cloth8;

    [SerializeField] private GameObject categoryWood;
    [SerializeField] private GameObject wood1;
    [SerializeField] private GameObject wood2;
    [SerializeField] private GameObject wood3;
    [SerializeField] private GameObject wood4;
    [SerializeField] private GameObject wood5;
    [SerializeField] private GameObject wood6;
    [SerializeField] private GameObject wood7;
    [SerializeField] private GameObject wood8;

    [SerializeField] private GameObject categoryColors;
    [SerializeField] private GameObject color1;
    [SerializeField] private GameObject color2;
    [SerializeField] private GameObject color3;
    [SerializeField] private GameObject color4;
    [SerializeField] private GameObject color5;
    [SerializeField] private GameObject color6;
    [SerializeField] private GameObject color7;
    [SerializeField] private GameObject color8;


    private bool isMenusSelected = true;
    private bool isBuildSelected = false;
    private bool isPaintSelected = false;
    private bool isSeatsSelected = false;
    private bool isLampsSelected = false;
    private bool isRacksSelected = false;
    private bool isTableSelected = false;
    private bool isClothSelected = false;
    private bool isWoodsSelected = false;
    private bool isColorSelected = false;

    private float leftGripTarget;
    private float rightGripTarget;
    private float leftTriggerTarget;
    private float rightTriggerTarget;
    private float leftGripCurrent;
    private float rightGripCurrent;
    private float leftTriggerCurrent;
    private float rightTriggerCurrent;

    private void Start()
    {
        menuButtons.SetActive(true);

        buildCategories.SetActive(false);
        colors.SetActive(false);

        categorySeats.SetActive(false);
        categoryLamps.SetActive(false);
        categoryRacks.SetActive(false);
        categoryTables.SetActive(false);

        categoryCloth.SetActive(false);
        categoryWood.SetActive(false);
        categoryColors.SetActive(false);

        lineVisual.lineLength = 0;
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        Change_Mode.mode = -1;
    }

    void Update()
    {
        SetGripL(l_ActionBasedController.selectAction.action.ReadValue<float>());
        SetGripR(r_ActionBasedController.selectAction.action.ReadValue<float>());
        SetTriggerL(l_ActionBasedController.activateAction.action.ReadValue<float>());
        SetTriggerR(r_ActionBasedController.activateAction.action.ReadValue<float>());

        AnimateLeftHand();
        AnimateRightHand();

        if (inputManager.IsRightThumbStickHold() || inputManager.IsRightThumbStickTriggered() || inputManager.IsRightThumbStickPressed())
        {
            tablet.SetActive(true);
            lineVisual.lineLength = 0;
            leftHand.SetActive(true);
            rightHand.SetActive(false);
        } else
        {
            tablet.SetActive(false);

            if (Change_Mode.mode != 0 && Change_Mode.mode != 1 && Change_Mode.mode != 2 && Change_Mode.mode != 3)
            {
                lineVisual.lineLength = 0;
                leftHand.SetActive(true);
                rightHand.SetActive(true);
            } else
            {
                lineVisual.lineLength = 10;
                leftHand.SetActive(true);
                rightHand.SetActive(false);
            }

            Debug.Log(Change_Mode.mode);

            if (isMenusSelected)
            {
                menuButtons.SetActive(true);
                buildCategories.SetActive(false);
                colors.SetActive(false);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(false);
                categoryColors.SetActive(false);
            }
            else if (isBuildSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(true);
                colors.SetActive(false);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(false);
                categoryColors.SetActive(false);
            }
            else if (isPaintSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(false);
                colors.SetActive(true);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(false);
                categoryColors.SetActive(false);
            }
            else if (isSeatsSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(false);
                colors.SetActive(false);
                categorySeats.SetActive(true);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(false);
                categoryColors.SetActive(false);
            }
            else if (isLampsSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(false);
                colors.SetActive(false);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(true);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(false);
                categoryColors.SetActive(false);
            }
            else if (isRacksSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(false);
                colors.SetActive(false);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(true);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(false);
                categoryColors.SetActive(false);
            }
            else if (isTableSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(false);
                colors.SetActive(false);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(true);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(false);
                categoryColors.SetActive(false);
            }
            else if (isClothSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(false);
                colors.SetActive(false);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(true);
                categoryWood.SetActive(false);
                categoryColors.SetActive(false);
            }
            else if (isWoodsSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(false);
                colors.SetActive(false);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(true);
                categoryColors.SetActive(false);
            }
            else if (isColorSelected)
            {
                menuButtons.SetActive(false);
                buildCategories.SetActive(false);
                colors.SetActive(false);
                categorySeats.SetActive(false);
                categoryLamps.SetActive(false);
                categoryRacks.SetActive(false);
                categoryTables.SetActive(false);
                categoryCloth.SetActive(false);
                categoryWood.SetActive(false);
                categoryColors.SetActive(true);
            }

        }

        if (inputManager.IsRightThumbStickTriggered())
        {
            if (menuButtons.activeSelf)
            {
                if ((Change_Mode.mode == 3) && !(Move_Object.mylock))
                {
                    Move_Object.Pressed_To_Destroy = true;
                }

                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    mode0.transform.localScale = new Vector3(.1f, .021f, .1f);
                    mode1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    //Change_Mode.mode = 0;
                    //Change_Mode.Change_Mode_Pressed = true;
                    isMenusSelected = false;
                    isBuildSelected = true;
                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    mode0.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode1.transform.localScale = new Vector3(.1f, .021f, .1f);
                    mode2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    //Change_Mode.mode = 1;
                    //Change_Mode.Change_Mode_Pressed = true;
                    isMenusSelected = false;
                    isPaintSelected = true;
                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    mode0.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode2.transform.localScale = new Vector3(.1f, .021f, .1f);
                    mode3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Mode.mode = 2;
                    Change_Mode.Change_Mode_Pressed = true;
                    // trigger delete function
                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    mode0.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode3.transform.localScale = new Vector3(.1f, .021f, .1f);
                    Change_Mode.mode = 3;
                    Change_Mode.Change_Mode_Pressed = true;
                    // trigger move function
                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    mode0.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    mode3.transform.localScale = new Vector3(.1f, .009f, .1f);
                }
            }

            if (buildCategories.activeSelf)
            {
                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    seatsBtn.transform.localScale = new Vector3(.1f, .021f, .1f);
                    lampsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    racksBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    tablesBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    isBuildSelected = false;
                    isSeatsSelected = true;
                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    seatsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lampsBtn.transform.localScale = new Vector3(.1f, .021f, .1f);
                    racksBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    tablesBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    isBuildSelected = false;
                    isLampsSelected = true;
                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    seatsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lampsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    racksBtn.transform.localScale = new Vector3(.1f, .021f, .1f);
                    tablesBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    isBuildSelected = false;
                    isRacksSelected = true;
                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    seatsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lampsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    racksBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    tablesBtn.transform.localScale = new Vector3(.1f, .021f, .1f);
                    isBuildSelected = false;
                    isTableSelected = true;
                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    seatsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lampsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    racksBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    tablesBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                }
                
            } 
            else if (colors.activeSelf)
            {

                if (inputManager.GetRightThumbStickDirection() == 1) {
                    clothBtn.transform.localScale = new Vector3(.1f, .021f, .1f);
                    woodBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    colorsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    isPaintSelected = false;
                    isClothSelected = true;
                } 
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    clothBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    woodBtn.transform.localScale = new Vector3(.1f, .021f, .1f);
                    colorsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    isPaintSelected = false;
                    isWoodsSelected = true;
                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    clothBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    woodBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    colorsBtn.transform.localScale = new Vector3(.1f, .021f, .1f);
                    isPaintSelected = false;
                    isColorSelected = true;
                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    clothBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    woodBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                    colorsBtn.transform.localScale = new Vector3(.1f, .009f, .1f);
                }

            } 
            else if (categorySeats.activeSelf)
            {

                if (!(Build_Object.mylock) && !Change_Mode.canReturn)
                {
                    Build_Object.Pressed_To_Destroy = true;
                }

                Change_Mode.mode = 0;
                Change_Mode.Change_Mode_Pressed = true;

                // <--
                
                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    seat1.transform.localScale = new Vector3(.1f, .021f, .1f);
                    seat3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 1;

                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    seat1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat3.transform.localScale = new Vector3(.1f, .021f, .1f);
                    seat5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 6;

                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    seat1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat5.transform.localScale = new Vector3(.1f, .021f, .1f);
                    seat7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 0;
                    
                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    seat1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat7.transform.localScale = new Vector3(.1f, .021f, .1f);
                    Build_Object.Optionindex = 5;
                    
                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    seat1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    seat7.transform.localScale = new Vector3(.1f, .009f, .1f);
                }

                // <--
                
            }
            else if (categoryLamps.activeSelf)
            {

                if (!(Build_Object.mylock) && !Change_Mode.canReturn)
                {
                    Build_Object.Pressed_To_Destroy = true;
                }

                Change_Mode.mode = 0;
                Change_Mode.Change_Mode_Pressed = true;

                // <--
                
                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    lamp1.transform.localScale = new Vector3(.1f, .021f, .1f);
                    lamp3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 3;

                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    lamp1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp3.transform.localScale = new Vector3(.1f, .021f, .1f);
                    lamp5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 8;

                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    lamp1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp5.transform.localScale = new Vector3(.1f, .021f, .1f);
                    lamp7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 11;

                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    lamp1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp7.transform.localScale = new Vector3(.1f, .021f, .1f);
                    Build_Object.Optionindex = 12;

                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    lamp1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    lamp7.transform.localScale = new Vector3(.1f, .009f, .1f);
                }

                // <--

            }
            else if (categoryRacks.activeSelf)
            {

                if (!(Build_Object.mylock) && !Change_Mode.canReturn)
                {
                    Build_Object.Pressed_To_Destroy = true;
                }

                Change_Mode.mode = 0;
                Change_Mode.Change_Mode_Pressed = true;

                // <--

                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    rack1.transform.localScale = new Vector3(.1f, .021f, .1f);
                    rack3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 4;

                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    rack1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack3.transform.localScale = new Vector3(.1f, .021f, .1f);
                    rack5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 13;

                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    rack1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack5.transform.localScale = new Vector3(.1f, .021f, .1f);
                    rack7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 14;

                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    rack1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack7.transform.localScale = new Vector3(.1f, .021f, .1f);
                    Build_Object.Optionindex = 15;

                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    rack1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    rack7.transform.localScale = new Vector3(.1f, .009f, .1f);
                }

                // <--

            }
            else if (categoryTables.activeSelf)
            {

                if (!(Build_Object.mylock) && !Change_Mode.canReturn)
                {
                    Build_Object.Pressed_To_Destroy = true;
                }

                Change_Mode.mode = 0;
                Change_Mode.Change_Mode_Pressed = true;

                // <--

                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    table1.transform.localScale = new Vector3(.1f, .021f, .1f);
                    table3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 2;

                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    table1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table3.transform.localScale = new Vector3(.1f, .021f, .1f);
                    table5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 7;

                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    table1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table5.transform.localScale = new Vector3(.1f, .021f, .1f);
                    table7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Build_Object.Optionindex = 9;

                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    table1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table7.transform.localScale = new Vector3(.1f, .021f, .1f);
                    Build_Object.Optionindex = 10;

                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    table1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    table7.transform.localScale = new Vector3(.1f, .009f, .1f);
                }

                // <--

            }
            else if (categoryCloth.activeSelf)
            {

                Change_Mode.mode = 1;
                Change_Mode.Change_Mode_Pressed = true;

                // <--

                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .021f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 0;  // 0,1,13,2,14,16,8,9

                }
                else if (inputManager.GetRightThumbStickDirection() == 2)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .021f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 1;

                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .021f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 13;

                }
                else if (inputManager.GetRightThumbStickDirection() == 4)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .021f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 2;

                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .021f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 14;

                }
                else if (inputManager.GetRightThumbStickDirection() == 6)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .021f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 15;

                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .021f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 8;

                }
                else if (inputManager.GetRightThumbStickDirection() == 8)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .021f, .1f);
                    Change_Material.index = 9;

                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    cloth1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    cloth8.transform.localScale = new Vector3(.1f, .009f, .1f);
                }

                // <--

            }
            else if (categoryWood.activeSelf)
            {

                Change_Mode.mode = 1;
                Change_Mode.Change_Mode_Pressed = true;

                // <--

                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    wood1.transform.localScale = new Vector3(.1f, .021f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 16;  

                }
                else if (inputManager.GetRightThumbStickDirection() == 2)
                {
                    wood1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .021f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 4;

                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    wood1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .021f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 5;

                }
                else if (inputManager.GetRightThumbStickDirection() == 4)
                {
                    wood1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .021f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 17;

                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    wood1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .021f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 18;

                }
                else if (inputManager.GetRightThumbStickDirection() == 6)
                {
                    wood1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .021f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 19;

                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    wood1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .021f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 20;

                }
                else if (inputManager.GetRightThumbStickDirection() == 8)
                {
                    wood1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .021f, .1f);
                    Change_Material.index = 3;

                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    wood1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    wood8.transform.localScale = new Vector3(.1f, .009f, .1f);
                }

                // <--

            }
            else if (categoryColors.activeSelf)
            {

                Change_Mode.mode = 1;
                Change_Mode.Change_Mode_Pressed = true;

                // <--

                if (inputManager.GetRightThumbStickDirection() == 1)
                {
                    color1.transform.localScale = new Vector3(.1f, .021f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 6;

                }
                else if (inputManager.GetRightThumbStickDirection() == 2)
                {
                    color1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .021f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 7;

                }
                else if (inputManager.GetRightThumbStickDirection() == 3)
                {
                    color1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .021f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 10;

                }
                else if (inputManager.GetRightThumbStickDirection() == 4)
                {
                    color1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .021f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 11;

                }
                else if (inputManager.GetRightThumbStickDirection() == 5)
                {
                    color1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .021f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 12;

                }
                else if (inputManager.GetRightThumbStickDirection() == 6)
                {
                    color1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .021f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 21;

                }
                else if (inputManager.GetRightThumbStickDirection() == 7)
                {
                    color1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .021f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .009f, .1f);
                    Change_Material.index = 22;

                }
                else if (inputManager.GetRightThumbStickDirection() == 8)
                {
                    color1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .021f, .1f);
                    Change_Material.index = 23;

                }
                else if (inputManager.GetRightThumbStickDirection() == 0)
                {
                    color1.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color2.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color3.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color4.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color5.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color6.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color7.transform.localScale = new Vector3(.1f, .009f, .1f);
                    color8.transform.localScale = new Vector3(.1f, .009f, .1f);
                }

                // <--

            }

        }

        if (inputManager.IsRightThumbStickPressed())
        {
            if (isBuildSelected || isPaintSelected)
            {
                isMenusSelected = true;
                isBuildSelected = false;
                isPaintSelected = false;
            } else if (isSeatsSelected || isLampsSelected || isRacksSelected || isTableSelected)
            {
                isSeatsSelected = false;
                isLampsSelected = false;
                isRacksSelected = false;
                isTableSelected = false;
                isBuildSelected = true;
            } else if (isClothSelected || isWoodsSelected || isColorSelected)
            {
                isClothSelected = false;
                isWoodsSelected = false;
                isColorSelected = false;
                isPaintSelected = true;
            }

            Build_Object.Pressed_To_Destroy = true;
            Move_Object.Pressed_To_Destroy = true;

            if (Change_Mode.canReturn)
            {
                Change_Mode.Escape_Pressed = true;
            }

            Change_Mode.mode = -1;

        }



        if (inputManager.IsRightPrimaryButtonPressed()){ //Y
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
            
            enabled = false;

        }
        else if (inputManager.IsLeftTriggerPressed())
        { //X
            if ((Change_Mode.mode == 0) && !(Build_Object.mylock) && Build_Object.canRotate)
            {
                Build_Object.Change_Rotation_Left = true;
            }
            else if ((Change_Mode.mode == 3) && !(Move_Object.mylock) && Move_Object.canRotate)
            {
                Move_Object.Change_Rotation_Left = true;
            }

            enabled = false;
        }
        else if (inputManager.IsRightTriggerPressed())
        { //C
            if ((Change_Mode.mode == 0) && !(Build_Object.mylock) && Build_Object.canRotate)
            {
                Build_Object.Change_Rotation_Right = true;
            }
            else if ((Change_Mode.mode == 3) && !(Move_Object.mylock) && Move_Object.canRotate)
            {
                Move_Object.Change_Rotation_Right = true;
            }

            enabled = false;

        }
        else if (inputManager.IsRightSecondaryButtonPressed()){ // G
            if (!Controll_Grid.mylock){
                Controll_Grid.Grid_Pressed = true;
            }

            enabled = false;

        }
        
    }

    private void SetGripL(float v)
    {
        leftGripTarget = v;
    }

    private void SetGripR(float v)
    {
        rightGripTarget = v;
    }

    private void SetTriggerL(float v)
    {
        leftTriggerTarget = v;
    }

    private void SetTriggerR(float v)
    {
        rightTriggerTarget = v;
    }

    void AnimateLeftHand()
    {
        if (leftGripCurrent != leftGripTarget) {
            leftGripCurrent = Mathf.MoveTowards(leftGripCurrent, leftGripTarget, Time.deltaTime * speed);
            leftAnimator.SetFloat(animatorGripParameter, leftGripCurrent);
        } 
        if (leftTriggerCurrent != leftTriggerTarget)
        {
            leftTriggerCurrent = Mathf.MoveTowards(leftTriggerCurrent, leftTriggerTarget, Time.deltaTime * speed);
            leftAnimator.SetFloat(animatorTriggerParameter, leftTriggerCurrent);
        }
    }

    void AnimateRightHand()
    {
        if (rightGripCurrent != rightGripTarget)
        {
            rightGripCurrent = Mathf.MoveTowards(rightGripCurrent, rightGripTarget, Time.deltaTime * speed);
            rightAnimator.SetFloat(animatorGripParameter, rightGripCurrent);
        }
        if (rightTriggerCurrent != rightTriggerTarget)
        {
            rightTriggerCurrent = Mathf.MoveTowards(rightTriggerCurrent, rightTriggerTarget, Time.deltaTime * speed);
            rightAnimator.SetFloat(animatorTriggerParameter, rightTriggerCurrent);
        }
    }

    public void enableScript()
    {
        enabled = true;
    }

}

