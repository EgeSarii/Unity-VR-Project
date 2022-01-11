using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class R2Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter (Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == ("Door"))
        {
           SceneManager.LoadScene(1);
        }
   }
}
