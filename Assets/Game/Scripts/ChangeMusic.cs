using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    //VARIABLES

    [Tooltip("Música que queremos que suene")]
    public AudioClip[] canciones;
    public int numeroCancion;

    public void CambiarMusica()
    {
        // Cambiar música de la zona
        if (canciones != null && MusicManager.instance != null)
        {
            Debug.Log("Se cambia la musica");
            MusicManager.instance.PlayMusic(canciones[numeroCancion], 1f);
        }
    }
}
