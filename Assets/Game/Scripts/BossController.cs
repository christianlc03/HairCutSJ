using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 3f;
    private GameObject player;
    private Rigidbody2D rb;

    [Header("Ataque")]
    public float distanciaDeAtaque = 7f; // rango para que ataque
    public float rangoMordisco = 1.5f;   // rango específico del mordisco
    public float cooldownAtaque = 3f;
    private bool puedeAtacar = true;

    [Header("Prefabs")]
    public GameObject babaPrefab;
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
        Instantiate(babaPrefab, transform.position, Quaternion.identity);
        // Prefab debe aplicar ralentización al jugador
    }

    void AtaqueMordisco()
    {
        Collider2D[] objetosGolpeados = Physics2D.OverlapCircleAll(transform.position, rangoMordisco);
        foreach (Collider2D col in objetosGolpeados)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>()?.RecibirDanoAranazo();
            }
        }
    }

    void AtaqueHorda()
    {
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