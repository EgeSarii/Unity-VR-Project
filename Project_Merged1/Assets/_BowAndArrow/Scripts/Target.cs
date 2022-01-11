using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject spherePrefab;
    [SerializeField] Transform spawnPoint;

    void OnTriggerEnter (Collider col){
        if(col.gameObject.name == ("Arrow"))
        {
             SpawnBall();
        }
    }
    public void SpawnBall()
    {
        Instantiate(spherePrefab, spawnPoint.position, Quaternion.identity);
        spherePrefab.GetComponent<Rigidbody>().velocity = new Vector3(50,50,50);
    }
}
  