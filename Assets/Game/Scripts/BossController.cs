using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    private GameObject player;
    private Rigidbody2D rb;

    [Header("Ataque")]
    public float distanciaDeAtaque = 5f; // rango para que ataque
    public float rangoMordisco = 5f;   // rango específico del mordisco
    public float cooldownAtaque = 1f;
    private bool puedeAtacar = true;

    [Header("Prefabs")]
    public GameObject babaPrefab;
    public Transform puntoBaba;
    public GameObject perritoPrefab;
    public Transform spawnPerritosPoint;

    [Header("Vida")]
    public int vidaMax = 20;
    private int vidaActual;
    public Slider barraVida;

    void Start()
    {
        player = GameObject.Find("Personaje");
        rb = GetComponent<Rigidbody2D>();
        vidaActual = vidaMax;
        rb.isKinematic = true;

        if (barraVida != null)
        {
            barraVida.maxValue = vidaMax;
            barraVida.value = vidaActual;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Siempre seguir al jugador salvo cuando esté atacando
        if (puedeAtacar)
        {
            float distancia = Vector2.Distance(transform.position, player.transform.position);

            if (distancia <= distanciaDeAtaque)
            {
                StartCoroutine(ElegirAtaque());
                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 dir = (player.transform.position - transform.position).normalized;
                rb.velocity = dir * speed;

                if (dir.x < 0)
                    transform.localScale = new Vector3(1, 1, 1);
                else if (dir.x > 0)
                    transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    IEnumerator ElegirAtaque()
    {
        puedeAtacar = false;
        rb.velocity = Vector2.zero;

        int ataque = Random.Range(0, 3); // ataque aleatorio: 0=baba, 1=mordisco, 2=horda

        switch (ataque)
        {
            case 0:
                AtaqueBaba();
                break;
            case 1:
                AtaqueMordisco();
                break;
            case 2:
                AtaqueHorda();
                break;
        }

        yield return new WaitForSeconds(cooldownAtaque);
        puedeAtacar = true;
    }

    void AtaqueBaba()
    {
        Debug.Log("AtaqueBaba");
        Instantiate(babaPrefab, puntoBaba.position, Quaternion.identity);
        
    }


    void AtaqueMordisco()
    {
        Debug.Log("AtaqueMordisco");

        float distancia = Vector2.Distance(transform.position, player.transform.position);
        if (distancia <= rangoMordisco)
        {
            player.GetComponent<Player>()?.RecibirDanoAranazo();
            Debug.Log("Player recibió daño del mordisco");
        }
    }

    void AtaqueHorda()
    {
        Debug.Log("AtaqueHorda");

        // Solo un perrito
        Instantiate(perritoPrefab, spawnPerritosPoint.position, Quaternion.identity);
    }

    public void RecibirDano()
    {
        vidaActual -= 1;
        if (barraVida != null)
            barraVida.value = vidaActual;

        if (vidaActual <= 0)
            Morir();
    }

    void Morir()
    {
        SceneManager.LoadScene("HasGanado");
    }

    public  void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeAtaque);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoMordisco);
    }
}