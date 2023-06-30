using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TMP_InputField inputText;
    public Dictionary<string, GameObject> objetos;
    [SerializeField] GameObject balonPrefab;
    [SerializeField] GameObject casaPrefab;
    // Start is called before the first frame update
    void Start()
    {
        inputText.text = "Pide un objeto";
        objetos = new Dictionary<string, GameObject>();
        objetos.Add("balon", balonPrefab);
        objetos.Add("casa", casaPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Detecta si se presiona la tecla "Enter"
        {
            string input = inputText.text.Trim().ToLower(); // Obtiene el texto ingresado y lo convierte a minúsculas
            CreateObject(input);

            inputText.text = "Ingrese el objeto que desea crear"; // Restablece el texto en el objeto de texto

            // Llama a la función "CreateObject" del objeto "GameManager" y pasa el input del usuario como parámetro
            
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            string input = inputText.text.Trim().ToLower(); // Obtiene el texto ingresado y lo convierte a minúsculas
            inputText.text = "Ingrese la fórmula del movimiento rectilíneo acelerado"; // Restablece el texto en el objeto de texto

            // Llama a la función "CalcularMRA" del objeto "GameManager" y pasa la fórmula ingresada por el usuario como parámetro
            CalcularMRA(input);
        }
        
    }
    
    public void CalcularMRA(string formula)
    {
        // Parsea la fórmula ingresada por el usuario para obtener los valores de la posición inicial, velocidad inicial, aceleración y tiempo
        string[] partes = formula.Split(' ');
        float x_i = float.Parse(partes[0]);
        float v_i = float.Parse(partes[1]);
        float a = float.Parse(partes[2]);
        float t = float.Parse(partes[3]);

        // Calcula la posición final y velocidad final de la pelota utilizando la fórmula del movimiento rectilíneo acelerado
        float x_f = x_i + v_i * t + 0.5f * a * t * t;
        float v_f = v_i + a * t;

        // Aplica la posición final y velocidad final a la pelota utilizando el componente "Rigidbody"
        GameObject go = GameObject.FindWithTag("Pelota");
        go.GetComponent<Rigidbody2D>().transform.position = new Vector3(x_f, 0, 0);
        go.GetComponent<Rigidbody2D>().velocity = new Vector3(v_f, 0, 0);
    }

    void CreateObject(string input){
        GameObject nuevoObjeto;

        if(objetos.ContainsKey(input))
        {
            nuevoObjeto = Instantiate(objetos[input], Vector3.zero, Quaternion.identity);
        }

    }
}
