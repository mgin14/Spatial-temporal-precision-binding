using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndofHaLL : MonoBehaviour
{

    public static bool reachedEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        var navTr = GameObject.Find("NavigationTrials").GetComponent<TaskList>().currentTask;
        var ST_navTr = GameObject.Find("ST_NavigationTrials").GetComponent<TaskList>().currentTask;
        var Seq_navTr = GameObject.Find("Seq_NavigationTrials").GetComponent<TaskList>().currentTask;
        // We need this if statement because MovePlayerSpawn would start at the end where they left off 
        // and trigger this again and it would skip the navigation
        if ( (navTr != null && navTr.name == "Navigate") 
            || (ST_navTr != null && ST_navTr.name == "ST_Navigate")
            || (Seq_navTr != null && Seq_navTr.name == "Seq_Navigate")
            )
        {
            reachedEnd = true;
        }
        
    }
}
