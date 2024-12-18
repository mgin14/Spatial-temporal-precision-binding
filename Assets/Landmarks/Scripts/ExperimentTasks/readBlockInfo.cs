/*
    LM Dummy format
       
    Attached object holds task components that need to be effectively ignored 
    by Tasklist but are required for the script. Thus the object this is 
    attached to can be detected by Tasklist (won't throw error), but does nothing 
    except start and end.   

    Copyright (C) 2019 Michael J. Starrett

    Navigate by StarrLite (Powered by LandMarks)
    Human Spatial Cognition Laboratory
    Department of Psychology - University of Arizona  
    -----------------------------------------------------------------------------------------
    ReadBlockInfo

    Created by Melanie Gin 12/2024
  
    This file will read in the block and trial info which will have the target location
    for each random target in trial for spatial and temporal.
   
    File name: Block_info.xlsx
 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class readBlockInfo : ExperimentTask
{
    [Header("Task-specific Properties")]
    public string blockInfoFile = "Block_info.csv";
    // These two will give us the location name for the target as the header for the file is: Environment, Block, Trial 1, Trial 2, Trial 3, Trial 4, Trial 5, Trial 6
    public int block = 0; // This var will help access targetLocation2D list. As of 12/24 there are 5 block with 6 trials. Spatial -> temporal -> spatial -> temporal, etc.
    public List<List<GameObject>> targetLocations2D = new List<List<GameObject>>(); // This will be a 2D array. Lists within a list, the first index will be the block and the list in that index will be of the trials
    public int count;


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

        // Make sure the file exists
        if (File.Exists(blockInfoFile))
        {
            Debug.Log("________ Block_info File found");
            // We will read the file and put the locations in a temp list to put in the targetLocation2D list
            using (var reader = new StreamReader(blockInfoFile))
            {
                int count = 1; // This will be used to not include the header line
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (count == 1)
                    {
                        count++;
                    }
                    else
                    {
                        var splitLine = line.Split(',');

                        List<GameObject> temp = new List<GameObject>();
                        for (int i = 2; i < splitLine.Length; i++)
                        {
                            GameObject loc = GameObject.Find(splitLine[i]); // Save the game object so we can get the coordinates and tag
                            Debug.Log(" Here is the name and tag: " + loc.name + " " + loc.tag);
                            temp.Add(loc);
                        }

                        targetLocations2D.Add(temp);
                    }
                }
            }
        }

        count = targetLocations2D.Count;

        // Replace position of target objects with the positons in the excel file
        var targets = GameObject.Find("TargetObjects");
        int b = 0; // temp block var
        var t = 0; // temp trial var
        //for (int i = 0; i < targets.transform.childCount; i++)
        for (int i = 0; i < 30; i++)
        {
            var cur_tar = targets.transform.GetChild(i);
            cur_tar.transform.position = targetLocations2D[b][t].transform.position;
            t++;
            if (t == 6)
            {
                t = 0; // go back to trial one for next block
                b++; // increase to next block
            }
        }
    }
    
    public void IncrementBlock()
    {
        block++;
    }

    public GameObject CurrentLocation()
    {
        var trial = GameObject.Find("TrialCounter").GetComponent<TrialCounter>().trialNum -1;
        return targetLocations2D[block][trial];
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