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
            Destroy(this, 0.25f);
        }
	}

    private void OnDestroy()
    {
        if(GetComponent<Player>() != null && player.GetComponents<Stun>().Length <= 1)
            player.enabled = true;
    }
}
