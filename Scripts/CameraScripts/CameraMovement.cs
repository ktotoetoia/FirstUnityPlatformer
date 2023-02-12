using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x,playerTransform.position.y,transform.position.z);
    }
}
