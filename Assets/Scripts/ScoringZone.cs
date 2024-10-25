using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    public int points;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            GameManager.Instance.AddScore(points); // Update score in GameManager
            Debug.Log("Hit! Score: " + points);
        }
    }
}
