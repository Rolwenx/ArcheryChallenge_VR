using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    public int points; // Points awarded for this scoring zone

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow")) 
        {
            GameManager.Instance.AddScore(points); 
            Debug.Log("Hit! Score: " + points);
            Destroy(other.gameObject);
        }
    }
}
