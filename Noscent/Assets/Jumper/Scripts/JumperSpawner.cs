using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperSpawner : MonoBehaviour
{

    public GameObject prefab;
    public GameObject lowSpawn;


    // Start is called before the first frame update
    void Start()
    {
        Spawn(lowSpawn);
    }

    public void Spawn(GameObject spawnLocation)
    {
        Instantiate(prefab, new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
