using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    private void OnInit()
    {
        transform.rotation = Quaternion.Euler(50, 0, 0);
    }
    void Start()
    {
        OnInit();
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, 1);
    }
}