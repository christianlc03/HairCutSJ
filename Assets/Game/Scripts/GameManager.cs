using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //VARIABLES

    public static GameManager Instance;
    public int numBaneras;
    public bool SpawnearBanera = false;

    private void Awake()
    {
        // Si ya hay una instancia y no somos nosotros, nos destruimos
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asignar la instancia y mantenerla entre escenas
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ActivarSpawnearBanera()
    {
        SpawnearBanera = true;
    }

    public void DesactivarSpawnearBanera()
    {
        SpawnearBanera = false;
    }
}
