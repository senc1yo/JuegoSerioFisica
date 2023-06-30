using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gravitation
{
    public class AddPoints : MonoBehaviour
    {
        [SerializeField] private GameObject winMenu;
        [SerializeField] private TMP_Text _textFinal;
        [SerializeField]
        private TMP_Text text;
        static int points = 0;
        public bool hasWon;
        string sceneName;
        private void Start()
        {
            points = 0;
            sceneName = SceneManager.GetActiveScene().name;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                points += 1;
                text.text = points.ToString() + " estrellas recogidas";
                Destroy(this.gameObject);
                
                if (points >= 2 && sceneName == "GravitationTuto")
                {
                    if(winMenu != null) winMenu.SetActive(true);
                }
                if (points <= 2 && sceneName == "Gravitation")
                {
                    _textFinal.text = "¡No está mal! Sigue intentándolo, seguro que puedes conseguir más";
                }
                if (points == 3 && sceneName == "Gravitation")
                {
                    _textFinal.text = "Ni tan mal. 3 estrellas está muy bien .Sigue intentándolo, seguro que puedes conseguir más";
                }

                if (points > 3 && sceneName == "Gravitation")
                {
                    _textFinal.text = "WoW, que pasada. " +points + " estrellas está muy bien";
                }
                
                if (points > 5 && sceneName == "Gravitation")
                {
                    _textFinal.text = "Eres increíble. " +points + " estrellas es una puntuación increíble";
                }

            }
        }
    }
}
