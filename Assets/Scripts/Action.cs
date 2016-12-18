using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Action", menuName = "Actions/Base", order = 1)]
public class Action : ScriptableObject {

	public GameObject actor;
	public GameObject target;
	public Action act;

}
