/*
    LM Dummy
       
    Attached object holds task components that need to be effectively ignored 
    by Tasklist but are required for the script. Thus the object this is 
    attached to can be detected by Tasklist (won't throw error), but does nothing 
    except start and end.   

    Copyright (C) 2019 Michael J. Starrett

    Navigate by StarrLite (Powered by LandMarks)
    Human Spatial Cognition Laboratory
    Department of Psychology - University of Arizona   
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LM_ChooseTask : ExperimentTask
{
    [Header("Task-specific Properties")]
    public List<GameObject> tar;
    public List<GameObject> loc;

    public override void startTask()
    {
        TASK_START();

        // LEAVE BLANK
    }


    public override void TASK_START()
    {
        if (!manager) Start();
        base.startTask();

        if (skip)
        {
            log.log("INFO    skip task    " + name, 1);
            return;
        }

        // Choose which task to not skip
        TaskList spatial = GameObject.Find("Spatial").GetComponent<TaskList>();
        TaskList temporal = GameObject.Find("Temporal").GetComponent<TaskList>();
        ArrayList ar = new ArrayList() { 3, 6 };
        int cur_trial = GameObject.Find("TrialCounter").GetComponent<TrialCounter>().trialNum;

        if (ar.Contains(cur_trial)) // Only do retrieval if in trial 3 or 6
        {

            if (GameObject.Find("ReadTrialInfo").GetComponent<readBlockInfo>().block % 2 == 0) // Spatial Block
            {
                temporal.skip = true;
                spatial.skip = false;
            }
            else // temporal block
            {
                spatial.skip = true;
                temporal.skip = false;
            }
        }

        // Shuffle the current 3 targets
        LM_TrackTargets tracker = GameObject.Find("TrackTargets").GetComponent<LM_TrackTargets>();
        //var random = new System.Random();
        tar = new List<GameObject>( new GameObject[] { tracker.GetTarget(0), tracker.GetTarget(1), tracker.GetTarget(2) });
        loc = new List<GameObject>( new GameObject[] { tracker.GetLocation(0), tracker.GetLocation(1), tracker.GetLocation(2) });
        var random = new System.Random();
        for (int i = 3; i > 1; i--)
        {
            // Pick random element to swap.
            int j = random.Next(i); // 0 <= j <= i-1
            GameObject tmp = tar[j];
            GameObject tmp2 = loc[j];
            tar[j] = tar[i - 1];
            loc[j] = loc[i - 1];
            tar[i - 1] = tmp;
            loc[i - 1] = tmp2;
        }

    }


    public override bool updateTask()
    {
        return true;

        // WRITE TASK UPDATE CODE HERE
    }


    public override void endTask()
    {
        TASK_END();

        // LEAVE BLANK
    }


    public override void TASK_END()
    {
        base.endTask();

        // WRITE TASK EXIT CODE HERE
    }

}