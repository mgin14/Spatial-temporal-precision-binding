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

public class LM_TrackTargets : ExperimentTask
{
    [Header("Task-specific Properties")]
    public List<GameObject> tar_array;
    public List<GameObject> loc_array;

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

        tar_array = new List<GameObject>();
        loc_array = new List<GameObject>();
    }


    public override bool updateTask()
    {
        return true;

        // WRITE TASK UPDATE CODE HERE
    }

    public void AddTarget(GameObject go)
    {
        tar_array.Add(go);
    }

    public GameObject GetTarget(int index)
    {
        return tar_array[index];
    }

    public void AddLocation(GameObject go)
    {
        loc_array.Add(go);
    }

    public GameObject GetLocation(int index)
    {
        return loc_array[index];
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