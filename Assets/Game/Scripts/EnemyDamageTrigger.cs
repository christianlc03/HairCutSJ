using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    //VARIABLES
    public float tiempoCountdownDanos = 1f;
    private bool puedeAtacar = true;
    private EnemyController enemyController;
    public bool gatoEnfermo = false;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Asegúrate de usar el mismo tag que tenga el jugador
        if (other.CompareTag("Player") && puedeAtacar)
        {
            Player player = other.GetComponent<Player>();
            Debug.Log("Estoy haciendo Triggre");

            if (player != null)
            {
                if (!gatoEnfermo)
                {
                    player.RecibirDanoAranazo();
                    Debug.Log("Enemigo hizo daño (trigger)");

                    StartCoroutine(CooldownAtaque());
                }
            }

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Asegúrate de usar el mismo tag que tenga el jugador
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            Debug.Log("Estoy haciendo Triggre");

            if (player != null)
            {
                if (gatoEnfermo)
                {
                    player.ActivarVeneno(0.1f, 5f);
                    enemyController.Contagiar();
                    Debug.Log("Estas envenenado");
                }
            }


        }
    }

    IEnumerator CooldownAtaque()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(tiempoCountdownDanos);
        puedeAtacar = true;
    }
}
