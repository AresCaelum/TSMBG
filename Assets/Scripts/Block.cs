using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    SpriteRenderer myRenderer;
    BoxCollider2D col;
    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        myRenderer.color = new Color(Random.Range(0.9f, 1f) * Random.Range(0, 2), Random.Range(0.9f, 1f) * Random.Range(0, 2), Random.Range(0.9f, 1f) * Random.Range(0, 2));

        if (myRenderer.color.Equals(Color.black))
            myRenderer.color = Color.white;
    }

    public void SetHeight(int topHeight)
    {
        col.size = new Vector2(1f, topHeight);
        myRenderer.size = new Vector2(1f, topHeight);
        transform.position = new Vector3(transform.position.x, -10 + (topHeight - 1) * 0.5f, transform.position.z);
    }
}
