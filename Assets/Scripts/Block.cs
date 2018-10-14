using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Block : MonoBehaviour {
    BoxCollider2D col;
    SpriteRenderer myRenderer;
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        if(col != null)
        {
            if(col.usedByComposite == false)
            {
                col.usedByComposite = true;
            }
        }
    }

    private void Start()
    {
        myRenderer.color = new Color(Random.Range(0.9f, 1f) * Random.Range(0, 2),Random.Range(0.9f, 1f) *Random.Range(0, 2),Random.Range(0.9f, 1f) *Random.Range(0, 2));

        if (myRenderer.color.Equals(Color.black))
            myRenderer.color = Color.white;

    }
}
