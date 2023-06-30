using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public float timeToActivate = 2f;

    private float timeInside = 0f;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player"){
            timeInside += Time.deltaTime;
            Debug.Log(timeInside);

            if (!(timeInside < timeToActivate))
            {
                Debug.Log("ha pasao tiempo");
                menu.SetActive(true);
            }
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        timeInside = 0f;
    }
}
