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
    public StreamWriter seqOutput;
    public string fileBuffer;
    private string fileName;
    //public static string seqBuffer;


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

        fileOutput = new StreamWriter(path);
        fileOutput.WriteLine(header);


        // DO LATER! SEE HOW WE WANT TO DO SEQ
        //path = outputPath + fileName + "_Sequence_Output.csv";
        //seqOutput = new StreamWriter(path);
        //seqOutput.WriteLine(header);

        // Start the buffers
        fileBuffer += fileName + ", " + sex + ", ";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddData()
    {
        fileOutput.WriteLine(fileBuffer);
        fileBuffer = fileName + ", " + sex + ", ";
    }

    void OnApplicationQuit()
    {
        fileOutput.Close();
    }
}
