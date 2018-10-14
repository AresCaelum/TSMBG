using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {
    public Vector3 deltaRotation = new Vector3(0, 0, 0);
	// Update is called once per frame
	void Update () {
        transform.Rotate(deltaRotation * Time.deltaTime);
	}
}
