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
            transform.position = new Vector3(Mathf.Clamp(seguimiento.position.x, -19, 19), Mathf.Clamp(seguimiento.position.y, -16, 16), -20);
        }
    }

}
