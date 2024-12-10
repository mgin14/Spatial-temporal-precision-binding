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
        cur_des = GameObject.FindGameObjectWithTag("tar_des").GetComponent<MoveObjects>().cur_des;
        Debug.Log(" Des tag " + cur_des);
        if (GameObject.Find("NavigationTrials").GetComponent<TaskList>().currentTask.name == "Navigate" && cur_des == gameObject.tag)
        {
            Debug.Log("++++++++Activate object");
            cur_tar.currentTarget.SetActive(true);
            //cur_tar.GetComponent<MeshRenderer>().enabled= true;
        }
    }

}
