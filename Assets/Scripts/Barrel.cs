using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	public GameObject explosion;
	LivingEntity thisEntity;

	void Awake(){
		thisEntity = gameObject.GetComponent<LivingEntity>();
	}

	void Update(){
		if(thisEntity.isDead){
			Vector3 pos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
			Instantiate(explosion,transform.position,transform.rotation);
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.layer==11 || col.gameObject.layer==12){
				thisEntity.takeDamage(1);
		}
	}
}
