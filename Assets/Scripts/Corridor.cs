using UnityEngine;
using System.Collections;

public enum Direction{
	North,East,South,West
}

public class Corridor : MonoBehaviour {

	public Vector2 origin;
	public int lenght;
	public int breadth;
	public Direction direction;

}
