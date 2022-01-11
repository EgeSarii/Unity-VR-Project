using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class R2Collision : MonoBehaviour
{
    // Start is called before the first frame update
    private void onCollisionEnter (Collision col)
    {
        
        if (col.collider.gameObject.name == ("Door"))
        {
            Debug.Log("hello");
           SceneManager.LoadScene("MainScene");
        }
   }
}
