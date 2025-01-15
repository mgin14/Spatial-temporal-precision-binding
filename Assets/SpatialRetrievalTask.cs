/* Created by Melanie Gin 12/2024
 * 
 * 
 * This script will be the spatial retrieval task where the subject is placed in the hall way 
 * and will have to click on the screen where they saw the object.
 * Only the z and y values will be used for calculation errors because we don't care where on the x-axis
 * (aka how far in the hallway) they viewed the object. Just the height and if it was close to the wall or not.
 */
 



using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SpatialRetrievalTask : ExperimentTask
{
    private NavigationTask cur_tar;
    public GameObject item;
    public Vector3 itemLocation;
    private int tempTrial; // this is just for output
    private bool mainLoopCurrent;

    // Start is called before the first frame update
    public override void startTask()
    {
        TASK_START();
    }

    public override void TASK_START()
    {
        GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = false;

        if (!manager) Start();
        base.startTask();

        //cur_tar = GameObject.FindGameObjectWithTag("tar_obj").GetComponent<NavigationTask>();

        // Move player to where the object first appears (close to where the respective collider is)
        var curTask = GameObject.Find("Tasks").GetComponent<TaskList>().currentTask.name;
        var currentRepeat = gameObject.GetComponentInParent<TaskList>().repeatCount;
        if (curTask == "TASK_MainLoop")
        {
            mainLoopCurrent = true;
            var trialNum = GameObject.Find("TrialCounter").GetComponent<TrialCounter>().trialNum;
            tempTrial = trialNum - (3 - currentRepeat);
            currentRepeat = currentRepeat - 1;
            Debug.Log(" Current Repeat Num: " + currentRepeat);
            item = GameObject.Find("ChooseTask").GetComponent<LM_ChooseTask>().loc[currentRepeat]; // this is just the location name
        }
        else
        {
            mainLoopCurrent = false;
            currentRepeat--;
            item = GameObject.Find("ST_TrackTargets").GetComponent<LM_TrackTargets>().loc_array[currentRepeat];
            
        }

        itemLocation = item.transform.position;
        
        //cur_tar.prevTarget.SetActive(false); // The item should already by deactivated
        var newItemLocation = new Vector3(itemLocation.x + 11, 0.25f, 0); // This is just to move the avatar to be placed in front of the item
        avatar.transform.position = newItemLocation;
        avatar.transform.rotation = Quaternion.Euler(0,-90,0);
        //Camera.main.transform.position = avatar.transform.position;
        Camera.main.transform.rotation = avatar.transform.rotation;

        hud.showEverything(); // Pin the tail on the donkey time
    }

    public override bool updateTask()
    {
        // The subject clicked on the screen that they think the object was
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(" This is the coordinates: " + mousePos.x + ", " + mousePos.y + ", " + Camera.main.nearClipPlane);
            var newCoords = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 11f)); // 11 because we changed the avatar position's x on line 50 to +11
            //Debug.Log(" This is the new coordinates: " + newCoords.x + ", " + newCoords.y + ", " + newCoords.z);
            Vector2 response = new Vector2(newCoords.y, newCoords.z);
            Vector2 goal = new Vector2(itemLocation.y, itemLocation.z);
            //var item3D2D = Camera.main.WorldToScreenPoint(itemLocation);
            //Debug.Log(" This is the item coordinates: " + itemLocation.x + ", " + itemLocation.y + ", " + itemLocation.z);
            //Debug.Log(" This is the item's new coordinates: " + item3D2D.x + ", " + item3D2D.y + ", " + item3D2D.z);
            //var back3D = Camera.main.ScreenToWorldPoint(item3D2D);
            //Debug.Log(" This is the item's converted back to 3D coordinates: " + back3D.x + ", " + back3D.y + ", " + back3D.z);
            float distanceError = Vector2.Distance(goal, response);
            GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = true;

            // RECORD DATA
            var output = GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>();
            if (mainLoopCurrent)
            {
               output.fileBuffer += "Spatial, " + tempTrial + ", " + item.name + ", " + itemLocation.x + ", " +
                    itemLocation.y + ", " + itemLocation.z + ", " + newCoords.x + ", " + newCoords.y + ", " + newCoords.z + ", " + distanceError + ", , , ";

                output.AddData();
            }
            else
            {
                if (gameObject.GetComponentInParent<TaskList>().repeatCount == 1)
                {
                    output.sTBuffer += GameObject.Find("TASK_SpaceTime").GetComponent<TaskList>().repeatCount + ", "; // Get the trial number
                }

                output.sTBuffer += itemLocation.x + ", " + itemLocation.y + ", " + itemLocation.z + ", " +
                    newCoords.x + ", " + newCoords.y + ", " + newCoords.z + ", " + distanceError + ", ";
            }
            
            return true;
        }
        return false;
    }
    public override void endTask()
    {
        TASK_END();
    }

    public override void TASK_END()
    {
        base.endTask();
    }
}
