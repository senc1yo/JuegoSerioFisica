using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlanetMass : MonoBehaviour
{
    public Slider massSlider;
    private float minMass = 10f;
    private float maxMass = 40;
    public float minScale = 0.5f;
    public float maxScale = 2.5f;
    private Rigidbody rb; // El componente Rigidbody del planeta
    private bool selected; // Si el planeta está seleccionado o no
    [SerializeField] private Camera _camera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtiene el componente Rigidbody
        massSlider.gameObject.SetActive(false); // Oculta el slider
        selected = false; // Establece el estado de selección a falso
    }

    private void Update()
    {
        if (Input.touchSupported) // Si el dispositivo soporta toques
        {
            foreach (Touch touch in Input.touches) // Itera sobre todos los toques en la pantalla
            {
                if (touch.phase == TouchPhase.Began) // Si el toque empieza
                {
                    Ray ray = _camera.ScreenPointToRay(touch.position); // Crea un rayo desde la cámara hasta la posición del toque
                    RaycastHit hit; // Almacena la información del impacto
                    if (Physics.Raycast(ray, out hit)) // Si el rayo impacta con algún objeto
                    {
                        if (hit.collider.gameObject == gameObject) // Si el objeto impactado es este planeta
                        {
                            selected = true; // Establece el estado de selección a verdadero
                            massSlider.gameObject.SetActive(true); // Muestra el slider
                            massSlider.value = rb.mass; // Asigna el valor del slider a la masa actual del planeta
                        }
                        else // Si el objeto impactado es otro
                        {
                            selected = false; // Establece el estado de selección a falso
                            massSlider.gameObject.SetActive(false); // Oculta el slider
                        }
                    }
                }
            }
        }
        else // Si el dispositivo no soporta toques
        {
            if (Input.GetMouseButtonDown(0)) // Si se presiona el botón izquierdo del ratón
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition); // Crea un rayo desde la cámara hasta la posición del ratón
                RaycastHit hit; // Almacena la información del impacto
                if (Physics.Raycast(ray, out hit)) // Si el rayo impacta con algún objeto
                {
                    if (hit.collider.gameObject == gameObject) // Si el objeto impactado es este planeta
                    {
                        selected = true; // Establece el estado de selección a verdadero
                        massSlider.gameObject.SetActive(true); // Muestra el slider
                        massSlider.value = rb.mass; // Asigna el valor del slider a la masa actual del planeta
                    }
                    else // Si el objeto impactado es otro
                    {
                        selected = false; // Establece el estado de selección a falso
                        massSlider.gameObject.SetActive(false); // Oculta el slider
                    }
                }
            }
        }
    }

    public void ChangeMass()
    {
        if (selected) // Si el planeta está seleccionado
        {
            rb.mass = Mathf.Clamp(massSlider.value, minMass, maxMass); // Asigna la masa del planeta al valor del slider limitado por los valores mínimo y máximo
            float scale = Mathf.Lerp(minScale, maxScale, (massSlider.value - minMass) / (maxMass - minMass)); // Calcula la escala del planeta en función de la masa usando una interpolación lineal
            transform.localScale = new Vector3(scale, scale, scale); // Asigna la escala del planeta al vector calculado
        }
    }
}