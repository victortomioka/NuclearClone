using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class GameController : MonoBehaviour {

	public GameObject playerCharacter;
	public GameObject player;

	public void spawnPlayer(int width, int height){
		player = GameObject.Instantiate(playerCharacter,new Vector3(width/2,height/2,0)	,Quaternion.identity) as GameObject;
		Camera.main.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,Camera.main.transform.position.z);
		Camera.main.GetComponent<CameraController>().tgt=player.transform;
	}
}
