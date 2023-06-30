using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoRectilineo : MonoBehaviour
{
    public float velocidadInicial = 10f;
    private float tiempo = 0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        tiempo += Time.deltaTime;
        float posicionActual = transform.position.x + (velocidadInicial * tiempo);
        Vector2 nuevaPosicion = new Vector2(posicionActual, transform.position.y);
        rb.MovePosition(nuevaPosicion);
    }
}
