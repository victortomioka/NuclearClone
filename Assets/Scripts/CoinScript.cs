using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

	AudioManager AMngr;

	void start() {
		AMngr = Camera.main.GetComponent<AudioManager>();
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag=="Player"){
			GameMngr.coinsLeft--;
			AMngr = Camera.main.GetComponent<AudioManager>();
			AMngr.SoundEffect("Minhoca");
			GameObject.Destroy(this.gameObject);
		}
	}
}
