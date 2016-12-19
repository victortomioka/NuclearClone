using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splashscreen : MonoBehaviour {

	public float duration = 10.0f;

	void Start () {
		Invoke("Begin",duration);
	}
	

	void Begin(){
		SceneManager.LoadScene("99_test");
	}
}
