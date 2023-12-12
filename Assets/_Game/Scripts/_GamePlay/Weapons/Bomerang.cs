using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomerang : MonoBehaviour
{
    private float rotateSpeed = 800f;
    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        
    }
}
