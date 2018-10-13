using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Block : MonoBehaviour {
    BoxCollider2D col;
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();

        if(col != null)
        {
            if(col.usedByComposite == false)
            {
                col.usedByComposite = true;
            }
        }
    }
}
