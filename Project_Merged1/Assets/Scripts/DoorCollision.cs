using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void onCollisionEnter (Collision col)
    {
        
        if (col.gameObject.tag ==("Door"))
        {
            print("hello");
           SceneManager.LoadScene(1);
        }
   }
}
