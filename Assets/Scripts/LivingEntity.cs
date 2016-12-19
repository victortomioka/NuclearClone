using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LivingEntity : MonoBehaviour, IDamageable {

	public int maxHealth;
	int health;
	Renderer rend;
	bool vulnerable = true;
	public bool isDead = false;
	public bool kill = true;

	void Start(){
		health=maxHealth;
		rend = gameObject.GetComponent<Renderer>();
		vulnerable = true;
	}

	public void takeDamage(int value){
		if(vulnerable){
		health-=value;
			if(health<=0){
				isDead = true;
				Die();
			}

			StartCoroutine(damageEffect());
		}

	}

	public void healDamage(int value){
		health+=value;
		if(health>=maxHealth){
			health=maxHealth;
		}
	}

	public void Die (){
		if(kill){
			if(gameObject.layer==8){
				print("player died");
				SceneManager.LoadScene("98_gameover");
			}else{
				GameObject.Destroy(this.gameObject);
			}
		}
	}

	public float getHealthPercent(){
		float perc = (health/maxHealth)*1.0f;
		return perc;
	}

	public int getHealth {
		get {
			return health;
		}
	}

	IEnumerator damageEffect(){
		vulnerable = false;
		if(rend!=null){
			Color origin = rend.material.color;
			Color target = Color.red;
			float timer = 0;
			while(timer<0.3f){
				timer+=Time.deltaTime;
				rend.material.color = Color.Lerp(origin,target,timer*3);
				yield return null;
			}
			vulnerable = true;
			rend.material.color = origin;
			yield return null;
		}
	}
}
