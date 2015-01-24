using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSpawner : MonoBehaviour {

	protected GameControl gc;
	public SpawnableObject[] staticObstacles;
	public SpawnableObject[] movableObstacles;
	public SpawnableObject[] powerUps;
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
	private List<MapBlock> blocks;
	private MapBlock st; //1st block of tiles
	private MapBlock nd; //2nd block of tiles
	private MapBlock rd; //3rd block of tiles

	private int gridColumn;
	private int gridSize;
	private int currentTrigger;

	// Use this for initialization
	void Start () {
		//Subscribing to receive event stateChanged from GameControll, if so, calls gameStateChanged	
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.stateChanged += gameStateChanged;
		
		speed = new Vector2(gc.GameSpeed , 0);
		timer = 2f;

		level1 = new Level1DB();
		level2 = new Level2DB();
		level3 = new Level3DB();

		bs = BlockSpawner.Instance;
		blocks = level1.blocks;
		if(blocks == null){
			level1 = new Level1DB();
			blocks = level1.blocks;
		}

		st = blocks[bs.randomInfluencedIndex(blocks)];
		nd = blocks[bs.randomInfluencedIndex(blocks)];
		rd = blocks[bs.randomInfluencedIndex(blocks)];

		gridColumn = 0;
		gridSize = st.grid.GetLength(1);
		tileCounter = 0;
		tileCountTriggers = new Queue();
		tileCountTriggers.Enqueue(20);
		tileCountTriggers.Enqueue(50);
		tileCountTriggers.Enqueue(350);
		tileCountTriggers.Enqueue(550);
		currentTrigger = (int) tileCountTriggers.Dequeue();
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
				rd = blocks[bs.randomInfluencedIndex(blocks)];
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
		changeDifficulty();
	}

	void spawnTile(int tile, int spawner){
		switch(tile){
		case 1:
			int rand = bs.randomInfluencedIndex(staticObstacles);
			GameObject obs = (GameObject) Instantiate(staticObstacles[rand].gameObject , spawnPoints[spawner].transform.position , spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			break;
		case 2:
			rand = bs.randomInfluencedIndex(movableObstacles);
			obs = (GameObject) Instantiate(movableObstacles[rand].gameObject , spawnPoints[spawner].transform.position , spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			SpawnableObject so = obs.GetComponent<SpawnableObject>();
			so.lanes = spawnPoints;
			so.spawnLane = spawner;
			break;
		case 3:
			rand = bs.randomInfluencedIndex(powerUps);
			obs = (GameObject) Instantiate(powerUps[rand].gameObject , spawnPoints[spawner].transform.position , spawnPoints[spawner].transform.rotation); 
			obs.transform.parent = spawnPoints[spawner].transform;
			break;
		default:
			break;
		}
	}

	int[] generateTransitoryColumn(MapBlock previous, MapBlock next){
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
			case 20:
				blocks = level2.blocks;
				break;
			case 50:
				blocks = level3.blocks;
				break;
			}
			currentTrigger = (int) tileCountTriggers.Dequeue();
		}
	}

	public void reset(){
		tileCounter = 0;
		blocks = level1.blocks;
	}

	void gameStateChanged(float gs)
	{
		speed = new Vector2(gs, 0);
	}
}
