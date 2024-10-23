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
        
        SceneManager.LoadScene("targets");
    }

    public void Medium()
    {
        PlayerPrefs.SetString("level", "medium");
        SceneManager.LoadScene("targets");
    }

    public void Hard()
    {
        PlayerPrefs.SetString("level", "hard");
        SceneManager.LoadScene("targets");
    }

}
