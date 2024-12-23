/* Creator: Melanie Gin Nov 2024
 * This script is for the temporal retrieval portion of the Spatial_temporal_precision_binding project proposed by Dr. Arne Ekstrom
 * from the University of Arizona.
 * 
 * In this script the user will be shown a black screen and they will have to hold the space bar for however long it took
 * them to get to the object that they were shown in the hallway.
 * 
 */


using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using Valve.VR;
using System.IO;
using UnityEngine.XR;

public class TemporalRetrievalTrial : ExperimentTask
{
    float temporalStartTime; //Get the time the space bar is held down
    float response;
    private int tempTrial; // this is just for output
    private float[] goalTimes = { 1.85f, 5.11f, 7.66f };
    private float goal;
    private GameObject item;
    public Vector3 itemLocation;

    // Start is called before the first frame update
    public override void startTask()
    {
        TASK_START();
    }

    public override void TASK_START()
    {
        if (!manager) Start();
        base.startTask();

        var currentRepeat = gameObject.GetComponentInParent<TaskList>().repeatCount;
        tempTrial = 3 - (3 - currentRepeat);
        currentRepeat = currentRepeat - 1;
        item = GameObject.Find("ChooseTask").GetComponent<LM_ChooseTask>().loc[currentRepeat];
        itemLocation = item.transform.position;

        if (item.tag == "front")
        {
            goal = goalTimes[0];
        }
        else if (item.tag == "mid")
        {
            goal = goalTimes[1];
        }
        else goal = goalTimes[2];

    }

    // the returned bool indicates whether to continue updating. True-stop, false-continue
    public override bool updateTask()
    {
        if (skip)
        {
            Debug.Log("Skipping Temporal Task");
            log.log("INFO    skip task    " + name, 1);
            return true;
        }
        
        // Get the time the subject first presses the space bar
        if (Input.GetKeyDown(KeyCode.Space)) // This will only be initialized once when they press the space bar
        {
            temporalStartTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            response = Time.time - temporalStartTime;
            var timeError = goal - response;

            int block = GameObject.Find("ReadTrialInfo").GetComponent<readBlockInfo>().block;
            GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>().fileBuffer += block + ", " + tempTrial + ", " + item.name + 
                ", " + itemLocation.x + ", " + itemLocation.y + ", " + itemLocation.z + ", , , , ," + goal + ", " + response + ", " + timeError;

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
        // Input response to output file HERE


        base.endTask();
    }

    public override void TASK_PAUSE()
    {
        avatarLog.navLog = false;
        if (isScaled) scaledAvatarLog.navLog = false;
        //base.endTask();
        log.log("TASK_PAUSE\t" + name + "\t" + this.GetType().Name + "\t", 1);
        //avatarController.stop();

        hud.setMessage("");
        hud.showScore = false;

    }




}
