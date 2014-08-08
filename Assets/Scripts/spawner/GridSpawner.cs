using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSpawner : MonoBehaviour {

	protected GameControl gc;
	public GameObject[] obstacles;
	public GameObject[] powerups;
	public GameObject[] spawnPoints;
	public Vector2 speed;
	public float timer;

	public BlocksDB blocksClass;
	private List<MapBlock> blocks;
	private MapBlock st; //1st block of tiles
	private MapBlock nd; //2nd block of tiles
	private MapBlock rd; //3rd block of tiles

	private int gridColumn;
	private int gridSize;

	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.stateChanged += gameStateChanged;
		
		speed = new Vector2(gc.GameSpeed , 0);
		timer = 2f;

		blocks = blocksClass.blocks;
		if(blocks == null){
			GameObject go = GameObject.Find ("BlockDB");
			blocksClass = go.GetComponent<BlocksDB>();
			blocks = blocksClass.blocks;
		}

		st = blocks[Random.Range(0,blocks.Count - 1)];
		nd = blocks[Random.Range(0,blocks.Count - 1)];
		rd = blocks[Random.Range(0,blocks.Count - 1)];

		gridColumn = 0;
		gridSize = MapBlock.Size;
	}
	
	// Update is called once per frame
	void Update () {
		if(gc.currentGameState != GameControl.GameState.Play)
			return;
		if(timer <= 0){
			if(gridColumn == gridSize){
				spawnColumn(generateTransitoryColumn(st, nd));
				st = nd;
				nd = rd;
				rd = blocks[Random.Range(0,blocks.Count - 1)];
				gridColumn = 0;
			}
			spawnColumn(new int[4] {
				st.grid[0, gridColumn],
				st.grid[1, gridColumn],
				st.grid[2, gridColumn],
				st.grid[3, gridColumn]
			});
			gridColumn++;
			timer = 2f;
		}
		else{
			timer -= (Time.deltaTime * speed.x);
		}
	}

	void spawnColumn(int[] column){
		spawnTile(column[0], 0);
		spawnTile(column[1], 1);
		spawnTile(column[2], 2);
		spawnTile(column[3], 3);
	}

	void spawnTile(int tile, int spawner){
		switch(tile){
		case 1:
			int objectLenght = obstacles.Length;
			int rand = Random.Range( 0 , objectLenght );
			GameObject obs = (GameObject) Instantiate(obstacles[rand] , spawnPoints[spawner].transform.position , spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			break;
		case 2:
			objectLenght = powerups.Length;
			rand = Random.Range( 0 , objectLenght );
			obs = (GameObject) Instantiate(powerups[rand] , spawnPoints[spawner].transform.position , spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			break;
		default:
			break;
		}
	}

	int[] generateTransitoryColumn(MapBlock previous, MapBlock next){
		return new int[4] {0, 0, 0, 0};
	}

	void gameStateChanged(float gs)
	{
		speed = new Vector2(gs, 0);
	}
}
