using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class GenerateMap : MonoBehaviour {

	[Header("Map Settings")]
	Map map;
	public int width = 25, height = 25;
	public string seed;
	public bool randomseed = true;
	[SerializeField]
	public TileSet tileSet;

	[Header("Room Settings")]
	public int roomQuantity = 2;
	public int roomWidthMin = 1, roomWidthMax = 4;
	public int roomHeightMin = 1, roomHeightMax = 4;

	[Header("Objects")]
	[Range(0.0f,100.0f)]
	public float obstacleFill;
	[Range(0.0f,100.0f)]
	public float enemyFill;
	[Range(0.0f,100.0f)]
	public float pickupFill;
	public GameObject[] enemies;
	public GameObject[] obstacles;
	public GameObject[] pickups;


	SpriteRenderer[,] rendMap;
	GameObject[,] viewMap;
	Room[] rooms;
	Room[] shuffledRooms;
	GameObject tileMaster;



	void Start(){
		createMap();
	}

	public void createMap(){
		if (tileMaster != null) {
			GameObject.Destroy (tileMaster);
		}

		tileMaster = new GameObject() as GameObject;
		tileMaster.gameObject.name = "==Tile Master==";
		map = new Map ();
		map.mapWidth = width;
		map.mapHeight = height;
		print ("map dimensions = " + width + ", " + height);
		//creates tiles

		map.tiles = populateTiles(width,height,map);
		print ("tiles populated!");

		//creates rooms

		rooms = new Room[roomQuantity];
		rooms[0] = roomSetup(width/2,height/2);
		for (int i = 1; i < roomQuantity; i++) {
			rooms[i] = roomSetup();
		}
		buildRooms();
		print ("rooms generated!");

		//connects rooms with corridoors
		corridorSetup();
		print("corridors generated!");

		createBorders ();
		setupGraphics();

		populateMap();

		if(gameObject.GetComponent<GameController>().player!=null){GameObject.Destroy(gameObject.GetComponent<GameController>().player);}
		gameObject.GetComponent<GameController>().spawnPlayer(width,height);
	}

	Tile[,] populateTiles(int mapW, int mapH, Map map){
		map.tiles = new Tile[mapW+1,mapH+1];
		viewMap = new GameObject[mapW+1, mapH+1];
		rendMap = new SpriteRenderer[mapW+1, mapH+1];
		for (int x = 0; x <= mapW; x++) {
			for (int y = 0; y <= mapH; y++) {
				map.tiles[x,y] = tileInstantiate(x,y); //creates tile data
				tileSetup(map.tiles[x,y],x,y); //creates tile visuals, functionality etc
			}
		}
		return map.tiles;
	}

	Tile tileInstantiate(int _x, int _y){
		Tile _tile = new Tile ();
		_tile.x = _x;
		_tile.y = _y;
		_tile.view = new GameObject();
		_tile.view.transform.position = new Vector3 (_tile.x, _tile.y, 0);
		rendMap [_x, _y] = _tile.view.AddComponent<SpriteRenderer>();
		_tile.view.name = "tile_"+_tile.x+","+_tile.y;
		_tile.view.transform.parent = tileMaster.transform;
		return _tile;
	}

	void tileSetup(Tile _tile, int _x, int _y){
		_tile.thisTileType = Tile.TileType.Wall;
	}

	void buildRooms(){
		foreach(Room r in rooms){
			//check if room intersects edges of map, move it accordingly.
			if(r.originX<=0){
				r.posX+=1;
				r.originX+=1;
			}
			if(r.originX+r.roomWidth>=width){
				r.posX -= Mathf.FloorToInt((r.originX+r.roomWidth-width)+1);
				r.originX = r.posX;
			}
			if(r.originY<=0){
				r.posY+=1;
				r.originY+=1;
			}
			if(r.originY+r.roomHeight>=height){
				r.posY -= Mathf.FloorToInt((r.originY+r.roomHeight-height)+1);
				r.originY = r.posY;
			}
				
			//build room
			map.tiles[(int)r.originX,(int)r.originY].thisTileType = Tile.TileType.Floor;
			for (int x = 0; x < r.roomWidth; x++) {
				for (int y = 0; y < r.roomHeight; y++) {
					map.tiles[(int)r.originX+x,(int)r.originY+y].thisTileType = Tile.TileType.Floor;
				}
			}
		}
	}

	void corridorSetup(){

		shuffledRooms = rooms;
		shuffleRooms (shuffledRooms);

		int cOriginX = 0;
		int cOriginY = 0;
		int cEndX = 0;
		int cEndY = 0;

		for (int i = 1; i < shuffledRooms.Length-1; i++) {
			cOriginX = shuffledRooms [i-1].originX;
			cOriginY = shuffledRooms [i-1].originY;
			cEndX = shuffledRooms [i].originX;
			cEndY = shuffledRooms [i].originY;

			if (cEndX > cOriginX) {
				for (int x = cOriginX; x < cEndX+1; x++) {
//					print (x + "," + cOriginY);
					map.tiles [x, cOriginY].thisTileType = Tile.TileType.Floor;
					map.tiles [x, cOriginY-1].thisTileType = Tile.TileType.Floor;
					map.tiles [x, cOriginY+1].thisTileType = Tile.TileType.Floor;

				}
			} else if (cEndX < cOriginX) {
				for (int x = cEndX; x <  cOriginX-1; x++) {
//					print (x + "," + cOriginY);
					map.tiles [x, cEndY].thisTileType = Tile.TileType.Floor;
					map.tiles [x, cEndY-1].thisTileType = Tile.TileType.Floor;
					map.tiles [x, cEndY+1].thisTileType = Tile.TileType.Floor;

				}
			}

			if (cEndY > cOriginY) {
				for (int y = cOriginY; y < cEndY+1; y++) {
//					print (cOriginX + "," + y);
					map.tiles [cOriginX, y].thisTileType = Tile.TileType.Floor;
					map.tiles [cOriginX-1, y].thisTileType = Tile.TileType.Floor;
					map.tiles [cOriginX+1, y].thisTileType = Tile.TileType.Floor;

				}
			} else if (cEndY < cOriginY) {
				for (int y = cEndY; y < cOriginY-1; y++) {
//					print (cOriginX + "," + y);
					map.tiles [cEndX, y].thisTileType = Tile.TileType.Floor;
					map.tiles [cEndX-1, y].thisTileType = Tile.TileType.Floor;
					map.tiles [cEndX+1, y].thisTileType = Tile.TileType.Floor;

				}
			}
		
				

		}
	}

	void setupGraphics(){
		for (int x = 0; x <= width; x++) {
			for(int y = 0; y <= height; y++){
			
				switch(map.tiles[x,y].thisTileType){

			case Tile.TileType.Floor:

					rendMap[x,y].sprite = tileSet.floor[Random.Range(0,tileSet.floor.Length-1)];
				
					break;

			case Tile.TileType.Wall:
					
					int index = tilePosSum(map.tiles[x,y]);
					rendMap[x,y].sprite = tileSet.walls[index];
					map.tiles[x,y].view.AddComponent<BoxCollider2D>();
					map.tiles[x,y].view.AddComponent<WallCollision>();
					break;
				}
			}
		}
	}

	int tilePosSum(Tile t){
		int sum = 0;
		if(t.x<1 || t.x>=width || t.y<1 || t.y>=height){return 15;}
		else{
		if(map.tiles[t.x,t.y+1].thisTileType==Tile.TileType.Wall){ sum+=1; }
		if(map.tiles[t.x-1,t.y].thisTileType==Tile.TileType.Wall){ sum+=2; }
		if(map.tiles[t.x,t.y-1].thisTileType==Tile.TileType.Wall){ sum+=4; }
		if(map.tiles[t.x+1,t.y].thisTileType==Tile.TileType.Wall){ sum+=8; }
		}
		return sum;
	}

	void populateMap(){
		for (int x = 0; x <= width; x++) {
			for (int y = 0; y <= height; y++) {
				if(map.tiles[x,y].thisTileType==Tile.TileType.Floor){
				float chance = Random.Range(0,100);
				if(chance<enemyFill){
					spawnEnemy(map.tiles[x,y].x,map.tiles[x,y].y);
				}else {
				chance = Random.Range(0,100);
				if(chance<obstacleFill){
					spawnObstacle(map.tiles[x,y].x,map.tiles[x,y].y);
						}
					}
				}
			}
		}
	}

	void spawnEnemy(int x,int y){
		int index = Random.Range(0,enemies.Length);
		if(enemies!=null){
			GameObject newEnemy = Instantiate(enemies[index],new Vector3(x,y,0),transform.rotation) as GameObject;
		}
	}

	void spawnObstacle(int x,int y){
		int index = Random.Range(0,obstacles.Length);
		if(enemies!=null){
			GameObject newObstacle = Instantiate(obstacles[index],new Vector3(x,y,0),transform.rotation) as GameObject;
		}
	}

	void spawnPickup(int x,int y){
		int index = Random.Range(0,pickups.Length);
		if(enemies!=null){
			GameObject newPickup = Instantiate(pickups[index],new Vector3(x,y,0),transform.rotation) as GameObject;
		}
	}

	void createBorders(){

		for (int x = 0; x <= width; x++) {
			for (int y = 0; y <= height; y++) {
				if (map.tiles [x, y].x <= 1 || map.tiles [x, y].x >= width-1 || map.tiles [x, y].y <= 1 || map.tiles [x, y].y >= height-1) { 
					map.tiles [x, y].thisTileType = Tile.TileType.Wall;
				}
			}
		}

	}

	void shuffleRooms(Room[] arr){
		
		for (var i = arr.Length - 1; i > 0; i--) {
			var r = Random.Range(0,i);
			var tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
	}

	Room roomSetup(){
		Room _room = new Room();
		_room.posX = Random.Range(0,width);
		_room.posY = Random.Range(0,height);
		_room.originX = _room.posX;
		_room.originY = _room.posY;
		_room.roomWidth = roomWidthMax;
		_room.roomHeight = roomHeightMax;
		return _room;
	}

	Room roomSetup(int _width, int _height){ //overloaded method for creating first room;
		Room _room = new Room();
		_room.posX = _width;
		_room.posY = _height;
		_room.originX = _room.posX;
		_room.originY = _room.posY;
		_room.roomWidth = Random.Range(roomWidthMin,roomWidthMax);
		_room.roomHeight = Random.Range(roomHeightMin,roomHeightMax);
		return _room;
	}

	[System.Serializable]
	public class TileSet{
		public Sprite[] floor;
		public Sprite[] walls;
	}

}
