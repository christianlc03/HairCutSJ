using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    //VARIABLES

    private GameObject player;
    public GameObject[] catPrefabs;
    private int catIndex;

    //Puntos de spawn
    public Transform[] spawns;

    //Parametros del spawn aleatorio
    public float startDelay = 2f;
    public float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Personaje");
        InvokeRepeating("SpawnRandomCat", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    void SpawnRandomCat()
    {
        int catIndex = Random.Range(0, catPrefabs.Length);
        Transform spawnPosT = spawns[Random.Range(0, spawns.Length)];
        Vector2 spawnPos = spawnPosT.position;
        Instantiate(catPrefabs[catIndex], spawnPos, catPrefabs[catIndex].transform.rotation = Quaternion.Euler(0f, 0f, 0f));
    }
}
