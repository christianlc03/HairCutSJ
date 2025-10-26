using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : MonoBehaviour
{
    //VARIABLES
    public float tiempoCountdownDanos = 1f;
    private bool puedeAtacar = true;
    private EnemyController enemyController;

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
                player.RecibirDanoAranazo();
                Debug.Log("Enemigo hizo daño (trigger)");

                StartCoroutine(CooldownAtaque());

                
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
