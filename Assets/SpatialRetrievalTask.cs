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
using System.IO;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SpatialRetrievalTask : ExperimentTask
{
    private NavigationTask cur_tar;
    private GameObject item;
    private Vector3 itemLocation;

    // Start is called before the first frame update
    public override void startTask()
    {
        TASK_START();
    }

    public override void TASK_START()
    {
        GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = false;

        if (!manager) Start();
        base.startTask();

        //cur_tar = GameObject.FindGameObjectWithTag("tar_obj").GetComponent<NavigationTask>();

        // Move player to where the triggered collider is
        //GameObject collider = GameObject.Find("Navigate").GetComponent<NavigationTask>().collider;
        //itemLocation = collider.transform.position;
        var currentRepeat = GameObject.Find("Spatial").GetComponent<TaskList>().repeatCount - 1;
        item = GameObject.Find("ChooseTask").GetComponent<LM_ChooseTask>().loc[currentRepeat];
        itemLocation = item.transform.position;
        //cur_tar.prevTarget.SetActive(false);
        var newItemLocation = new Vector3(itemLocation.x - 11, 0.25f, 0);
        avatar.transform.position = newItemLocation;
        avatar.transform.rotation = Quaternion.Euler(0,-90,0);
        //Camera.main.transform.position = avatar.transform.position;
        Camera.main.transform.rotation = avatar.transform.rotation;

        hud.showEverything();
    }

    public override bool updateTask()
    {
        // The subject clicked on the screen that they think the object was
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            var newCoords = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            Debug.Log(newCoords.x);
            Debug.Log(newCoords.y);
            Debug.Log(newCoords.z);
            GameObject.Find("KeyboardMouseController").GetComponent<FirstPersonController>().enabled = true;
            GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>().fileBuffer += item.name + ", " + itemLocation.x + ", " + 
                itemLocation.y + ", " + itemLocation.z + ", "; // DON"T FORGET TO GET RESPONSE COORDS
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
