using UnityEngine;

namespace Gravitation
{
    public class CameraControllerG : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField] private Vector3 offset;
        private float smoothSpeed = 2f;
        private void LateUpdate()
        {
            // Calculamos la posición deseada de la cámara sumando la posición del objeto y el desplazamiento
            Vector3 desiredPosition = target.position + offset;
            // Interpolamos linealmente la posición actual de la cámara con la posición deseada usando la velocidad de interpolación
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Asignamos la posición interpolada a la cámara
            transform.position = smoothedPosition;
        }

    }
}
