using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimateLogoLogic : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;
    
    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene() {
        Color color = rawImage.color;
        color.a = 0f;

        rawImage.color = color;

        float opacidadInicial = 0f; // empieza en cero
        float opacidadFinal = 1f; // termina en uno
        float tiempo = 0f; // empieza en cero
        
        // mientras el tiempo sea menor que uno
        while (tiempo < 2f) {
            // aumenta el tiempo cada frame según la velocidad de los frames
            tiempo += Time.deltaTime;
            // interpola la opacidad entre el valor inicial y el final según el tiempo
            float opacidad = Mathf.Lerp(opacidadInicial, opacidadFinal, tiempo);
            // asigna la opacidad al color
            color.a = opacidad;
            // asigna el color a la imagen
            rawImage.color = color;
            // espera al siguiente frame
            yield return null;
        }
        
            tiempo = 0f; // reinicia el tiempo
        
         // mientras el tiempo sea menor que uno
         while (tiempo < 2f) {
             // aumenta el tiempo cada frame según la velocidad de los frames
             tiempo += Time.deltaTime;
             // interpola la opacidad entre el valor final y el inicial según el tiempo
             float opacidad = Mathf.Lerp(opacidadFinal, opacidadInicial, tiempo);
             // asigna la opacidad al color
             color.a = opacidad;
             // asigna el color a la imagen
             rawImage.color = color;
             // espera al siguiente frame
             yield return null;
         }
        
         // inicia la carga asíncrona de la escena Main
         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
         // mientras la escena no esté completamente cargada
         while (!asyncLoad.isDone) {
             // espera al siguiente frame
             yield return null;
         }
    }

}
