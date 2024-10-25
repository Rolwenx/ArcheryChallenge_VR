using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Choose_Difficulty : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void Easy()
    {
        PlayerPrefs.SetString("level", "easy");
        
        SceneManager.LoadScene(3);
    }

    public void Medium()
    {
        PlayerPrefs.SetString("level", "medium");
        SceneManager.LoadScene(3);
    }

    public void Hard()
    {
        PlayerPrefs.SetString("level", "hard");
        SceneManager.LoadScene(3);
    }

    public void Training()
    {
        PlayerPrefs.SetString("level", "training");
        SceneManager.LoadScene(2);
    }

    public void goToScene(int id)
    {
        SceneManager.LoadScene(id);
    }


}
