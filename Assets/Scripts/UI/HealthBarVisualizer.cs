using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarVisualizer : MonoBehaviour
{
    public int band;
    public float startScale;
    public float scaleMultiplier;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Max((AudioVisualizerManager.audioBandBuffer[band] * scaleMultiplier) + startScale, 0.0f), transform.localScale.z);
    }
}