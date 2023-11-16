using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientBackground : MonoBehaviour
{
    public Gradient gradient;
    public float speed;

    private Camera mainCamera;
    private float t = 0f;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        t += Time.deltaTime * speed;
        
        mainCamera.backgroundColor = gradient.Evaluate(2);
    }
}
