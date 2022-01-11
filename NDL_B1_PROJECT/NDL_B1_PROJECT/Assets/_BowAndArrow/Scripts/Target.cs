using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject spherePrefab;
    [SerializeField] Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     //   if(Input.GetKeyDown(KeyCode.Space))
     //       SpawnBall();
    }

    private void OnTriggerEnter2D (Collider2D col){
        SpawnBall();
    }
    public void SpawnBall()
    {
        Instantiate(spherePrefab, spawnPoint.position, Quaternion.identity);
    }
}
