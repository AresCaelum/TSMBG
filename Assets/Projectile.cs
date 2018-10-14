using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
    public Vector2 startVelocity = new Vector2(-10f, 0f);
    Rigidbody2D myBody;
    void Awake ()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
        myBody.velocity = startVelocity;
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            player.gameObject.AddComponent<Stun>();
            Destroy(this.gameObject);
        }
    }
}

