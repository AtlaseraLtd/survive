using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ParallaxLayer : MonoBehaviour
{
    [Header("Parallax Settings")]
    public float parallaxFactor; // 0 = moves with camera, 1 = static background
    public bool infiniteScrolling = true;

    private Camera mainCamera;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    void Start()
    {
        mainCamera = Camera.main;
        lastCameraPosition = mainCamera.transform.position;

        // Get the width of the sprite for infinite scrolling
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        Vector3 cameraDelta = mainCamera.transform.position - lastCameraPosition;

        // Move layer based on parallax factor
        transform.position += new Vector3(cameraDelta.x * parallaxFactor,
                                          cameraDelta.y * parallaxFactor,
                                          0);

        lastCameraPosition = mainCamera.transform.position;

        // Loop the layer infinitely
        if (infiniteScrolling)
        {
            float distanceFromCamera = mainCamera.transform.position.x - transform.position.x;
            if (Mathf.Abs(distanceFromCamera) >= textureUnitSizeX)
            {
                float offset = distanceFromCamera % textureUnitSizeX;
                transform.position = new Vector3(mainCamera.transform.position.x + offset,
                                                 transform.position.y,
                                                 transform.position.z);
            }
        }
    }
}