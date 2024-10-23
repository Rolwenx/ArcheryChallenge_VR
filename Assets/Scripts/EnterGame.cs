using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter_Game(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectDifficulty");

    }
}
