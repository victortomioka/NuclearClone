using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

	public Weapon[] weapons = new Weapon[2];
	Weapon current, reserve;

	Dictionary<string,int> ammoPool = new Dictionary<string,int>();
	public static int bullCount,shellCount,enerCount,explCount;



	void Start(){
		ammoPool.Add("bullet",bullCount);
		ammoPool.Add("shell",shellCount);
		ammoPool.Add("energy",enerCount);
		ammoPool.Add("explosive",explCount);
		equipWeapons();
	}

	void Update(){
		if(current.action == Weapon.actionType.semiauto){

		}

		if(current.action == Weapon.actionType.automatic){

		}

		if(current.action == Weapon.actionType.burst){
			//insert burst code here
		}
	}

	public void equipWeapons(){
		current = weapons[0];
		if(weapons[1]!=null){
			reserve = weapons[1];
		}
	}

	public void setupWeapons(Weapon w){

	}

	public void switchWeapons(){
		Weapon cur,res;
		cur = current;
		res = reserve;
		current = res;
		reserve = cur;
		weapons[0] = current;
		weapons[1] = reserve;
		equipWeapons();
	}

	public void exchangeCurrent(Weapon _new){
		weapons[0] = _new;
		equipWeapons();
	}
}
