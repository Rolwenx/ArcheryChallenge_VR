using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingManager : MonoBehaviour
{
    public GameObject targetPrefab; // Prefab for the target
    public float[] timers = { 15f, 20f, 25f }; // Time limits for each level
    public int[] targetsPerLevel = { 1, 3, 6 }; // Number of targets per level

    private int currentLevel = 0; // Current level index
    private int attempts = 0; // Number of attempts made

    private Vector3 initialSpawnPosition = new Vector3(-258.4f, 0.794f, 53.75f);
    private float spacing = 2f;

    void Start()
    {
        GameManager.Instance.ResetScore();
        StartTraining();
    }

    void StartTraining()
    {
        StartCoroutine(TrainingRoutine());
    }

    IEnumerator TrainingRoutine()
    {
        while (currentLevel < timers.Length)
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log($"Level: {currentLevel}, Attempt: {i + 1}");

                // Spawn targets for the current level
                SpawnTargets(targetsPerLevel[currentLevel]);

                // Wait for the designated time
                yield return new WaitForSeconds(timers[currentLevel]);

                // Increment attempts
                attempts++;

                // Reload scene to reset after timer
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                yield return null; // Wait for the scene to reload
            }
            currentLevel++;
        }

        // Restart the training scene after completing all levels
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SpawnTargets(int numberOfTargets)
    {
        // Calculate the initial position based on the current level and the number of targets
        Vector3 spawnPosition = initialSpawnPosition;

        for (int i = 0; i < numberOfTargets; i++)
        {
            // Spawn the target at the calculated position
            GameObject newTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);

            // Set the tag for the new target
            newTarget.tag = "Target";

            // Move the spawn position for the next target
            spawnPosition.x += spacing; // Increment the x position to place the next target beside the previous one
        }

        Debug.Log($"Spawned {numberOfTargets} targets at level {currentLevel}");
    }
}
