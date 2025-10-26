using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabaBoss : MonoBehaviour
{
    public float duracion = 3f;
    public float factorRalentizacion = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Ralentizar(other.GetComponent<Player>()));
        }
    }

    IEnumerator Ralentizar(Player player)
    {
        if (player == null) yield break;

        player.velocidad *= factorRalentizacion;
        yield return new WaitForSeconds(duracion);
        player.velocidad /= factorRalentizacion;
    }
}
