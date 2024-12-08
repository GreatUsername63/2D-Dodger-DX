using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AspectRatio : MonoBehaviour
{
    public float aspectRatiodividend = 4f;
    public float aspectRatioDivisor = 3f;

    private float aspectRatio;
    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        aspectRatio = aspectRatiodividend / aspectRatioDivisor;
    }

    void SetCameraAspect()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / aspectRatio;

        if (scaleHeight < 1.0f)
        {
            //Letterboxxing
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            //Pillarboxing
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraAspect();
    }
}
