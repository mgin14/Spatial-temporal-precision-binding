/* Created by Melanie Gin 12/2024
 * 
 * 
 * This script will be the spatial retrieval task where the subject is placed in the hall way 
 * and will have to click on the screen where they saw the object.
 * Only the z and y values will be used for calculation errors because we don't care where on the x-axis
 * (aka how far in the hallway) they viewed the object. Just the height and if it was close to the wall or not.
 */
 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SpatialRetrievalTask : ExperimentTask
{
    private NavigationTask cur_tar;
    private Vector3 itemLocation;
    // Start is called before the first frame update
    public override void startTask()
    {
        TASK_START();
    }

    public override void TASK_START()
    {
        if (!manager) Start();
        base.startTask();


        GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = false;
        cur_tar = GameObject.FindGameObjectWithTag("tar_obj").GetComponent<NavigationTask>();
        
        // Move player to the location where they viewed the item
        itemLocation = cur_tar.transform.position;
        cur_tar.prevTarget.SetActive(false);
        avatar.transform.position = itemLocation;
        Vector3 temp = new Vector3(avatar.transform.position.x + 11f,
            avatar.transform.position.y,
            avatar.transform.position.z);
        avatar.transform.position = temp;

        hud.showEverything();
    }

    public override bool updateTask()
    {
        // The subject clicked on the screen that they think the object was
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log(Input.mousePosition.z);
            Debug.Log(Input.mousePosition.y);
            GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = true;
            return true;
        }
        return false;
    }
    public override void endTask()
    {
        TASK_END();
    }

    public override void TASK_END()
    {
        base.endTask();
    }
}
