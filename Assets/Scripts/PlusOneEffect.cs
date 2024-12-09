using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class PlusOneEffect : MonoBehaviour
{
    TextMeshPro textMesh;
    public float animationTime = 1f;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.alpha -= Time.deltaTime / animationTime;
        transform.Translate(0, speed * Time.deltaTime, 0);
        if (textMesh.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
