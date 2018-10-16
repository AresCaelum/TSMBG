using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Block))]
public class WaveBuilder : MonoBehaviour {
    Block block;
    private void Awake()
    {
        block = GetComponent<Block>();
    }

    public void CreateTile(float height)
    {
        int topHeight = Mathf.Max(1, Mathf.RoundToInt(height));
        block.SetHeight(topHeight);
    }

}
