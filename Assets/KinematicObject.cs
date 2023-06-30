using UnityEngine;
using UnityEngine.SceneManagement;

public class KinematicObject : MonoBehaviour
{
    // El rigidbody2D del objeto
    public Rigidbody2D rb;

    // La posición inicial del objeto
    public Vector2 initialPosition;

    // La velocidad inicial del objeto
    public Vector2 initialVelocity;

    // La aceleración del objeto
    public Vector2 acceleration;

    // El tiempo transcurrido desde el inicio del movimiento
    private float time;

    // Inicializar el rigidbody2D y el tiempo
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        time = 0f;
    }

    // Calcular la posición del objeto usando las fórmulas de la cinemática
    void Update()
    {
        time += Time.deltaTime;
        Vector2 position = initialPosition + initialVelocity * time + 0.5f * acceleration * time * time;
        rb.MovePosition(position);
        
        if(Input.GetKeyDown(KeyCode.R))  SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Manejar la colisión con el boxcollider2D
    void OnCollisionStay2D(Collision2D collision)
    {
        // Si el otro objeto tiene un boxcollider2D
        if (collision.collider is BoxCollider2D)
        {
            Debug.Log("hemos chocao");
            // Cambiar la posición o la velocidad del rigidbody2D
            // Por ejemplo, hacer que rebote con el mismo ángulo de incidencia
            initialVelocity = Vector2.Reflect(initialVelocity, collision.contacts[0].normal);
            initialPosition = rb.position;
            time = 0f;
        }
    }
}