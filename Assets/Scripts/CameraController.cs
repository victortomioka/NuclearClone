using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform tgt;

	void Update () {
		if(tgt!=null){
			transform.position = new Vector3(tgt.position.x,tgt.position.y,transform.position.z);
		}
	}
}
