using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float speed = 0.16f; // La velocidad máxima del coche
    public GameObject arrowPrefab; // El prefab de la flecha

    public Vector2 velocity; // La velocidad actual del coche
    public Vector2 forceDirection; // La dirección de la fuerza aplicada
    private GameObject _arrow; // La instancia de la flecha
    private bool _dragging; // Si el usuario está arrastrando el ratón o el dedo
    
    [SerializeField]
    private TMP_Text textX;
    [SerializeField]
    private TMP_Text textY;
    [SerializeField]
    private TMP_Text textMoves;

    private int _movesCount = 0;
    [SerializeField]
    private int _checkCount = 0;
    [SerializeField] private GameObject menuGO;
    private bool hasCrashed = false;
    [SerializeField]
    private GameObject menuStart;
    [SerializeField]
    private GameObject menuFinish;
    [SerializeField]
    private TMP_Text finishText;
    private void Start()
    {
        velocity = Vector2.zero; // Inicializa la velocidad a cero
        forceDirection = Vector2.zero; // Inicializa la dirección de la fuerza a cero
        _dragging = false; // Inicializa el estado de arrastre a falso
        
    }

    private void Update()
    {
        if (menuStart.activeSelf) return;
        if(hasCrashed) return;
        if (_dragging) // Si el usuario está arrastrando el ratón o el dedo
        {
            UpdateArrow(); // Actualiza la flecha
        }
        CheckInput(); // Comprueba los eventos de entrada
    }

    private void CheckInput()
    {
        if (Input.touchSupported) // Si el dispositivo soporta toques
        {
            if (Input.touchCount > 0) // Si hay algún toque en la pantalla
            {
                
                Touch touch = Input.GetTouch(0); // Obtiene el primer toque
                if (touch.phase == TouchPhase.Began  && !EventSystem.current.currentSelectedGameObject) // Si el toque ha empezado
                {
                    CreateArrow(); // Crea la flecha
                    _dragging = true; // Establece el estado de arrastre a verdadero
                }
                else if (touch.phase == TouchPhase.Ended  && !EventSystem.current.currentSelectedGameObject) // Si el toque ha terminado
                {
                    DestroyArrow(); // Destruye la flecha
                    Simulate(); // Simula el movimiento del coche
                    _dragging = false; // Establece el estado de arrastre a falso
                    _movesCount++;
                }
            }
        }
        else 
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
            {
                
                CreateArrow();
                _dragging = true;
            }
            else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                DestroyArrow();
                Simulate();
                _dragging = false;
                _movesCount++;
            }
        }
    }

    private void CreateArrow()
    {
        _arrow = Instantiate(arrowPrefab);
        _arrow.transform.position = transform.position + new Vector3(0,0,-1);
        _arrow.transform.rotation = transform.rotation;
    }

    private void UpdateArrow()
    {
        Vector3 inputPosition;
        if (Input.touchSupported)
        {
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            inputPosition = Input.mousePosition;
        }
        
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition); // Convierte la posición de entrada en una posición del mundo
        
        float threshold = 0.25f; // El umbral de distancia al coche
        float x = worldPosition.x - transform.position.x; // La diferencia en el eje x
        float y = worldPosition.y - transform.position.y; // La diferencia en el eje y
        Vector2 direction = new Vector2(Mathf.Sign(x), Mathf.Sign(y)); // Calcula la dirección de la fuerza como un vector con signo
        if (Mathf.Abs(x) < threshold) // Si la diferencia en el eje x es menor que el umbral
        {
            direction.x = 0f; // Establece la dirección en el eje x a cero
        }
        if (Mathf.Abs(y) < threshold) // Si la diferencia en el eje y es menor que el umbral
        {
            direction.y = 0f; // Establece la dirección en el eje y a cero
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calcula el ángulo de la fuerza en grados
        _arrow.transform.rotation = Quaternion.Euler(0, 0, angle); // Rota la flecha según el ángulo de la fuerza
        forceDirection = direction; // Guarda la dirección de la fuerza
    }

    private void DestroyArrow()
    {
        Destroy(_arrow); // Destruye la instancia de la flecha
        _arrow = null; // Borra la referencia a la flecha
    }

    private void Simulate()
    {
        
        Vector2 force = (forceDirection * speed); // Calcula la fuerza en función de la dirección y la velocidad máxima
        velocity += force;
        transform.position += (Vector3)velocity; // Actualiza la posición en función de la velocidad y el tiempo
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3)velocity); // Actualiza la rotación en función de la dirección de la fuerza

        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Finish")
        {
            menuGO.SetActive(true);
            hasCrashed = true;
        }

        if (other.tag == "CheckPoint")
        {
            _checkCount++;
            if (_checkCount >= 6)
            {
                menuFinish.SetActive(true);
                finishText.text = "Has conseguido superar el circuito en " + _movesCount + " movimientos";
            }
        }
    }

    public void RestartLvl()
    {
        SceneManager.LoadScene("CarPhysics");
    }

    private void UpdateUI()
    {
        textX.text = (velocity.x * 100).ToString("F2") + " m/s";
        textY.text = (velocity.y * 100).ToString("F2") + " m/s";
        textMoves.text = _movesCount.ToString();

    }
}
