using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabaBoss : MonoBehaviour
{
    public float duracion = 2f;          // tiempo de ralentización
    public float factorRalentizacion = 0.5f; // velocidad relativa

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                StartCoroutine(RalentizarJugador(player));
            }
        }
    }

    IEnumerator RalentizarJugador(Player player)
    {
        Debug.Log("Relentizado");
        float velocidadOriginal = player.velocidad;
        player.velocidad *= factorRalentizacion;
        yield return new WaitForSeconds(duracion);
        player.velocidad = velocidadOriginal;
    }
}
