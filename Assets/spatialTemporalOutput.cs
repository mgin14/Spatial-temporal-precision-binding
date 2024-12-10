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

    public StreamWriter spatialOutput;
    public StreamWriter temporalOutput;
    public StreamWriter seqOutput;
    public static string spatialBuffer;
    public static string temporalBuffer;
    //public static string seqBuffer;


    // Start is called before the first frame update
    void Start()
    {
        // Initiate the csv files for spatial, temporal, and sequence
        string outputPath = Directory.GetCurrentDirectory() + "\\Subject_Data\\";
        string fileName = subject + subjectNumber;
        string envir = "_Spatial_Output.csv";
        string path = outputPath + fileName + envir;

        // Check that subject number does not exist
        while (File.Exists(path))
        {
            subjectNumber++;
            fileName = subject + subjectNumber + envir;
            path = outputPath + fileName + envir;
        }

        string header = "Subject, sex, TrialNum, Object, ";

        spatialOutput = new StreamWriter(path);
        spatialOutput.WriteLine(header + "Coord_x, Coord_y, Coord_z, Response_x, Response_y, Response_z, X_error, Y_error, Z_error");

        path = outputPath + fileName + "_Temporal_Output.csv";
        temporalOutput = new StreamWriter(path);
        temporalOutput.WriteLine(header + "Actual_time, Response_time, Temporal_error");

        // DO LATER! SEE HOW WE WANT TO DO SEQ
        //path = outputPath + fileName + "_Sequence_Output.csv";
        //seqOutput = new StreamWriter(path);
        //seqOutput.WriteLine(header);

        // Start the buffers
        spatialBuffer += fileName + ", " + sex + ", ";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
