using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;


    [Header("Rotation")]
    [SerializeField] Vector3 playerRotate;
    [SerializeField] Vector3 gamePlayRotate;

    [Header("Offset")]
    [SerializeField] Vector3 scaleUpOffset;
    [SerializeField] Vector3 playerOffset;
    [SerializeField] Vector3 offsetMax;
    [SerializeField] Vector3 offsetMin;

    [SerializeField] float moveSpeed = 5f;
    private Vector3 targetOffset;
    private Quaternion targetRotate;

    [SerializeField] Transform[] offsets;
    public Camera Camera { get; private set; }
    private void Awake()
    {

        Camera = Camera.main;
    }


    void Start()
    {
    }

    void LateUpdate()
    {
        offset = Vector3.Lerp(offset, targetOffset, Time.deltaTime * moveSpeed);
        transform.position = Vector3.Lerp(transform.position, player.position + targetOffset, Time.deltaTime * moveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, Time.deltaTime * moveSpeed);
    }

    public void ScaleOffset(float size)
    {
        if (size > 0)
            targetOffset = targetOffset + scaleUpOffset;
    }
    public void ChangeState(GameState state)
    {
        targetOffset = offsets[(int)state].localPosition;
        targetRotate = offsets[(int)state].localRotation;
        return;

        //switch (state)
        //{
        //    case GameState.MainMenu:
        //        targetOffset = playerOffset;
        //        targetRotate = Quaternion.Euler(playerRotate);
        //        break;

        //    case GameState.GamePlay:
        //        targetOffset = offsetMin;
        //        targetRotate = Quaternion.Euler(gamePlayRotate);
        //        break;

        //    default:
        //        break;
        //}
    }
}