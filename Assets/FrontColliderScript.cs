/* Created by Melanie Gin 12/2024
 * 
 * This script is attached to the invisible colliders that get triggered to show the hidden object.
 * It also adds the object and its location to TrackTarget GO in order to grab the current three objects.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontColliderScript : MonoBehaviour
{

    private NavigationTask cur_tar;
    private NavigationTask cur_tar_st;
    private NavigationTask cur_tar_seq;
    private string cur_des;
    public int objNum; // we will use this variable during the seq block to grab the next 
    private ObjectList targList;

    // Start is called before the first frame update
    void Start()
    {
        cur_tar = GameObject.FindGameObjectWithTag("tar_obj").GetComponent<NavigationTask>(); // currently it is Navigate
        cur_tar_st = GameObject.Find("ST_Navigate").GetComponent<NavigationTask>();
        cur_tar_seq = GameObject.Find("Seq_Navigate").GetComponent<NavigationTask>();
        targList = GameObject.Find("ListNavigationTargets").GetComponent<ObjectList>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // This function will show the target item at its designated spot in the hallway when the collider is reached
    // other: 
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(" ___________________________INSIDE TIRIGGER: " + gameObject.name);
        //Debug.Log(" Time: " + (Time.time - GameObject.Find("Navigate").GetComponent<NavigationTask>().pressTime).ToString());

        // For the first part of the project we want to activate the items one at a time
        if (GameObject.Find("NavigationTrials").GetComponent<TaskList>().currentTask != null && GameObject.Find("NavigationTrials").GetComponent<TaskList>().currentTask.name == "Navigate")
        {
            GameObject location = GameObject.Find("ReadTrialInfo").GetComponent<readBlockInfo>().CurrentLocation();
            cur_des = location.tag;
            //Debug.Log(" Des tag " + cur_des);

            if (cur_des == gameObject.tag)
            {
                GameObject.Find("TrackTargets").GetComponent<LM_TrackTargets>().AddLocation(location);
                //Debug.Log("++++++++Activate object");
                cur_tar.currentTarget.SetActive(true);
                GameObject.Find("Navigate").GetComponent<NavigationTask>().collider = gameObject; // We need this for SpatialRetrievalTask
                //cur_tar.GetComponent<MeshRenderer>().enabled= true;
            }

        }
        // this is for the second part where we want to track the items and their location during sequence
        else if (GameObject.Find("ST_NavigationTrials").GetComponent<TaskList>().currentTask && GameObject.Find("ST_NavigationTrials").GetComponent<TaskList>().currentTask.name == "ST_Navigate")
        {
            int trial = GameObject.Find("TASK_SpaceTime").GetComponent<TaskList>().repeatCount -1;
            if (gameObject.tag == "front") { objNum = 0; }
            else if (gameObject.tag == "mid") { objNum = 1; }
            else { objNum = 2; }
            GameObject seqLocation = GameObject.Find("ReadTrialInfo").GetComponent<readBlockInfo>().CurrentSeq(trial, objNum);

            var trackTar = GameObject.Find("ST_TrackTargets").GetComponent<LM_TrackTargets>();
            trackTar.AddLocation(seqLocation);
            //Debug.Log("++++++++Activate space time object");
            cur_tar_st.currentTarget.SetActive(true);
            trackTar.AddTarget(cur_tar_st.currentTarget);
            targList.incrementCurrent();
            cur_tar_st.UpdateCurrent();
        }
        // This else if is for the sequence block where we just want to track the items that are seen
        else if (GameObject.Find("Seq_NavigationTrials").GetComponent<TaskList>().currentTask &&
            GameObject.Find("Seq_NavigationTrials").GetComponent<TaskList>().currentTask.name == "Seq_Navigate")
        {
            
            var trackTar = GameObject.Find("Seq_TrackTargets").GetComponent<LM_TrackTargets>();
            //Debug.Log("++++++++Activate sequence object");
            cur_tar_seq.currentTarget.SetActive(true);
            trackTar.AddTarget(cur_tar_seq.currentTarget);
            targList.incrementCurrent();
            cur_tar_seq.UpdateCurrent();
        }
    }

}
