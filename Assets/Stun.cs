using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour {
    Player player;
	// Use this for initialization
	void Start () {
        player = gameObject.GetComponent<Player>();
        if(player != null)
        {
            player.StopMoving();
            player.enabled = false;
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20 + Vector2.up * 2, ForceMode2D.Impulse);
            Destroy(this, 0.25f);
        }
	}

    private void OnDestroy()
    {
        if (GetComponent<Player>() != null && player.GetComponents<Stun>().Length <= 1)
        {
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 20, ForceMode2D.Impulse);
            player.enabled = true;
        }
    }
}
