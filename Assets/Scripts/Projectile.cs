using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour{

	public float velocity;
	public float mass;
	public Sprite graphic;
	public int damage;
	float timer = 0;
	public float timeToDestroy = 1.0f;
	public GameObject explosion;

	void Update () {
		timer += Time.deltaTime;
		if(timer>=timeToDestroy){
			if(explosion!=null){
				Instantiate(explosion,transform.position,Quaternion.identity);
			}
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(explosion!=null){
			Instantiate(explosion,transform.position,Quaternion.identity);
		}
		GameObject.Destroy(this.gameObject);

	}
}
