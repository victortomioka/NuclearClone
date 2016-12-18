using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy"){
			LivingEntity entity = col.gameObject.GetComponent<LivingEntity>();
			entity.takeDamage(1);
		}
	}

}
