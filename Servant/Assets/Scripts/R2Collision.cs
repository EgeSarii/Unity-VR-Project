using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class R2Collision : MonoBehaviour
{
    // Start is called before the first frame update void onTriggerEnter (Collision col)
    void onTriggerEnter (Collision col)
    {
        if (col.gameObject.name == "R2D2")
        {
           print("hello");
           SceneManager.LoadScene("MainScene");
        }
    }
}
