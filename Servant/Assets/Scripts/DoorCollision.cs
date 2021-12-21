using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void onTriggerEnter (Collision col)
    {
        
        if (col.gameObject.name == "Door")
        {
            print("hello");
           SceneManager.LoadScene("MainScene");
        }
   }
}
