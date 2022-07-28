using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/*
 * This class provides more actions than needed - good for adding features in future ... 
 * INPUT MAPPING:
 * 
 * Left Thumbstick:                         Moving the player
 * XRNode.Head (Quest2 Headset):            Rotating the player
 * Hold right Thumbstick (.0<x<=.3):        Show radial menu 
 * Trigger right Thumbstick (.3<=x):        Select menu action (select tool, material, etc.)
 * Press right Thumbstick down:             Step back in menu (disselect tool, etc.)
 * Hold right Grip:                         Grab object where rays point at
 * Hold right Grip & Press right Trigger:   Rotate Object
 * Press right Trigger:                     Action: Apply material, delete object, etc. (not place! this happens with thumbstick)
 * Press B (right sec. button):             Toggle Grid
 * 
 * TOOLS:   Place-Object-Tool (build, drag/rotate)      (1),
 *          Paint-Object-Tool (paint, drag/rotate)      (3), 
 *          Delete-Object-Tool (delete, drag/rotate)    (7), 
 *          Fist (Disselect tool/empty hand)            (5)
 */

public class ControllerInputManager : MonoBehaviour
{

    public InputDevice leftDevice;
    public InputDevice rightDevice;
    public List<InputDevice> leftDevices = new List<InputDevice>();
    public List<InputDevice> rightDevices = new List<InputDevice>();

    private bool leftTriggerPressed = false;
    private bool rightTriggerPressed = false;
    private bool leftGripPressed = false;
    private bool rightGripPressed = false;
    private bool leftTriggerHold = false;
    private bool rightTriggerHold = false;
    private bool leftGripHold = false;
    private bool rightGripHold = false;
    private bool leftPrimaryButtonPressed = false;
    private bool rightPrimaryButtonPressed = false;
    private bool leftSecondaryButtonPressed = false;
    private bool rightSecondaryButtonPressed = false;
    private bool leftPrimaryButtonHold = false;
    private bool rightPrimaryButtonHold = false;
    private bool leftSecondaryButtonHold = false;
    private bool rightSecondaryButtonHold = false;
    private bool rightThumbStickHold = false;
    private bool rightThumbStickTriggered = false;
    private bool rightThumbStickPressed = false;
    private int rightThumbStickDirection = 0;
    private int rightThumbStickLastDirection = 0;

    [SerializeField] private Controll_VR controlVR;

    // Update is called once per frame
    void Update()
    {
        
        // Initialize Devices
        if (leftDevices.Count < 1)
        {
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftDevices);
            leftDevice = leftDevices[0];
        }

        if (rightDevices.Count < 1)
        {
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightDevices);
            rightDevice = rightDevices[0];
        }

        // left trigger press
        leftDevice.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue);
        if (leftTriggerValue > 0.1f && !(leftTriggerPressed))
        {
            leftTriggerPressed = true;
        }
        if (leftTriggerValue <= 0.1f && leftTriggerPressed)
        {
            leftTriggerPressed = false;

            EnableScripts();
        }

        // left trigger hold
        if (leftTriggerValue > 0.1f)
        {
            leftTriggerHold = true;
        }else
        {
            leftTriggerHold = false;
        }

        // right trigger press
        rightDevice.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);
        if (rightTriggerValue > 0.1f && !(rightTriggerPressed))
        {
            rightTriggerPressed = true;
        }
        if (rightTriggerValue <= 0.1f && rightTriggerPressed)
        {
            rightTriggerPressed = false;

            EnableScripts();
        }

        // right trigger hold
        if (rightTriggerValue > 0.1f)
        {
            rightTriggerHold = true;
        }else
        {
            rightTriggerHold = false;
        }

        // left grip press
        leftDevice.TryGetFeatureValue(CommonUsages.grip, out float leftGripValue);
        if (leftGripValue > 0.1f && !(leftGripPressed))
        {
            leftGripPressed = true;
        }
        if (leftGripValue <= 0.1f && leftGripPressed)
        {
            leftGripPressed = false;

            EnableScripts();
        }

        // left grip hold
        if (leftGripValue > 0.1f)
        {
            leftGripHold = true;
        }else
        {
            leftGripHold = false;
        }

        // right grip press
        rightDevice.TryGetFeatureValue(CommonUsages.grip, out float rightGripValue);
        if (rightGripValue > 0.1f && !(rightGripPressed))
        {
            rightGripPressed = true;
        }
        if (rightGripValue <= 0.1f && rightGripPressed)
        {
            rightGripPressed = false;

            EnableScripts();
        }

        // right grip hold
        if (rightGripValue > 0.1f)
        {
            rightGripHold = true;
        }else
        {
            rightGripHold = false;
        }

        // left primary btn press
        leftDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonValue);
        if (leftPrimaryButtonValue && !(leftPrimaryButtonPressed))
        {
            leftPrimaryButtonPressed = true;
        }
        if (!(leftPrimaryButtonValue) && leftPrimaryButtonPressed)
        {
            leftPrimaryButtonPressed = false;

            EnableScripts();
        }

        // right primary btn press
        rightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue);
        if (rightPrimaryButtonValue && !(rightPrimaryButtonPressed))
        {
            rightPrimaryButtonPressed = true;
        }
        if (!(rightPrimaryButtonValue) && rightPrimaryButtonPressed)
        {
            rightPrimaryButtonPressed = false;

            EnableScripts();
        }

        // left secondary btn press
        leftDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool leftSecondaryButtonValue);
        if (leftSecondaryButtonValue && !(leftSecondaryButtonPressed))
        {
            leftSecondaryButtonPressed = true;
        }
        if (!(leftSecondaryButtonValue) && leftSecondaryButtonPressed)
        {
            leftSecondaryButtonPressed = false;

            EnableScripts();
        }

        // right secondary btn press
        rightDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rightSecondaryButtonValue);
        if (rightSecondaryButtonValue && !(rightSecondaryButtonPressed))
        {
            rightSecondaryButtonPressed = true;
        }
        if (!(rightSecondaryButtonValue) && rightSecondaryButtonPressed)
        {
            rightSecondaryButtonPressed = false;

            EnableScripts();
        }

        // left primary btn hold
        if (leftPrimaryButtonValue)
        {
            leftPrimaryButtonHold = true;
        }else
        {
            leftPrimaryButtonHold = false;
        }

        // right primary btn hold
        if (rightPrimaryButtonValue)
        {
            rightPrimaryButtonHold = true;
        }else
        {
            rightPrimaryButtonHold = false;
        }

        // left secondary btn hold
        if (leftSecondaryButtonValue)
        {
            leftSecondaryButtonHold = true;
        }else
        {
            leftSecondaryButtonHold = false;
        }

        // right secondary btn hold
        if (rightSecondaryButtonValue)
        {
            rightSecondaryButtonHold = true;
        }else
        {
            rightSecondaryButtonHold = false;
        }

        // right thumbstick 
        rightDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightPrimary2DAxisValue);
        if (rightPrimary2DAxisValue.x >= 0.3f || rightPrimary2DAxisValue.x <= -0.3f || rightPrimary2DAxisValue.y >= 0.3f || rightPrimary2DAxisValue.y <= -0.3f) 
        {
            rightThumbStickHold = true;
        }
        if (rightPrimary2DAxisValue.y > 0.8f)
        {
            // up = 1
            rightThumbStickTriggered = true;
            rightThumbStickDirection = 1;
        }
        if (rightPrimary2DAxisValue.y <= 0.8f && rightPrimary2DAxisValue.x >= 0.6f && rightPrimary2DAxisValue.y >= 0.6f && rightPrimary2DAxisValue.x <= 0.8f)
        {
            // up-right = 2
            rightThumbStickTriggered = true;
            rightThumbStickDirection = 2;
        }
        if (rightPrimary2DAxisValue.x > 0.8f)
        {
            // right = 3
            rightThumbStickTriggered = true;
            rightThumbStickDirection = 3;
        }
        if (rightPrimary2DAxisValue.y >= -0.8f && rightPrimary2DAxisValue.x >= 0.6f && rightPrimary2DAxisValue.y <= -0.6f && rightPrimary2DAxisValue.x <= 0.8f)
        {
            // down-right = 4
            rightThumbStickTriggered = true;
            rightThumbStickDirection = 4;
        }
        if (rightPrimary2DAxisValue.y < -0.8f)
        {
            // down = 5
            rightThumbStickTriggered = true;
            rightThumbStickDirection = 5;
        }
        if (rightPrimary2DAxisValue.y >= -0.8f && rightPrimary2DAxisValue.x <= -0.6f && rightPrimary2DAxisValue.y <= -0.6f && rightPrimary2DAxisValue.x >= -0.8f)
        {
            // down-left = 6
            rightThumbStickTriggered = true;
            rightThumbStickDirection = 6;
        }
        if (rightPrimary2DAxisValue.x < -0.8f)
        {
            // left = 7
            rightThumbStickTriggered = true;
            rightThumbStickDirection = 7;
        }
        if (rightPrimary2DAxisValue.y <= 0.8f && rightPrimary2DAxisValue.x <= -0.6f && rightPrimary2DAxisValue.y >= 0.6f && rightPrimary2DAxisValue.x >= -0.8f)
        {
            // up-left = 8
            rightThumbStickTriggered = true;
            rightThumbStickDirection = 8;
        }
        if (rightPrimary2DAxisValue.x < 0.3f && rightPrimary2DAxisValue.x > -0.3f && rightPrimary2DAxisValue.y < 0.3f && rightPrimary2DAxisValue.y > -0.3f)
        {
            if (rightThumbStickTriggered)
            {
                rightThumbStickLastDirection = rightThumbStickDirection;
                rightThumbStickTriggered = false;
            }
            rightThumbStickHold = false;
            rightThumbStickDirection = 0;
        }
        
        rightDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool rightPrimary2DAxisClick);
        if (rightPrimary2DAxisClick && !(rightThumbStickPressed))
        {
            rightThumbStickPressed = true;
        }
        if (!(rightPrimary2DAxisClick) && rightThumbStickPressed)
        {
            rightThumbStickPressed = false;

            EnableScripts();
        }

    }   

    // getter methods
    public bool IsLeftTriggerPressed()
    {
        return this.leftTriggerPressed;
    }

    public bool IsRightTriggerPressed()
    {
        return this.rightTriggerPressed;
    }

    public bool IsLeftGripPressed()
    {
        return this.leftGripPressed;
    }

    public bool IsRightGripPressed()
    {
        return this.rightGripPressed;
    }

    public bool IsLeftTriggerHold()
    {
        return this.leftTriggerHold;
    }

    public bool IsRightTriggerHold()
    {
        return this.rightTriggerHold;
    }

    public bool IsLeftGripHold()
    {
        return this.leftGripHold;
    }

    public bool IsRightGripHold()
    {
        return this.rightGripHold;
    }

    public bool IsLeftPrimaryButtonPressed()
    {
        return this.leftPrimaryButtonPressed;
    }

    public bool IsRightPrimaryButtonPressed()
    {
        return this.rightPrimaryButtonPressed;
    }

    public bool IsLeftSecondaryButtonPressed()
    {
        return this.leftSecondaryButtonPressed;
    }

    public bool IsRightSecondaryButtonPressed()
    {
        return this.rightSecondaryButtonPressed;
        Debug.Log("Methode");
    }

    public bool IsLeftPrimaryButtonHold()
    {
        return this.leftPrimaryButtonHold;
    }

    public bool IsRightPrimaryButtonHold()
    {
        return this.rightPrimaryButtonHold;
    }

    public bool IsLeftSecondaryButtonHold()
    {
        return this.leftSecondaryButtonHold;
    }

    public bool IsRightSecondaryButtonHold()
    {
        return this.rightSecondaryButtonHold;
    }

    public bool IsRightThumbStickHold()
    {
        return this.rightThumbStickHold;
    }

    public bool IsRightThumbStickTriggered()
    {
        return this.rightThumbStickTriggered;
    }

    public bool IsRightThumbStickPressed()
    {
        return this.rightThumbStickPressed;
    }

    public int GetRightThumbStickDirection()
    {
        return (int)this.rightThumbStickDirection;
    }
    
    public int GetRightThumbStickLastDirection()
    {
        return (int)this.rightThumbStickLastDirection;
    }

    // because of synchronisation issues we have to disable methods when their actions are triggered. When the button gets released, they get enabled again
    private void EnableScripts()
    {
        controlVR.enableScript();
    }

    public void disableScript()
    {
        this.enabled = false;
    }

    public void enableScript()
    {
        this.enabled = true;
    }

}
