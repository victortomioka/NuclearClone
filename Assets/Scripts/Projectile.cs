using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour{

	public float velocity;
	public float mass;
	public Sprite graphic;
	public int damage;
	float timer = 0;
	public float timeToDestroy = 1.0f;

	void Update () {
		timer += Time.deltaTime;
		if(timer>=timeToDestroy){
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		GameObject.Destroy(this.gameObject);
	}
}
