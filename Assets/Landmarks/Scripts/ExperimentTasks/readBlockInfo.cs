/*
    ReadBlockInfo

    Created by Melanie Gin 12/2024
  
    This file will read in the block and trial info which will have the target location
    for each random target in trial for spatial and temporal. It will save these as a 2D
    list where the row is the block number and the columns are the trial. Items 1-60 (60 items)
   
    File name: Block_info.csv
    

    It will also read in the sequence block csv file into a separate 2D list since there are
    30 trials as well but three objects appear in the same hall. Items 61-150 (90 items)

    File name: seq_block.csv

    Lastly it will read the last portion of the project's csv file that has the 5 items' locations for
    the 10 trials. This will again be stored in a 2D list. Items 151-200 (50 items)

    File name: lastSequence5.csv
 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class readBlockInfo : ExperimentTask
{
    [Header("Task-specific Properties")]

    // ----------------------- SPATIAL & TEMPORAL BLOCK VARS ---------------------------------------------
    public string blockInfoFile = "Block_info.csv";

    // These two will give us the location name for the target as the header for the file is: Environment, Block, Trial 1, Trial 2, Trial 3, Trial 4, Trial 5, Trial 6
    public int block; // This var will help access targetLocation2D list. As of 12/24 there are 5 block with 6 trials. Spatial -> temporal -> spatial -> temporal, etc.
    public List<List<GameObject>> targetLocations2D = new List<List<GameObject>>(); // This will be a 2D array. Lists within a list, the first index will be the block and the list in that index will be of the trials

    private GameObject targets;
    public int count;
    // -------------------------------------------------------------------------------


    //------------------------ SPACE-TIME BLOCK VARS ----------------------------------------
    // This list won't need any other variable as it will be accessed directly when the seq block happens.
    public string spaceTimeInfoFile = "seq_block.csv";
    public List<List<GameObject>> seqLocations2D = new List<List<GameObject>>();
    // -------------------------------------------------------------------------------

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
        targets = GameObject.Find("TargetObjects");
        ReplaceTargetPos(blockInfoFile, targetLocations2D, 0, 60, 2, 6);
        ReplaceTargetPos(spaceTimeInfoFile, seqLocations2D, 60, 150, 2, 3);
        ReplaceTargetPos(seqInfoFile, seqLoc2D, 150, 200, 1, 5);
        //SpatialTempFile();
        //SeqFile();
        //ReadSeqFile();
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

    public GameObject CurrentSeq(int row, int col)
    {
        return seqLocations2D[row][col];
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

    // This function just reads in the csv file for spatial and temporal block and fills in the 2D list with the locations
    //private void SpatialTempFile()
    //{
    //    Create2DList(blockInfoFile, targetLocations2D, 2);

    //    count = targetLocations2D.Count;

    //    // Replace position of target objects with the positons in the excel file
    //    var targets = GameObject.Find("TargetObjects");
    //    int b = 0; // temp block var
    //    var t = 0; // temp trial var
    //    //for (int i = 0; i < targets.transform.childCount; i++)
    //    for (int i = 0; i < 60; i++) // The spatial-temp blocks are 30 each so 60 items needed total
    //    {
    //        var cur_tar = targets.transform.GetChild(i);
    //        if (targetLocations2D[b][t].name.Contains("bottom"))
    //        {
    //            var temp_pos = new Vector3(targetLocations2D[b][t].transform.position.x, cur_tar.transform.position.y + targetLocations2D[b][t].transform.position.y, targetLocations2D[b][t].transform.position.z);
    //            cur_tar.transform.position = temp_pos;
    //        }
    //        else { cur_tar.transform.position = targetLocations2D[b][t].transform.position; }
    //        t++;
    //        if (t == 6)
    //        {
    //            t = 0; // go back to trial one for next block
    //            b++; // increase to next block
    //        }
    //    }

    //}


    //private void SeqFile()
    //{
    //    Create2DList(spaceTimeInfoFile, seqLocations2D, 2);


    //    // Replace position of target objects with the positons in the excel file
    //    var targets = GameObject.Find("TargetObjects");
    //    int b = 0; // temp block var
    //    var t = 0; // temp trial var
    //    for (int i = 60; i < 150; i++)
    //    {
    //        var cur_tar = targets.transform.GetChild(i);
    //        if (seqLocations2D[b][t].name.Contains("bottom"))
    //        {
    //            var temp_pos = new Vector3(seqLocations2D[b][t].transform.position.x, cur_tar.transform.position.y + seqLocations2D[b][t].transform.position.y, seqLocations2D[b][t].transform.position.z);
    //            cur_tar.transform.position = temp_pos;
    //        }
    //        else { cur_tar.transform.position = seqLocations2D[b][t].transform.position; }
            
    //        t++;
    //        if (t == 3)
    //        {
    //            t = 0; // go back to trial one for next block
    //            b++; // increase to next block
    //        }
    //    }
    //}

    //private void ReadSeqFile()
    //{

    //    Create2DList(seqInfoFile, seqLoc2D, 1);


    //    // Replace position of target objects with the positons in the excel file
    //    var targets = GameObject.Find("TargetObjects");
    //    int b = 0; // temp block var
    //    var t = 0; // temp trial var
    //    for (int i = 150; i < 200; i++)
    //    {
    //        var cur_tar = targets.transform.GetChild(i);
    //        if (seqLoc2D[b][t].name.Contains("bottom"))
    //        {
    //            var temp_pos = new Vector3(seqLoc2D[b][t].transform.position.x, cur_tar.transform.position.y + seqLoc2D[b][t].transform.position.y, seqLoc2D[b][t].transform.position.z);
    //            cur_tar.transform.position = temp_pos;
    //        }
    //        else { cur_tar.transform.position = seqLoc2D[b][t].transform.position; }

    //        t++;
    //        if (t == 5)
    //        {
    //            t = 0; // go back to trial one for next block
    //            b++; // increase to next block
    //        }
    //    }
    //}

    private void ReplaceTargetPos(String file, List<List<GameObject>> twoD, int start, int end, int startCol, int endTrial)
    {
        Create2DList(file, twoD, startCol);
        
        // Replace position of target objects with the positons in the excel file
        int b = 0; // temp block var
        var t = 0; // temp trial var
        for (int i = start; i < end; i++)
        {
            var cur_tar = targets.transform.GetChild(i);
            if (twoD[b][t].name.Contains("bottom"))
            {
                var temp_pos = new Vector3(twoD[b][t].transform.position.x, cur_tar.transform.position.y + twoD[b][t].transform.position.y, twoD[b][t].transform.position.z);
                cur_tar.transform.position = temp_pos;
            }
            else { cur_tar.transform.position = twoD[b][t].transform.position; }

            t++;
            if (t == endTrial)
            {
                t = 0; // go back to trial one for next block
                b++; // increase to next block
            }
        }
    }

    private void Create2DList(String file, List<List<GameObject>> twoD, int startCol)
    {
        if (File.Exists(file))
        {
            Debug.Log("________ Last Seq info File found");
            // We will read the file and put the locations in a temp list to put in the seqLocation2D list
            using (var reader = new StreamReader(file))
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
                        for (int i = startCol; i < splitLine.Length; i++)
                        {
                            GameObject loc = GameObject.Find(splitLine[i]); // Save the game object so we can get the coordinates and tag
                            //Debug.Log(" Here is the name and tag: " + loc.name + " " + loc.tag);
                            temp.Add(loc);
                        }

                        twoD.Add(temp);
                    }
                }
            }
        }

    }
}