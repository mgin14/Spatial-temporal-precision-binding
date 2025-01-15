/*
    LM_TrackTargets.cs
    created by Melanie G 12/2024

    The script is found by FrontColliderScript in order to keep track of the current three objects and their location
    in order to successfully complete the spatial and temporal retrieval. 
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