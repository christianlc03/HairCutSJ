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
            transform.position = new Vector3(Mathf.Clamp(seguimiento.position.x, -13, 13), Mathf.Clamp(seguimiento.position.y, -12, 12), -20);
        }
    }

}
