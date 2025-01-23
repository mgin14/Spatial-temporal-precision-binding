/* Created by: Melanie 12/2024
 * 
 * This script creates the 3 output files and closes them when appropriate. There are added methods to start a new line for new data.
 */


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public enum SubjectSex
{
    Male,
    Female
}

public class spatialTemporalOutput : MonoBehaviour
{
    public string subject = "subject";
    public int subjectNumber;
    public SubjectSex sex;

    public StreamWriter fileOutput;
    public StreamWriter sTOutput;
    public StreamWriter seqOutput;
    private string fileName;

    [HideInInspector]
    public string fileBuffer;
    [HideInInspector]
    public string sTBuffer;
    [HideInInspector]
    public string seqBuffer;


    // Start is called before the first frame update
    void Start()
    {
        // Initiate the csv files for spatial, temporal, and sequence
        string outputPath = Directory.GetCurrentDirectory() + "\\Subject_Data\\";
        fileName = subject + subjectNumber;
        string envir = "_Output.csv";
        string path = outputPath + fileName + envir;

        // Check that subject number does not exist
        while (File.Exists(path))
        {
            subjectNumber++;
            fileName = subject + subjectNumber;
            path = outputPath + fileName + envir;
        }

        string header = "Subject, sex, Block, Trial, LocationName, LocationX, LocationY, LocationZ, ResponseX, ResponseY, ResponseZ, SpatialError, GoalTime, ResponseTime, TemporalError";
        fileOutput = new StreamWriter(path, true); // append to the file
        fileOutput.WriteLine(header);
        fileOutput.Flush();


        path = outputPath + fileName + "_SpaceTime_Output.csv";
        string st_header = "Subject, sex, Trial, " +
            "LocationX1, LocationY1, LocationZ1, ResponseX1, ResponseY1, ResponseZ1, SpatialError1, GoalTime1, ResponseTime1, TemporalError1, " +
            "LocationX2, LocationY2, LocationZ2, ResponseX2, ResponseY2, ResponseZ2, SpatialError2, GoalTime2, ResponseTime2, TemporalError2, " +
            "LocationX3, LocationY3, LocationZ3, ResponseX3, ResponseY3, ResponseZ3, SpatialError3, GoalTime3, ResponseTime3, TemporalError3";
        sTOutput = new StreamWriter(path, true); // append to the file
        sTOutput.WriteLine(st_header);
        sTOutput.Flush();

        path = outputPath + fileName + "_Sequence_Output.csv";
        string seq_header = "Subject, sex, Trial, SeqItem1, SeqItem2, SeqItem3, SeqItem4, SeqItem5, ItemResp1, ItemResp2, ItemResp3, ItemResp4, ItemResp5, SeqError";
        seqOutput = new StreamWriter(path, true); // append to the file
        seqOutput.WriteLine(seq_header);
        seqOutput.Flush();

        // Start the data line
        fileOutput.Write(fileName + ", " + sex + ", ");
        fileOutput.Flush();
        sTOutput.Write(fileName + ", " + sex + ", ");
        sTOutput.Flush();
        seqOutput.Write(fileName + ", " + sex + ", ");
        seqOutput.Flush();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddData()
    {
        fileOutput.WriteLine();
        fileBuffer = fileName + ", " + sex + ", ";
        fileOutput.Write(fileBuffer);
        fileOutput.Flush();
    }

    public void AddSTData()
    {
        sTOutput.WriteLine();
        sTBuffer = fileName + ", " + sex + ", ";
        sTOutput.Write(sTBuffer);
        sTOutput.Flush();

    }

    public void AddSeqData()
    {
        seqOutput.WriteLine();
        seqBuffer = fileName + ", " + sex + ", ";
        seqOutput.Write(seqBuffer);
        seqOutput.Flush();

    }

    void OnApplicationQuit()
    {
        fileOutput.Close();
        sTOutput.Close();
        seqOutput.Close();
    }
}
