using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMngr : MonoBehaviour {

	int coinsTotal;
	public static int coinsLeft;
	bool initiated;

	void Start () {
		Invoke("StartMatch",0.3f);
	}

	void Update () {
		if(coinsLeft == 0 && initiated){
			SceneManager.LoadScene("97_win");
		}
	}

	void StartMatch(){
		coinsTotal = GenerateMap.coinCount;
		coinsLeft = coinsTotal;
	}
}
