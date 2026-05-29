using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;
    public float smoothSpeed = 5f;

    [Header("Offset")]
    public Vector3 offset = new Vector3(3f, 0f, -10f);

    [Header("Vertical Clamp")]
    public float minY = -2f;
    public float maxY = 2f;

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = player.position.x + offset.x;
        float targetY = Mathf.Clamp(player.position.y + offset.y, minY, maxY);

        Vector3 targetPosition = new Vector3(targetX, targetY, offset.z);

        transform.position = Vector3.Lerp(transform.position,
                                          targetPosition,
                                          smoothSpeed * Time.deltaTime);
    }
}