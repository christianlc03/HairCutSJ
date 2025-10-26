using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaneraSpawner : MonoBehaviour
{
    //VARIABLES

    private GameObject player;
    public GameObject baneraPrefabs;

    public Transform[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Personaje");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.SpawnearBanera == true)
        {
            Transform spawnPosT = spawns[Random.Range(0, spawns.Length)];
            Vector2 spawnPos = spawnPosT.position;
            Instantiate(baneraPrefabs, spawnPos, baneraPrefabs.transform.rotation = Quaternion.Euler(0f, 0f, 0f));
            GameManager.Instance.DesactivarSpawnearBanera();
        }
    }
}
