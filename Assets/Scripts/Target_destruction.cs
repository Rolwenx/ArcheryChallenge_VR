using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target_destruction : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            Destroy(gameObject);
        }
    }

 
}