using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasExistFinish : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            menu.SetActive(true);
        }
    }
}
