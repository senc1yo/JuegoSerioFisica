using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioControler : MonoBehaviour
{
    public static AudioControler instance = null; //Instancia estática del controlador de audio
    public AudioClip clip; //El clip de audio que quieres reproducir
    private AudioSource source; //El componente AudioSource

    void Awake()
    {
        //Comprobar si ya existe una instancia del controlador de audio
        if (instance == null)
        {
            //Si no existe, asignar esta instancia
            instance = this;
        }
        else if (instance != this)
        {
            //Si ya existe otra instancia, destruir esta
            Destroy(gameObject);
        }

        //Evitar que este GameObject se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);

        //Obtener el componente AudioSource y asignarle el clip
        source = GetComponent<AudioSource>();
        source.clip = clip;
    }

    void Update()
    {
        /*//Obtener el nombre de la escena actual
        string sceneName = SceneManager.GetActiveScene().name;

        //Reproducir o detener el sonido según la escena
        if (sceneName == "Loading" || sceneName == "MainMenu")
        {
            //Si la escena es la 1 o la 2, reproducir el sonido si no está sonando
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else
        {
            //Si la escena es otra, detener el sonido si está sonando
            if (source.isPlaying)
            {
                source.Stop();
            }
        }*/
    }
}