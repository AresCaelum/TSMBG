using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeath : MonoBehaviour {
    public float minX = -13.0f;
	// Use this for initialization
	void Start () {
	}

    private void Update()
    {
        if(transform.position.x < minX)
        {
            Destroy(this.gameObject);
        }
    }
}
