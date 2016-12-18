using UnityEngine;
using System.Collections;

public class EnemyMelee : MonoBehaviour {

	public Rigidbody2D projectile;
	public float sightRadius = 10.0f;
	public float shootRadius = 7.0f;
	public float strikeRadius = 1.0f;
	public float speed = 1.0f;
	public float runningSpeed = 1.5f;
	public float shotCooldown = 1.0f;
	public float cowardice = 1.0f;

	[Header("Behaviours")]
	public bool ranged = false;
	public bool shootWhileFleeing = false;

	[Header("Animations")]
	public string idleAnim;
	public string moveAnim;
	public string attackAnim;

	public enum States {idle, pursuit, shooting, fleeing};
	States currentState = States.idle;
	States previousState = States.idle;

	float coolTimer;
	float cowardTimer;
	LayerMask mask;
	LivingEntity thisEntity;
	Transform tgt;
	Rigidbody2D rb2D;
	Collider2D colliders;
	Collider2D colliders_pursuit;
	Collider2D colliders_flee;
	Animator anim;

	void Start(){
		thisEntity=gameObject.GetComponent<LivingEntity>();
		mask = LayerMask.GetMask("Player");
		rb2D = gameObject.GetComponent<Rigidbody2D>();
		coolTimer = shotCooldown;
	}

	void Update(){
		if(coolTimer>0){
			coolTimer-=Time.deltaTime;
			if(coolTimer<0){
				coolTimer = 0;
			}
		}

		if(rb2D.velocity.x<0){
			Vector3 v = new Vector3(1,1,1);
			transform.localScale = v;
		}
		if(rb2D.velocity.x>=0){
			Vector3 v = new Vector3(-1,1,1);
			transform.localScale = v;
		}

		if(anim!=null){
		if(rb2D.velocity.x>0 || rb2D.velocity.y>0 && currentState!= States.idle){
			anim.Play(moveAnim);
		}else{
			anim.Play(idleAnim);
		}
		}

		switch(currentState){
			case(States.idle):
			colliders = Physics2D.OverlapCircle(transform.position,sightRadius,mask);
			if(colliders!=null){
			if(colliders.gameObject.tag=="Player"){
					tgt = colliders.transform;
					currentState = States.pursuit;
				}
			}
			break;

			case(States.pursuit):Pursue();break;
			case(States.fleeing):Flee();break;
			case(States.shooting):break;
		}
	}

	void Pursue(){
		rb2D.AddForce((tgt.transform.position-transform.position)*runningSpeed);
		if(ranged){
			colliders_pursuit = Physics2D.OverlapCircle(transform.position,shootRadius,mask);
			if(colliders_pursuit!=null){
			if(colliders_pursuit.gameObject.tag =="Player"){
					if(coolTimer == 0){
						coolTimer=shotCooldown;
						previousState = currentState;
						currentState = States.shooting;
						Shoot(tgt);
					}
				}
			}
		}else{
			colliders_pursuit = Physics2D.OverlapCircle(transform.position,strikeRadius,mask);
			if(colliders_pursuit!=null){
				if(colliders_pursuit.gameObject.tag == "Player"){
				colliders_pursuit.gameObject.GetComponent<IDamageable>().takeDamage(1);
					cowardTimer = cowardice;
					currentState = States.fleeing;
				}
			}
		}
	}

	void Flee(){
		cowardTimer-=Time.deltaTime;
		if(cowardTimer<=0){
			cowardTimer=0;
			currentState = States.idle;
			return;
		}
		if(ranged && shootWhileFleeing){
			colliders_flee = Physics2D.OverlapCircle(transform.position,shootRadius,mask);
			if(colliders_flee!=null){
				if(colliders_flee.gameObject.tag =="Player"){
					if(coolTimer == 0){
						coolTimer=shotCooldown;
						previousState = currentState;
						currentState = States.shooting;
						Shoot(tgt);
					}
				}
			}
		}
		rb2D.AddForce(-1*(tgt.transform.position-transform.position)*runningSpeed);
		colliders_flee = Physics2D.OverlapCircle(transform.position,sightRadius,mask);
		if(colliders_flee!=null){
		if(colliders_flee.gameObject.tag == "Player"){
			currentState = States.fleeing;
		}else{
			currentState = States.idle;
			}
		}
	}

	void Shoot(Transform t){
		if(anim!=null){
			anim.SetTrigger("attacking");
			anim.Play(attackAnim);
		}
		var dir = t.position - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Rigidbody2D b = Instantiate(projectile,transform.position,Quaternion.AngleAxis(angle, Vector3.forward)) as Rigidbody2D;
		b.AddForce(b.transform.right*100);
		currentState = previousState;
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.layer == 11){
			thisEntity.takeDamage(1);
			cowardTimer = cowardice;
			currentState = States.fleeing;
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position,sightRadius);
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position,shootRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,strikeRadius);
	}

}
