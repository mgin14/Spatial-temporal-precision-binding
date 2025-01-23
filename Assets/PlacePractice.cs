/*
    Created by Melanie Gin 1/2025
    
    This script simply places the practice object into place.
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlacePractice : ExperimentTask
{
    //[Header("Task-specific Properties")]


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

        gameObject.transform.parent.GetChild(1).GetComponent<ObjectList>().objects[0].transform.position = GameObject.Find("mm_l_mid_mid").transform.position;
        //gameObject.GetComponent<ObjectList>().objects[0].transform.position = GameObject.Find("mm_l_mid_mid").transform.position;

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
