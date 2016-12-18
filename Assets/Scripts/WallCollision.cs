using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WallCollision : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if (col.collider.tag=="Player") {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
