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

public class SeqSpatialRetrievalTask : ExperimentTask
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
        currentRepeat = currentRepeat - 1;
        item = GameObject.Find("Seq_TrackTargets").GetComponent<LM_TrackTargets>().loc_array[currentRepeat]; // this is the current object they are trying to locate
        itemLocation = item.transform.position;
        avatar.transform.position = GameObject.Find("TempRetrievalTask").GetComponent<SeqNavigationTask>().avatarLocation;

        //var newItemLocation = new Vector3(itemLocation.x + 11, 0.25f, 0);
        //avatar.transform.position = newItemLocation;
        avatar.transform.rotation = Quaternion.Euler(0,-90,0);
        ////Camera.main.transform.position = avatar.transform.position;
        Camera.main.transform.rotation = avatar.transform.rotation;

        hud.showEverything(); // Pin the tail on the donkey time
    }

    public override bool updateTask()
    {
        // The subject clicked on the screen that they think the object was
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            var newCoords = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 11f));
            //Vector3 response = new Vector3(newCoords.x, newCoords.y, newCoords.z * 10);
            Vector2 response = new Vector2(newCoords.y, newCoords.z);
            //Vector3 goal = new Vector3(itemLocation.x, itemLocation.y, itemLocation.z);
            Vector2 goal = new Vector2(itemLocation.y, itemLocation.z);
            //float distanceError = Vector3.Distance(goal, response);
            float distanceError = Vector2.Distance(goal, response);
            GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = true;
            var output = GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>();
            output.seqOutput.Write(itemLocation.x + ", " + 
                itemLocation.y + ", " + itemLocation.z + ", " + newCoords.x + ", " + newCoords.y + ", " + (newCoords.z) + ", " + distanceError + ",");
            output.seqOutput.Flush();
            output.AddSeqData();
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
