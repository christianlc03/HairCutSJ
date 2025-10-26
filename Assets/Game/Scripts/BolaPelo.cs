using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaPelo : MonoBehaviour
{
    public float radioAtaque = 5f;       
    public float duracion = 0.5f;        
    public string tagEnemigo = "Enemigo";

    private void Start()
    {
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(transform.position, radioAtaque);
        foreach (Collider2D col in enemigos)
        {
            if (col.CompareTag(tagEnemigo))
            {
                Destroy(col.gameObject); 
            }
        }

        
        Destroy(gameObject, duracion);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
    }
}


