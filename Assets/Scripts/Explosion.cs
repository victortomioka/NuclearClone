﻿using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public LayerMask mask;
	public float explosionRadius = 10.0f;
	public float force = 500.0f;
	public float timetoDeath = 1.0f;
	float timer;
	public int damage;

	Collider2D[] col;

	AudioManager AMngr;

	void Start () {
		StartCoroutine(selfDestruct());
		AMngr = Camera.main.GetComponent<AudioManager>();
		AMngr.SoundEffect("Explosion");
		col = Physics2D.OverlapCircleAll(transform.position,explosionRadius,mask);
		foreach(Collider2D c in col){
			Rigidbody2D rb2D = c.gameObject.GetComponent<Rigidbody2D>();
			IDamageable entity = c.gameObject.GetComponent<IDamageable>();
			if(entity!=null){
				entity.takeDamage(damage);
			}

			if(rb2D!=null){
				
				Vector2 dir = rb2D.transform.position - transform.position;
				rb2D.AddForce(dir*force);
			}
		}
	}

	IEnumerator selfDestruct(){
		while(timer<timetoDeath){
			timer+=Time.deltaTime;
			yield return null;
		}
		GameObject.Destroy(this.gameObject);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,explosionRadius);
	}
}
