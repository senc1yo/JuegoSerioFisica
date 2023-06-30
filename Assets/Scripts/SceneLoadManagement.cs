using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManagement : MonoBehaviour
{
    [SerializeField] private GameObject MenuDinamica, MainMenu;
    public void SceneLoadGravitation()
    {
        SceneManager.LoadScene("Gravitation");
    }
    public void SceneLoadCarPhysics()
    {
        SceneManager.LoadScene("CarPhysics");
    }
    public void SceneLoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SceneLoadTutorial()
    {
        SceneManager.LoadScene("GravitationTuto");
    }
    public void ShowDinamicaMenu()
    {
        MenuDinamica.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        MenuDinamica.SetActive(false);
       
    }
}
