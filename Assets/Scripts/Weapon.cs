using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Base", order = 1)]
public class Weapon : ScriptableObject {

	public string name;
	public Rigidbody2D projectile;
	public float rateOfFire;
	public float recoil;
	public int burstSize;
	public float burstInterval;

	public enum actionType{semiauto,burst,automatic};
	public actionType action;

	public enum ammoType{bullet,shell,explosive,energy};
	public ammoType ammo;

	public virtual void Shoot(Transform muzzle){
		GameObject p = GameObject.Instantiate(projectile,muzzle.position,muzzle.rotation) as GameObject;
	}

	public virtual IEnumerator Burst(Transform muzzle){
		int counter = 0;
		while(counter<burstSize){
			counter++;
			GameObject p = GameObject.Instantiate(projectile,muzzle.position,muzzle.rotation) as GameObject;
			yield return new WaitForSeconds(burstInterval);
		}
		yield return null;
	}
}
