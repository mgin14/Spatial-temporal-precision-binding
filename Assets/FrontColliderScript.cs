using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontColliderScript : MonoBehaviour
{

    private NavigationTask cur_tar;
    private string cur_des;
    private MeshRenderer cur_mesh;

    // Start is called before the first frame update
    void Start()
    {
        cur_tar = GameObject.FindGameObjectWithTag("tar_obj").GetComponent<NavigationTask>();
        //cur_mesh = cur_tar.currentTarget.GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This function will show the target item at its designated spot in the hallway when the collider is reached
    // other: 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" ___________________________INSIDE TIRIGGER: " + gameObject.name);

        GameObject location = GameObject.Find("ReadTrialInfo").GetComponent<readBlockInfo>().CurrentLocation();
        cur_des = location.tag;
        
        Debug.Log(" Des tag " + cur_des);

        if (GameObject.Find("NavigationTrials").GetComponent<TaskList>().currentTask.name == "Navigate" && cur_des == gameObject.tag)
        {
            GameObject.Find("TrackTargets").GetComponent<LM_TrackTargets>().AddLocation(location);
            Debug.Log("++++++++Activate object");
            cur_tar.currentTarget.SetActive(true);
            GameObject.Find("Navigate").GetComponent<NavigationTask>().collider = gameObject; // We need this for SpatialRetrievalTask
            //cur_tar.GetComponent<MeshRenderer>().enabled= true;
        }
    }

}
