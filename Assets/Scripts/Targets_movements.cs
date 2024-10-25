using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Targets_movements : MonoBehaviour
{
    public String level; // Niveau de difficulté : 1 = facile, 2 = moyen, 3 = difficile <------ TO REACTIVATE
    public float speed = 2f; // Vitesse de base pour le mouvement
    public float distance = 5f; // Distance pour l'aller-retour

    private Vector3 startPosition;
    private float timeCounter = 0;

    //public String level = "easy"; // Niveau de difficulté par défaut

    void Start()
    {
        // Enregistre la position initiale de la cible
        startPosition = transform.position;

        // Récupérer le niveau de difficulté depuis PlayerPrefs
        level = PlayerPrefs.GetString("level"); // Par défaut "facile" (niveau 1)  <----- TO REACTIVATE
    }

    void Update()
    {
        // Mise à jour du mouvement en fonction du niveau de difficulté
        MoveTarget(level);
    }

    void MoveTarget(String level)
    {
        // Compteur de temps basé sur la vitesse
        timeCounter += Time.deltaTime * speed;

        switch (level)
        {
            case "easy": // Facile - Aller-retour sur un axe X
                // Utilisation de PingPong pour faire un aller-retour sur l'axe X
                float posX = Mathf.PingPong(timeCounter * speed, distance);
                transform.position = startPosition + new Vector3(posX, 0, 0);
                break;

            case "medium": // Moyen - Aller-retour en cercle
                float x = Mathf.Cos(timeCounter) * distance;
                float y = Mathf.Sin(timeCounter) * distance;
                transform.position = startPosition + new Vector3(x, y, 0);
                break;

            case "hard": // Difficile - Aller-retour aléatoire
                // Déplacement aléatoire entre deux points avec Perlin Noise, mais lissé pour des allers-retours
                float randomX = Mathf.PingPong(Mathf.PerlinNoise(timeCounter, 0) * distance, distance);
                float randomY = Mathf.PingPong(Mathf.PerlinNoise(0, timeCounter) * distance, distance);
                transform.position = startPosition + new Vector3(randomX, randomY, 0);
                break;

            default:
                break;
        }
    }
}
