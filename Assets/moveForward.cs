using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForward : MonoBehaviour
{
    private LM_PlayerController player;
    private Experiment manager;
    // Start is called before the first frame update
    void Start()
    {
        //manager = FindObjectOfType<Experiment>().GetComponent<Experiment>();
        //player = manager.player.GetComponent<LM_PlayerController>();
        //Debug.Log("++++++++++ This is the gameobject: " + player);
    }

    // Update is called once per frame
    void Update()
    {

        // Move the camera forward
        //player.controller.transform.Translate(Vector3.forward * (Time.deltaTime * 5.0f));
    }
}
