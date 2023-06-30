using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gravitation
{
    public class Attractor : MonoBehaviour
    {
        private const float G = 6.674f;
        public Rigidbody rb;
        [SerializeField]
        public static List<Attractor> Attractors = new List<Attractor>();
[SerializeField]
        private LineRenderer _lineRenderer;
        [SerializeField]
        private TMP_Text forceText;
        public float forceMagnitude;
        public Transform proyectileTransform;

        private void OnEnable()
        {
            Attractors.Add(this);
        }
        private void OnDisable() { Attractors.Remove(this); }
        
        private void FixedUpdate()
        {
            foreach (Attractor attractor in Attractors)
            {
                if(attractor != this && !attractor.CompareTag("Planet"))
                    Attract(attractor);
            }
        }

        void Attract(Attractor objToAttract)
        {
            Rigidbody rbToAttract = objToAttract.rb;

            Vector3 direction = rb.position - rbToAttract.position;
            float distance = direction.magnitude;

            forceMagnitude = G * (rb.mass * rbToAttract.mass / Mathf.Pow(distance, 2));
            Vector3 force = direction.normalized * forceMagnitude;
            
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, proyectileTransform.position);
            
            forceText.text = forceMagnitude.ToString();
            Vector3 point = Vector3.Lerp(transform.position, proyectileTransform.position, 0.75f);
            forceText.transform.position = point + Vector3.up * 0.5f;



            
        
            rbToAttract.AddForce(force);
        }
    }
}
