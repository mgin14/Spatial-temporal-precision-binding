/*
    Created by Melanie Gin 1/2025
    
    This script is to activate the other two colliders between the front-mid and mid-far for the last block
    for sequence, as well as read in the information from the excel file with all 5 locations for each trial.
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ActivateSeqStuff : ExperimentTask
{
    [Header("Task-specific Properties")]
    public string seqInfoFile = "lastSequence5.csv";
    public List<List<GameObject>> seqLoc2D = new List<List<GameObject>>();
    

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

        GameObject.Find("front_mid_collider").GetComponent<BoxCollider>().isTrigger = true;
        GameObject.Find("far_mid_collider").GetComponent<BoxCollider>().isTrigger = true;

        ReadSeqFile();
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


    private void ReadSeqFile()
    {
        // Make sure the spatial and temporal file exists
        if (File.Exists(seqInfoFile))
        {
            Debug.Log("________ Last Seq info File found");
            // We will read the file and put the locations in a temp list to put in the seqLocation2D list
            using (var reader = new StreamReader(seqInfoFile))
            {
                int row = 1; // This will be used to not include the header line
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (row == 1)
                    {
                        row++;
                    }
                    else
                    {
                        var splitLine = line.Split(',');

                        List<GameObject> temp = new List<GameObject>();
                        for (int i = 1; i < splitLine.Length; i++)
                        {
                            GameObject loc = GameObject.Find(splitLine[i]); // Save the game object so we can get the coordinates and tag
                            //Debug.Log(" Here is the name and tag: " + loc.name + " " + loc.tag);
                            temp.Add(loc);
                        }

                        seqLoc2D.Add(temp);
                    }
                }
            }
        }


        // Replace position of target objects with the positons in the excel file
        var targets = GameObject.Find("TargetObjects");
        int b = 0; // temp block var
        var t = 0; // temp trial var
        for (int i = 150; i < 200; i++)
        {
            var cur_tar = targets.transform.GetChild(i);
            seqLoc2D[b][t].transform.position = new Vector3(seqLoc2D[b][t].transform.position.x, cur_tar.transform.position.y + seqLoc2D[b][t].transform.position.y, seqLoc2D[b][t].transform.position.z);
            cur_tar.transform.position = seqLoc2D[b][t].transform.position;
            t++;
            if (t == 5)
            {
                t = 0; // go back to trial one for next block
                b++; // increase to next block
            }
        }
    }

}