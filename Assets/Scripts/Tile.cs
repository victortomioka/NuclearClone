using UnityEngine;
using System.Collections;

public class Tile{

	public int x, y;
	public GameObject view; //view object for the tile
	public enum TileType {Wall, Floor};
	public TileType thisTileType = TileType.Floor;
	public int tileSetIndex = 0;

}
