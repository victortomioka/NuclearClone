using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Multi", menuName = "Weapon/Multi", order = 2)]
public class WeaponMulti : Weapon {

	public int projectileCount;

	public override void Shoot(Transform muzzle){
		for (int i = 0; i < projectileCount; i++) {
			GameObject p = GameObject.Instantiate(projectile,muzzle.position,muzzle.rotation) as GameObject;
		}
	}

	public override IEnumerator Burst(Transform muzzle){
		int counter = 0;
		while(counter<burstSize){
			counter++;
			for (int i = 0; i < projectileCount; i++) {
				GameObject p = GameObject.Instantiate(projectile,muzzle.position,muzzle.rotation) as GameObject;
			}
			yield return new WaitForSeconds(burstInterval);
		}
		yield return null;
	}
}
