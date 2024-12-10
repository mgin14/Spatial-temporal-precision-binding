using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToItem : MonoBehaviour
{
    private GameObject avatar;
    private Vector3 itemLocation;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GetComponent<ExperimentTask>().avatar;
        itemLocation = GetComponent<NavigationTask>().currentTarget.transform.position;
        avatar.transform.position = itemLocation;
        Vector3 temp = new Vector3(avatar.transform.position.x + 11f, 
            avatar.transform.position.y, 
            avatar.transform.position.z);
        avatar.transform.position = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
