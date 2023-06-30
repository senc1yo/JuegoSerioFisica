using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gravitation
{
    public class ThrowPlanet : MonoBehaviour
    {
        public float speed = 0.16f; // La velocidad máxima del coche
        public GameObject arrowPrefab; // El prefab de la flecha

        public Vector2 velocity; // La velocidad actual del coche
        public Vector2 forceDirection; // La dirección de la fuerza aplicada
        private GameObject _arrow; // La instancia de la flecha
        private bool _dragging; // Si el usuario está arrastrando el ratón o el dedo
        [SerializeField] private Camera cameraSide, cameraUp;
        [SerializeField]
        private Attractor attractor;
    
        [SerializeField]
        private TMP_Text textX;
        [SerializeField]
        private TMP_Text textY;

        private bool hasCrashed = false;

        private Rigidbody rb;
        private Vector3 worldPosition;

        private GameManagerGravitation _gameManagerGravitation;
        [SerializeField] private int movesLeft = 3;
        [SerializeField] TMP_Text movesLeftText;

        private void OnEnable()
        {
            _gameManagerGravitation = FindObjectOfType<GameManagerGravitation>();
            speed = _gameManagerGravitation.velocityProyectile;

        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            velocity = Vector2.zero;
            forceDirection = Vector2.zero;
            _dragging = false;
            attractor = GetComponent<Attractor>();
        }

        private void Update()
        {
            if(hasCrashed) return;
            if(movesLeft == 0) return;
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
                    if (touch.phase == TouchPhase.Began && !EventSystem.current.currentSelectedGameObject) // Si el toque ha empezado
                    {
                        CreateArrow(); // Crea la flecha
                        _dragging = true; // Establece el estado de arrastre a verdadero
                    }
                    else if (touch.phase == TouchPhase.Ended && !EventSystem.current.currentSelectedGameObject) // Si el toque ha terminado
                    {
                        DestroyArrow(); // Destruye la flecha
                        Simulate(); // Simula el movimiento del coche
                        _dragging = false; // Establece el estado de arrastre a falso
                        attractor.enabled = true;
                        movesLeft -= 1;
                        movesLeftText.text = "Te quedan " + movesLeft + " cambios en la dirección de la fuerza";
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
                    attractor.enabled = true;
                    movesLeft -= 1;
                    movesLeftText.text = "Te quedan " + movesLeft + " cambios en la dirección de la fuerza";
                }
            }
        }

        private void CreateArrow()
        {
            _arrow = Instantiate(arrowPrefab);
            _arrow.transform.position = transform.position;
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
            _arrow.transform.position = transform.position;

            worldPosition = cameraSide.enabled ? cameraSide.ScreenToWorldPoint(inputPosition) : cameraUp.ScreenToWorldPoint(inputPosition);

            float x = worldPosition.x - transform.position.x; // La diferencia en el eje x
            float y = worldPosition.y - transform.position.y; // La diferencia en el eje y
            Vector2 direction = new Vector2(x, y); // Calcula la dirección de la fuerza como un vector con la diferencia
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calcula el ángulo de la fuerza en grados
            _arrow.transform.rotation = Quaternion.Euler(0, 0, angle); // Rota la flecha según el ángulo de la fuerza
            forceDirection = direction.normalized; // Guarda la dirección de la fuerza normalizada
        }

        private void DestroyArrow()
        {
            Destroy(_arrow); // Destruye la instancia de la flecha
            _arrow = null; // Borra la referencia a la flecha
        }

        private void Simulate()
        {
        
            Vector2 force = (forceDirection * speed);
            rb.AddForce(force);
        
            //UpdateUI();
        }

        public void RestartLvl()
        {
            SceneManager.LoadScene(0);
        }

        private void UpdateUI()
        {
            textX.text = "Velocidad X: " + (velocity.x * 100).ToString("F2") + " m/s";
            textY.text = "Velocidad Y: " + (velocity.y * 100).ToString("F2") + " m/s";
        }

       
    }
}
