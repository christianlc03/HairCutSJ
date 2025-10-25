using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform seguimiento;
    void Update()
    {
        if (seguimiento != null)
        {
            transform.position = new Vector3(Mathf.Clamp(seguimiento.position.x, -4.2f, 4.2f), Mathf.Clamp(seguimiento.position.y, -7.2f, 5.3f), -20);
        }
    }

}
