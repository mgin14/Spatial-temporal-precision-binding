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
using UnityEngine.UI;

public class TemporalRetrievalTrial : ExperimentTask
{
    float temporalStartTime; //Get the time the space bar is held down
    float response;
    private int tempTrial; // this is just for output
    private float[] goalTimes = { 1.73f, 4.97f, 8.25f }; // These are the measurements from unity
    private float goal;
    private GameObject item;
    public Vector3 itemLocation;

    //text
    [TextArea] public string masterText;
    private Text canvas;
    private HUD avatarHUD;
    private Color defaultColor;
    private int defaultFont;

    private bool mainLoopCurrent;

    // Start is called before the first frame update
    public override void startTask()
    {
        TASK_START();
    }

    public override void TASK_START()
    {
        if (!manager) Start();
        base.startTask();

        var curTask = GameObject.Find("Tasks").GetComponent<TaskList>().currentTask.name;
        var currentRepeat = gameObject.GetComponentInParent<TaskList>().repeatCount;
        if (curTask == "TASK_MainLoop")
        {
            mainLoopCurrent = true;
            var trialNum = GameObject.Find("TrialCounter").GetComponent<TrialCounter>().trialNum;
            tempTrial = trialNum - (3 - currentRepeat);
            currentRepeat--;
            item = GameObject.Find("ChooseTask").GetComponent<LM_ChooseTask>().loc[currentRepeat];
        }
        else if (gameObject.transform.parent.name == "Practice")
        {
            item = GameObject.Find("mm_l_mid_mid");
        }
        else
        {
            mainLoopCurrent = false;
            currentRepeat--;
            item = GameObject.Find("ST_TrackTargets").GetComponent<LM_TrackTargets>().loc_array[currentRepeat];// this is the current object's location name
        }

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

        // handle changes to the hud
        // Change the anchor points to put the message at the bottom
        RectTransform hudposition = hud.hudPanel.GetComponent<RectTransform>() as RectTransform;
        hudposition.pivot = new Vector2(0.5f, 0.5f);

        // Set up the text
        hud.setMessage(masterText);
        canvas = GameObject.Find("[HudCanvas]").GetComponent<Text>();
        defaultColor = canvas.color;
        defaultFont = canvas.fontSize;
        canvas.color = Color.white;
        canvas.fontSize = 55;
        hud.ForceShowMessage();
        avatarHUD = avatar.GetComponent<HUD>();
        avatarHUD.SecondsToShow = 9999;
        avatarHUD.GeneralDuration = 9999;
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
            if (gameObject.transform.parent.name == "Practice") { return true; }

            response = Time.time - temporalStartTime;
            var timeError = response - goal; // Overshooting will result in positive error; undershooting will be negative
            var output = GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>();
            if (mainLoopCurrent)
            {
                output.fileOutput.Write("Temporal, " + tempTrial + ", " + item.name +
                    ", " + itemLocation.x + ", " + itemLocation.y + ", " + itemLocation.z + ", , , , ," + goal + ", " + response + ", " + timeError);
                output.fileOutput.Flush();
                // Input response to output file HERE
                output.AddData();
            }
            else
            {
                output.sTOutput.Write(goal + ", " + response + ", " + timeError + ", ");
                output.sTOutput.Flush();
            }
            
            return true;
        }

        return false;
    }


    public override void endTask()
    {
        
        TASK_END();
        // Reset the text
        canvas.color = defaultColor;
        canvas.fontSize = defaultFont;
        avatarHUD.SecondsToShow = 0;
        avatarHUD.GeneralDuration = 0;

    }

    public override void TASK_END()
    {

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
