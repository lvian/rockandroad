using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSpawner : MonoBehaviour {

	protected GameControl gc;
	protected Player player;
	public GameObject[] spawnPoints;
	public Vector2 speed;
	public float timer;
	public int tileCounter;
	public bool isDouble;
	public Queue tileCountTriggers;

	private Difficulty level1;
	private Difficulty level2;
	private Difficulty level3;
	private Difficulty level4;
	private Difficulty level5;
	private BlockSpawner bs;
	public Difficulty difficulty;
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
		_3 = 250,
		_4 = 350,
		_5 = 550,
	}

	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		player = GameObject.Find("Player").GetComponent<Player>();
		gc.stateChanged += gameStateChanged;

		level1 = GameObject.Find("Level1").GetComponent<Level1>();
		level2 = GameObject.Find("Level2").GetComponent<Level2>();
		level3 = GameObject.Find("Level3").GetComponent<Level3>();
		//level4 = GameObject.Find("Level4").GetComponent<Level4>();
		//level5 = GameObject.Find("Level5").GetComponent<Level5>();
		
		speed = new Vector2(gc.GameSpeed , 0);
		timer = 2f;

		bs = BlockSpawner.Instance;
		difficulty = level1;

		st = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];
		nd = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];
		rd = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];

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
				rd = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];
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
		int index = isDoubleObstaclePossible(column);
		//Random.Range(0f, 100f) < 10f && 

		if(index >= 0 && Random.Range(0f, 100f) > 50f && difficulty.doubleObjects.Length > 0){
			Debug.Log(index + " {" + column[0] + "," + column[1] +"," + column[2]+"," + column[3] + "}");
			switch(index){
			case 1:
				isDouble = true;
				spawnTile(column[1], 1);
				spawnTile(column[2], 2);
				spawnTile(column[3], 3);
				break;
			case 2:
				spawnTile(column[0], 0);
				isDouble = true;
				spawnTile(column[1], 1);
				spawnTile(column[3], 3);
				break;
			case 3:
				spawnTile(column[0], 0);
				spawnTile(column[1], 1);
				isDouble = true;
				spawnTile(column[2], 2);
				break;
			}
		}
		else{
			spawnTile(column[0], 0);
			spawnTile(column[1], 1);
			spawnTile(column[2], 2);
			spawnTile(column[3], 3);
		}
		tileCounter++;
		multiplierCounter--;
		changeDifficulty();
	}

	int isDoubleObstaclePossible(int[] column){
		bool tmp = false;
		for(int c = 0; c < 4; c++){
			if(column[c] == 1){
				if(!tmp)
					tmp = true;
				else
					return c;
			}
			else
				tmp = false;
		}
		return -1;
	}

	void spawnTile(int tile, int spawner){
		int rand = 0;
		GameObject obs = null;
		switch(tile){
		case 0:
			if(multiplierCounter <= 0){
				obs = (GameObject) Instantiate(difficulty.multiplierObjects[0].gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
				obs.transform.parent = spawnPoints[spawner].transform;
				multiplierCounter = 30;
			}
			break;
		case 1:
			if(!isDouble){
				rand = bs.randomInfluencedIndex(difficulty.staticObjects);
				obs = (GameObject) Instantiate(difficulty.staticObjects[rand].gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
				obs.transform.parent = spawnPoints[spawner].transform;
			}
			else{
				rand = bs.randomInfluencedIndex(difficulty.doubleObjects);
				obs = (GameObject) Instantiate(difficulty.doubleObjects[rand].gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
				obs.transform.parent = spawnPoints[spawner].transform;
				isDouble = false;
			}
			break;
		case 2:
			rand = bs.randomInfluencedIndex(difficulty.movableObjects);
			obs = (GameObject) Instantiate(difficulty.movableObjects[rand].gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			SpawnableObject so = obs.GetComponent<SpawnableObject>();
			so.lanes = spawnPoints;
			so.spawnLane = spawner;
			break;
		case 3:
			rand = bs.randomInfluencedIndex(difficulty.powerUpObjects);
			obs = (GameObject) Instantiate(difficulty.powerUpObjects[rand].gameObject, spawnPoints[spawner].transform.position, spawnPoints[spawner].transform.rotation); 
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
				difficulty = level2;
				break;
			case (int)DifficultyTriggers._3:
				difficulty = level3;
				break;
			default:
				break;
			}
			currentTrigger = (int) tileCountTriggers.Dequeue();
			nd = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];
			rd = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];
		}
	}

	private void defineHealthBoostPriority(){
		//70 35 15
		if(player.Energy >= 70){
			difficulty.powerUpObjects[0].spawnChance = 100;
			difficulty.powerUpObjects[1].spawnChance = 25;
			difficulty.powerUpObjects[2].spawnChance = 5;
		}
		else if(player.Energy >= 30){
			difficulty.powerUpObjects[0].spawnChance = 25;
			difficulty.powerUpObjects[1].spawnChance = 100;
			difficulty.powerUpObjects[2].spawnChance = 5;
		}
		else if(player.Energy < 30){
			difficulty.powerUpObjects[0].spawnChance = 5;
			difficulty.powerUpObjects[1].spawnChance = 25;
			difficulty.powerUpObjects[2].spawnChance = 100;
		}
	}

	public void reset(){
		tileCounter = 0;
		difficulty = level1;

		st = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];
		nd = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];
		rd = difficulty.blocks[bs.randomInfluencedIndex(difficulty.blocks)];
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
