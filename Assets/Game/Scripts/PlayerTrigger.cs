using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    //VARIABLES

    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
      player = GetComponentInParent<Player>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Banera"))
        {
            player.Desaparecer();
        }
    }
}
