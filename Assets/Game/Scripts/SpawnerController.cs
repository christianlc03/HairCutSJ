using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Tiempo Spawn
    public float duracionMaxima = 90f; // 4 minutos
    private float tiempoTranscurrido = 0f;
    private bool spawneando = true;

    //Instancia Boss
    [Header("Boss")]
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    private bool bossInstanciado = false;

    public Text contadorTexto;

    public string tagEnemigo = "Enemigo";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Personaje");
        InvokeRepeating("SpawnRandomCat", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            transform.position = player.transform.position;

        if (spawneando)
        {
            tiempoTranscurrido += Time.deltaTime;
            float tiempoRestante = Mathf.Clamp(duracionMaxima - tiempoTranscurrido, 0f, duracionMaxima);
            ActualizarContadorUI(tiempoRestante);

            if (tiempoTranscurrido >= duracionMaxima)
            {
                spawneando = false;
                contadorTexto.gameObject.SetActive(false); // Oculta el contador cuando termina
                
                GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigo);
                foreach (GameObject enemigo in enemigos)
                {
                    Destroy(enemigo);
                }
               
                if (!bossInstanciado && bossPrefab != null && bossSpawnPoint != null)
                {
                    Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
                    bossInstanciado = true;
                }
            }
        }
    }

    void SpawnRandomCat()
    {
        if (!spawneando) return;

        int catIndex = Random.Range(0, catPrefabs.Length);
        Transform spawnPosT = spawns[Random.Range(0, spawns.Length)];
        Vector2 spawnPos = spawnPosT.position;
        Instantiate(catPrefabs[catIndex], spawnPos, catPrefabs[catIndex].transform.rotation = Quaternion.Euler(0f, 0f, 0f));
    }

    void ActualizarContadorUI(float tiempoRestante)
    {
        int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
        contadorTexto.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
