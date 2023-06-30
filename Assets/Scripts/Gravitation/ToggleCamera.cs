using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject cam_side, cam_up;

    private bool camSideActive = true;
  
    void Update()
    {
        ToggleCam();
    }

    void ToggleCam()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (camSideActive)
            {
                cam_up.SetActive(true);
                cam_side.SetActive(false);
                camSideActive = false;
            }
            else
            {
                cam_side.SetActive(true);
                cam_up.SetActive(false);
                camSideActive = true;
            }
        }
    }
}
