using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuchaController : MonoBehaviour
{
    //VARIABLES

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ducharse()
    {
        anim.SetTrigger("ducharse");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Ducharse();
        }

    }

    public void Destruir()
    {
        GameManager.Instance.ActivarSpawnearBanera();
        Destroy(this.gameObject);
    }
}