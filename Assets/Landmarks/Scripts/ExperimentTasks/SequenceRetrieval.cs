/*
    This script is for the sequence retrieval where the subject needs to pick the order of the objects they saw.
    
*/

using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class SequenceRetrieval : ExperimentTask
{
    [Header("Task-specific Properties")]
    public List<GameObject> startObjects;
    public GameObject current;
    [HideInInspector] public GameObject destination;

    private static Vector3 position0;
    private static Quaternion rotation0;
    private static Transform parent0;
    private static Vector3 scale0;
    private static Vector3 position1;
    private static Quaternion rotation1;
    private static Transform parent1;
    private static Vector3 scale1;
    private static Vector3 position2;
    private static Quaternion rotation2;
    private static Transform parent2;
    private static Vector3 scale2;
    private static Vector3 position3;
    private static Quaternion rotation3;
    private static Transform parent3;
    private static Vector3 scale3;
    private static Vector3 position4;
    private static Quaternion rotation4;
    private static Transform parent4;
    private static Vector3 scale4;

    private int saveLayer0;
    private int saveLayer1;
    private int saveLayer2;
    private int saveLayer3;
    private int saveLayer4;
    private int viewLayer = 11;
    public bool blackout = true;
    public bool rotate = true;
    public long rotation_start;
    public float rotation_start_float;
    public Vector3 objectRotationOffset;
    public RotationAxis rotationAxis;
    public float rotationSpeed = 30.0f;
    public Vector3 objectPositionOffset;
    public bool restrictMovement = true;
    public float endTime = 5f;
    private float timer;

    //text
    private Text canvas;
    private HUD avatarHUD;
    private Color defaultColor;
    private int defaultFont;

    //public TextAsset instruction;
    [TextArea] public string masterText;

    [HideInInspector]
    public List<GameObject> resp;
    [HideInInspector]
    public List<GameObject> ogOrder;

    private Vector3 initialHUDposition;

    public override void startTask()
    {
        TASK_START();

        resp = new List<GameObject>();

        initCurrent();
        rotation_start = Experiment.Now();
        rotation_start_float = rotation_start / 1f;
        timer += Time.deltaTime;
        // Debug.Log(rotation_start);

    }

    public override void TASK_START()
    {
        Cursor.visible = true;

        if (!manager) Start();
        base.startTask();
        if (skip)
        {
            log.log("INFO	skip task	" + name, 1);
            return;
        }
        


        if (blackout) hud.showOnlyTargets();
        else hud.showEverything();



        if (restrictMovement)
        {
            manager.player.GetComponent<CharacterController>().enabled = false;
            if (avatar.GetComponent<FirstPersonController>())
            {
                avatar.GetComponentInChildren<Camera>().transform.localEulerAngles = Vector3.zero;
                avatar.GetComponent<FirstPersonController>().ResetMouselook();
                avatar.GetComponent<FirstPersonController>().enabled = false;
            }
            manager.scaledPlayer.GetComponent<ThirdPersonCharacter>().immobilized = true;
        }

        destination = avatar.GetComponentInChildren<LM_SnapPoint>().gameObject;

        // handle changes to the hud
        // Change the anchor points to put the message at the bottom
        RectTransform hudposition = hud.hudPanel.GetComponent<RectTransform>() as RectTransform;
        hudposition.pivot = new Vector2(0.5f, 0.75f);


        startObjects = GameObject.Find("Seq_TrackTargets").GetComponent<LM_TrackTargets>().tar_array;
        foreach (GameObject item in startObjects)
        {
            item.SetActive(true);
        }

        // Record the current five objects seen in the hall for this trial in the output
        var output = GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>();
        output.seqOutput.Write( 
            GameObject.Find("TASK_seq").GetComponent<TaskList>().repeatCount.ToString() + ',' + startObjects[0].name + ',' + startObjects[1].name + ',' + startObjects[2].name + ',' +
            startObjects[3].name + ',' + startObjects[4].name + ',');
        output.seqOutput.Flush();

        ogOrder = new List<GameObject>
        {
            startObjects[0],
            startObjects[1],
            startObjects[2],
            startObjects[3],
            startObjects[4]
        };

        // Shuffle list
        List<GameObject> temp_list = new List<GameObject>();
        var _random = new System.Random();
        while (startObjects.Count != 0)
        {
            int r = _random.Next(0, startObjects.Count);
            temp_list.Add(startObjects[r]);
            startObjects.RemoveAt(r);
        }

        startObjects = temp_list;
    }

    public override bool updateTask()
    {

        if (skip)
        {
            //log.log("INFO	skip task	" + name,1 );
            return true;
        }


        // If all the items have been clicked exit this task
        if (!startObjects[0].activeSelf && !startObjects[1].activeSelf && !startObjects[2].activeSelf && !startObjects[3].activeSelf && !startObjects[4].activeSelf)
        {
            returnCurrent();
            return true;
        }
        
        return false;
    }
    public void initCurrent()
    {
        // store original properties of the target
        position0 = startObjects[0].transform.position;
        rotation0 = startObjects[0].transform.rotation;
        parent0 = startObjects[0].transform.parent;
        scale0 = startObjects[0].transform.localScale;

        position1 = startObjects[1].transform.position;
        rotation1 = startObjects[1].transform.rotation;
        parent1 = startObjects[1].transform.parent;
        scale1 = startObjects[1].transform.localScale;

        position2 = startObjects[2].transform.position;
        rotation2 = startObjects[2].transform.rotation;
        parent2 = startObjects[2].transform.parent;
        scale2 = startObjects[2].transform.localScale;

        position3 = startObjects[3].transform.position;
        rotation3 = startObjects[3].transform.rotation;
        parent3 = startObjects[3].transform.parent;
        scale3 = startObjects[3].transform.localScale;

        position4 = startObjects[4].transform.position;
        rotation4 = startObjects[4].transform.rotation;
        parent4 = startObjects[4].transform.parent;
        scale4 = startObjects[4].transform.localScale;

        // move the targets to the viewing location temporarily
        startObjects[0].transform.parent = destination.transform;
        startObjects[0].transform.localPosition = new Vector3(-4f,1.5f,0);
        startObjects[1].transform.parent = destination.transform;
        startObjects[1].transform.localPosition = new Vector3(-2, 1.5f, 0);
        startObjects[2].transform.parent = destination.transform;
        startObjects[2].transform.localPosition = new Vector3(0f, 1.5f, 0);
        startObjects[3].transform.parent = destination.transform;
        startObjects[3].transform.localPosition = new Vector3(2, 1.5f, 0);
        startObjects[4].transform.parent = destination.transform;
        startObjects[4].transform.localPosition = new Vector3(4f, 1.5f, 0);
        //current.transform.localEulerAngles = objectRotationOffset;
        startObjects[0].transform.localScale = Vector3.Scale(startObjects[0].transform.localScale, destination.transform.localScale);
        startObjects[1].transform.localScale = Vector3.Scale(startObjects[1].transform.localScale, destination.transform.localScale);
        startObjects[2].transform.localScale = Vector3.Scale(startObjects[2].transform.localScale, destination.transform.localScale);
        startObjects[3].transform.localScale = Vector3.Scale(startObjects[3].transform.localScale, destination.transform.localScale);
        startObjects[4].transform.localScale = Vector3.Scale(startObjects[4].transform.localScale, destination.transform.localScale);

        // return the target to its original parent (we'll revert other values later)
        // this way it won't track with the "head" of the avatar
        startObjects[0].transform.parent = parent0;
        startObjects[1].transform.parent = parent1;
        startObjects[2].transform.parent = parent2;
        startObjects[3].transform.parent = parent3;
        startObjects[4].transform.parent = parent4;


        saveLayer0 = startObjects[0].layer;
        setLayer(startObjects[0].transform, viewLayer);
        saveLayer1 = startObjects[1].layer;
        setLayer(startObjects[1].transform, viewLayer);
        saveLayer2 = startObjects[2].layer;
        setLayer(startObjects[2].transform, viewLayer);
        saveLayer3 = startObjects[3].layer;
        setLayer(startObjects[3].transform, viewLayer);
        saveLayer4 = startObjects[4].layer;
        setLayer(startObjects[4].transform, viewLayer);

        // Set up the text
        hud.setMessage(masterText);
        canvas = GameObject.Find("[HudCanvas]").GetComponent<Text>();
        defaultColor = canvas.color;
        defaultFont = canvas.fontSize;
        canvas.color= Color.white;
        canvas.fontSize = 55;
        hud.ForceShowMessage();
        avatarHUD = avatar.GetComponent<HUD>();
        avatarHUD.SecondsToShow = 9999;
        avatarHUD.GeneralDuration = 9999;

        log.log("Practice\t" + startObjects[0].name, 1);
    }

    //public override void TASK_ADD(GameObject go, string txt)
    //{
    //    if (txt == "add")
    //    {
    //        saveLayer = go.layer;
    //        setLayer(go.transform, viewLayer);
    //    }
    //    else if (txt == "remove")
    //    {
    //        setLayer(go.transform, saveLayer);
    //    }

    //}

    public void returnCurrent()
    {
        startObjects[0].transform.position = position0;
        startObjects[0].transform.localRotation = rotation0;
        startObjects[0].transform.localScale = scale0;
        setLayer(startObjects[0].transform, saveLayer0);

        startObjects[1].transform.position = position1;
        startObjects[1].transform.localRotation = rotation1;
        startObjects[1].transform.localScale = scale1;
        setLayer(startObjects[0].transform, saveLayer1);

        startObjects[2].transform.position = position2;
        startObjects[2].transform.localRotation = rotation2;
        startObjects[2].transform.localScale = scale2;
        setLayer(startObjects[0].transform, saveLayer2);

        startObjects[3].transform.position = position3;
        startObjects[3].transform.localRotation = rotation3;
        startObjects[3].transform.localScale = scale3;
        setLayer(startObjects[0].transform, saveLayer3);

        startObjects[4].transform.position = position4;
        startObjects[4].transform.localRotation = rotation4;
        startObjects[4].transform.localScale = scale4;
        setLayer(startObjects[0].transform, saveLayer4);

        // turn off the objects
        startObjects[0].SetActive(false);
        startObjects[1].SetActive(false);
        startObjects[2].SetActive(false);
        startObjects[3].SetActive(false);
        startObjects[4].SetActive(false);
    }
    public override void endTask()
    {
        //returnCurrent();
        //startObjects.current = 0;
        TASK_END();

    }


    private string GetError()
    {
        var c = 0;
        for (int i = 0; i < 5; i++)
        {
            if (resp[i] != ogOrder[i]) { c++; }
        }

        return c.ToString();
    }


    public override void TASK_END()
    {

        base.endTask();

        var output = GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>();
        output.seqOutput.Write(GetError());
        output.seqOutput.Flush();
        output.AddSeqData();

        if (vrEnabled)
        {
            hud.hudPanel.transform.position = initialHUDposition;
        }
        else
        {
            // Change the anchor points to put the message back in center
            RectTransform hudposition = hud.hudPanel.GetComponent<RectTransform>() as RectTransform;
            hudposition.pivot = new Vector2(0.5f, 0.5f);
        }

        //turn on all targets
        //foreach (GameObject item in startObjects.objects)
        //{
        //    item.SetActive(true);
        //}

        // Reset the text
        canvas.color = defaultColor;
        canvas.fontSize = defaultFont;
        avatarHUD.SecondsToShow = 0;
        avatarHUD.GeneralDuration = 0;

        if (restrictMovement)
        {
            manager.player.GetComponent<CharacterController>().enabled = true;
            if (avatar.GetComponent<FirstPersonController>()) avatar.GetComponent<FirstPersonController>().enabled = true;
            manager.scaledPlayer.GetComponent<ThirdPersonCharacter>().immobilized = false;
        }
    }

    public void setLayer(Transform t, int l)
    {
        t.gameObject.layer = l;
        foreach (Transform child in t)
        {
            setLayer(child, l);
        }
    }
}
