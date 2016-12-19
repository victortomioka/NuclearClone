using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player"){
			IDamageable entity = col.gameObject.GetComponent<IDamageable>();
			entity.takeDamage(1);
		}
	}

}
