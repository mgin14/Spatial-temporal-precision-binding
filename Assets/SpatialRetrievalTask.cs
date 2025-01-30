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

    private bool feedback = false;
    public float timeFrame = 3f;
    private float timer;

    // Start is called before the first frame update
    public override void startTask()
    {
        TASK_START();
    }

    public override void TASK_START()
    {
        GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
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
        else if (gameObject.transform.parent.name == "Practice")
        {
            item = gameObject.transform.parent.GetChild(1).GetComponentInChildren<ViewPracticeObject>().current;
        }
        else
        {
            mainLoopCurrent = false;
            currentRepeat--;
            item = GameObject.Find("ST_TrackTargets").GetComponent<LM_TrackTargets>().loc_array[currentRepeat];
            
        }
        
        itemLocation = item.transform.position;
        
        //cur_tar.prevTarget.SetActive(false); // The item should already by deactivated
        var newItemLocation = new Vector3(itemLocation.x + 10.4f, 0, 0); // This is just to move the avatar to be placed in front of the item
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
            if (gameObject.transform.parent.name == "Practice")
            {
                item.SetActive(true);
                timer += Time.deltaTime;
                feedback = true;
                return false;
            }

            Vector3 mousePos = Input.mousePosition;
            //Debug.Log(" This is the coordinates: " + mousePos.x + ", " + mousePos.y + ", " + Camera.main.nearClipPlane);
            var newCoords = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10.4f)); // 10.4 because we changed the avatar position's x on line 50 to +11
            //Debug.Log(" This is the new coordinates: " + newCoords.x + ", " + newCoords.y + ", " + newCoords.z);
            Vector2 response = new Vector2(newCoords.y, newCoords.z);
            Vector2 goal = new Vector2(itemLocation.y, itemLocation.z);
            float distanceError = Vector2.Distance(goal, response);
            GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = true;

            // ----------------------------RECORD DATA------------------------------
            var output = GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>();
            if (mainLoopCurrent)
            {
                output.fileOutput.Write("Spatial, " + tempTrial + ", " + item.name + ", " + itemLocation.x + ", " +
                    itemLocation.y + ", " + itemLocation.z + ", " + newCoords.x + ", " + newCoords.y + ", " + newCoords.z + ", " + distanceError + ", , , ");
                output.fileOutput.Flush();
                output.AddData();
            }
            else
            {
                if (gameObject.GetComponentInParent<TaskList>().repeatCount == 1)
                {
                    output.sTOutput.Write(GameObject.Find("TASK_SpaceTime").GetComponent<TaskList>().repeatCount + ", "); // Get the trial number
                }
                var cur_item = gameObject.transform.parent.parent.parent.GetChild(1).GetComponent<LM_TrackTargets>().tar_array[gameObject.GetComponentInParent<TaskList>().repeatCount - 1];
                output.sTOutput.Write(cur_item.name + ", " + itemLocation.x + ", " + itemLocation.y + ", " + itemLocation.z + ", " +
                    newCoords.x + ", " + newCoords.y + ", " + newCoords.z + ", " + distanceError + ", ");
                output.sTOutput.Flush();
            }
            
            return true;
        }
        if (feedback)
        {
            timer += Time.deltaTime;
            if (timer > timeFrame)
            {
                timer = 0;
                feedback = false;
                item.SetActive(false);
                return true;
            }
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
