using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CompositeCollider2D))]
public class WaveBuilder : MonoBehaviour {
    public Block BlockType;
    public Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    public void CreateTile(float height)
    {
        int totalSpawn = Mathf.Max(1, Mathf.RoundToInt(height));
        for(int i = 0; i < totalSpawn; ++i)
        {
            Block newBlock = Instantiate(BlockType, new Vector3(transform.position.x, transform.position.y + i, transform.position.z), Quaternion.identity);
            newBlock.transform.SetParent(this.transform);
        }
    }

    public void SetSpeed(float speed)
    {
        myBody.velocity = new Vector2(speed, 0f);
    }
}
