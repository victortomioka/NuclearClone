using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown("space")){
			SceneManager.LoadScene("99_test");
		}
	}
}
