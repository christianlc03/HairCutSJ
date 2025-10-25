using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //VARIABLES

    [Header("Referencia Player")]
    private GameObject player;
    private Vector2 playerPos;

    [Header("Valores Enemigo")]
    public float speed;
    public float distanciaDeCerca;
    public bool CountdownDano;
    public float tiempoCountdownDano;
    public bool persecucion;

    [Header("Tipos de enemigos")]
    public bool EnemigoMolesto;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Personaje");
    }

    // Update is called once per frame
    void Update()
    {
        //Convertimos la posicion del player a un Vector2
        playerPos = player.transform.position;
        //Convertimos la posicion del enemigo a un Vector2
        Vector2 enemyPos = transform.position;
        //Le decimos la direcion a la que esta el player
        Vector2 direction = playerPos - enemyPos;
        //Te dice a la distancia a la que esta del player
        float distance = direction.magnitude;

        direction.Normalize();

        if (persecucion)
        {
            if (distance > distanciaDeCerca)
            {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
        }
        
        
    }

    IEnumerator CountDownAtaque()
    {
        //Esta esperando a que pueda recibir daño
        if (!CountdownDano)
        {
            yield return new WaitForSeconds(tiempoCountdownDano);
            CountdownDano = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            persecucion = false;

            if (player != null)
            {
                if (CountdownDano)
                {
                    player.RecibirDanoAranazo();
                    CountdownDano = false;
                    StartCoroutine(CountDownAtaque());
                    Debug.Log("Miau");
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            persecucion = true;
        }
    }
}