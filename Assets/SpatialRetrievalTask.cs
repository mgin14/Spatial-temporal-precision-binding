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
        var currentRepeat = gameObject.GetComponentInParent<TaskList>().repeatCount;
        var trialNum = GameObject.Find("TrialCounter").GetComponent<TrialCounter>().trialNum;
        tempTrial = trialNum - (3 - currentRepeat);
        currentRepeat = currentRepeat - 1;
        Debug.Log(" Current Repeat Num: " + currentRepeat);
        item = GameObject.Find("ChooseTask").GetComponent<LM_ChooseTask>().loc[currentRepeat]; // this is just the location name
        itemLocation = item.transform.position;
        //cur_tar.prevTarget.SetActive(false); // The item should already by deactivated
        var newItemLocation = new Vector3(itemLocation.x + 11, 0.25f, 0);
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
            var newCoords = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            Debug.Log(newCoords.x); // This will always show that we are 10/11 units less than the actual position (see line 47). Use Y and z
            Debug.Log(newCoords.y);
            Debug.Log(newCoords.z);
            Vector2 response = new Vector2(newCoords.y, newCoords.z * 100);
            Vector2 goal = new Vector2(itemLocation.y, itemLocation.z);
            float distanceError = Vector2.Distance(goal, response);
            GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = true;
            int block = GameObject.Find("ReadTrialInfo").GetComponent<readBlockInfo>().block;
            GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>().fileBuffer += block + ", " + tempTrial + ", " + item.name + ", " + itemLocation.x + ", " + 
                itemLocation.y + ", " + itemLocation.z + ", " + newCoords.x + ", " + newCoords.y + ", " + (newCoords.z * 100) + ", " + distanceError + ", , , ";

            GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>().AddData();
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
