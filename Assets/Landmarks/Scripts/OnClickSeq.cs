/* Created by Melanie Gin 1/2025
 * 
 * This script will change the color of the object when clicked during the sequence retrieval block
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSeq : MonoBehaviour
{
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("LM_Experiment").GetComponent<spatialTemporalOutput>().seqBuffer += gameObject.name + ',';
            GameObject.Find("SequenceRetrieval").GetComponent<SequenceRetrieval>().resp.Add(gameObject);
            gameObject.SetActive(false);
        }
    }
}
