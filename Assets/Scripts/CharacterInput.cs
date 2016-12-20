using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour {

	float acceleration;
	public float speed = 2f;
	[Range(0.0f,1.0f)]
	public float damper = 0.5f;
	public float weaponDistance = 1f;
	[Range(0.0f,360.0f)]
	public float aimSpeed = 15f;

	AudioManager AMngr;
	Transform gunMount;
	Rigidbody2D rb;
	Vector2 vector;

	public bool canMove = true;

	Camera cam;
	Transform firePoint;
	public LayerMask dontHit;
	public float cooldown = 0.3f;
	float timeToFire;
	public Rigidbody2D bullet;

	LivingEntity thisEntity;

	void Start(){
		timeToFire = 0;
		cam = Camera.main;
		rb = gameObject.GetComponent<Rigidbody2D>();
		gunMount = new GameObject().transform;
		gunMount.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		gunMount.parent = transform;
		thisEntity = gameObject.GetComponent<LivingEntity>();
	}

	void Update(){

		if(timeToFire>0){
			timeToFire-=Time.deltaTime;
			if(timeToFire<0){
				timeToFire=0;
			}
		}

		vector = new Vector3(Input.GetAxis("Horizontal")*damper*speed,Input.GetAxis("Vertical")*damper*speed);
		rb.AddForce(vector,ForceMode2D.Impulse);

		Aim();

		if(Input.GetButton("Fire1") && timeToFire==0){
			timeToFire = cooldown;
			Shoot();
		}

		if(Input.GetKeyDown(KeyCode.A)){
			Vector3 v = new Vector3(-1,1,1);
			transform.localScale = v;
		}
		if(Input.GetKeyDown(KeyCode.D)){
			Vector3 v = new Vector3(1,1,1);
			transform.localScale = v;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.layer==9){
			thisEntity.takeDamage(1);
		}
		if(col.gameObject.layer==12){
			thisEntity.takeDamage(1);
		}
		if(col.gameObject.layer==13){
			thisEntity.takeDamage(1);
		}
	}

	void Aim(){
		var pos = Camera.main.WorldToScreenPoint(transform.position);
		var dir = Input.mousePosition - pos;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		gunMount.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
	}

	void Shoot(){
		Rigidbody2D proj = Instantiate(bullet,gunMount.position,gunMount.rotation) as Rigidbody2D;
		AMngr = Camera.main.GetComponent<AudioManager>();
		AMngr.SoundEffect("TiroBoss");
		proj.AddForce(proj.transform.right*500);
	}
		
}
