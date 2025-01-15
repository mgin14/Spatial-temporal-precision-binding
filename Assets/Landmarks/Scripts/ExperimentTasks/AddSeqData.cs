/*
    All this does is add the current trial's data to the sequence output file
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSeqData : ExperimentTask
{
    [Header("Task-specific Properties")]
    public GameObject dummyProperty;

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

        GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>().AddSTData();
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