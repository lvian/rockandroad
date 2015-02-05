using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSpawner : MonoBehaviour {

	protected GameControl gc;
	protected Playermovement player;
	public SpawnableObject[] staticObstacles;
	public SpawnableObject[] movableObstacles;
	public SpawnableObject[] powerUps;
	public SpawnableObject multiplier;
	public GameObject[] spawnPoints;
	public Vector2 speed;
	public float timer;
	public int tileCounter;
	public Queue tileCountTriggers;

	private BlocksDB level1;
	private BlocksDB level2;
	private BlocksDB level3;
	private BlocksDB level4;
	private BlocksDB level5;
	private BlockSpawner bs;
	private BlocksDB blocksDB;
	private MapBlock st; //1st block of tiles
	private MapBlock nd; //2nd block of tiles
	private MapBlock rd; //3rd block of tiles

	private int gridColumn;
	private int gridSize;
	private int currentTrigger;
	private int multiplierCounter;

	public enum DifficultyTriggers{
		_1 = 0,
		_2 = 50,
		_3 = 300,
		_4 = 350,
		_5 = 550,
	}

	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		player = GameObject.Find("Player").GetComponent<Playermovement>();
		gc.stateChanged += gameStateChanged;
		
		speed = new Vector2(gc.GameSpeed , 0);
		timer = 2f;

		bs = BlockSpawner.Instance;
		blocksDB = new Level1DB();

		st = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];
		nd = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];
		rd = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];

		gridColumn = 0;
		gridSize = st.grid.GetLength(1);
		tileCounter = 0;
		tileCountTriggers = new Queue();
		tileCountTriggers.Enqueue((int)DifficultyTriggers._2);
		tileCountTriggers.Enqueue((int)DifficultyTriggers._3);
		tileCountTriggers.Enqueue((int)DifficultyTriggers._4);
		tileCountTriggers.Enqueue((int)DifficultyTriggers._5);
		currentTrigger = (int) tileCountTriggers.Dequeue();
		multiplierCounter = 30;
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
				rd = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];
				gridColumn = 0;
				gridSize = st.grid.GetLength(1);
			}
			else{
				spawnColumn(new int[4] {
					st.grid[0, gridColumn],
					st.grid[1, gridColumn],
					st.grid[2, gridColumn],
					st.grid[3, gridColumn]
				});
				gridColumn++;
			}
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
		tileCounter++;
		multiplierCounter--;
		changeDifficulty();
	}

	void spawnTile(int tile, int spawner){
		switch(tile){
		case 0:
			if(multiplierCounter <= 0){
				GameObject obs = (GameObject) Instantiate(multiplier.gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
				obs.transform.parent = spawnPoints[spawner].transform;
				multiplierCounter = 30;
			}
			break;
		case 1:
			int rand = bs.randomInfluencedIndex(staticObstacles);
			GameObject obs = (GameObject) Instantiate(staticObstacles[rand].gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			break;
		case 2:
			rand = bs.randomInfluencedIndex(movableObstacles);
			obs = (GameObject) Instantiate(movableObstacles[rand].gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			SpawnableObject so = obs.GetComponent<SpawnableObject>();
			so.lanes = spawnPoints;
			so.spawnLane = spawner;
			break;
		case 3:
			rand = bs.randomInfluencedIndex(powerUps);
			obs = (GameObject) Instantiate(powerUps[rand].gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			break;
		default:
			break;
		}
	}

	int[] generateTransitoryColumn(MapBlock previous, MapBlock next){
		defineHealthBoostPriority();
		int[] column = new int[4] {0, 0, 0, 0};
		for(int r = 0; r < previous.grid.GetLength(0); r++){
			int _prev = previous.grid[r,previous.grid.GetLength(1) - 1];
			int _next = next.grid[r,0];
			if(_prev == 0){
				if(_next == 1){
					column[r] = 3;
					return column;
				}
			}
			else{
				if(_next == 0){
					column[r] = 3;
					return column;
				}
			}
		}
		return column;
	}

	void changeDifficulty(){
		if(tileCounter == currentTrigger){
			switch(tileCounter){
			case (int)DifficultyTriggers._2:
				blocksDB = new Level2DB();
				break;
			case (int)DifficultyTriggers._3:
				blocksDB = new Level3DB();
				break;
			default:
				break;
			}
			currentTrigger = (int) tileCountTriggers.Dequeue();
			nd = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];
			rd = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];
		}
	}

	private void defineHealthBoostPriority(){
		//70 35 15
		if(player.Energy >= 70){
			powerUps[0].spawnChance = 100;
			powerUps[1].spawnChance = 25;
			powerUps[2].spawnChance = 5;
		}
		else if(player.Energy >= 30){
			powerUps[0].spawnChance = 25;
			powerUps[1].spawnChance = 100;
			powerUps[2].spawnChance = 5;
		}
		else if(player.Energy < 30){
			powerUps[0].spawnChance = 5;
			powerUps[1].spawnChance = 25;
			powerUps[2].spawnChance = 100;
		}
	}

	public void reset(){
		tileCounter = 0;
		blocksDB = new Level1DB();

		st = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];
		nd = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];
		rd = blocksDB.blocks[bs.randomInfluencedIndex(blocksDB.blocks)];
		gridColumn = 0;
		gridSize = st.grid.GetLength(1);

		tileCountTriggers.Clear();
		tileCountTriggers.Enqueue((int)DifficultyTriggers._2);
		tileCountTriggers.Enqueue((int)DifficultyTriggers._3);
		tileCountTriggers.Enqueue((int)DifficultyTriggers._4);
		tileCountTriggers.Enqueue((int)DifficultyTriggers._5);
		currentTrigger = (int) tileCountTriggers.Dequeue();
	}

	void gameStateChanged(float gs)
	{
		speed = new Vector2(gs, 0);
	}

	public int TileCounter {
		get {
			return tileCounter;
		}
	}

}
