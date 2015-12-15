using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public GameObject smoke;
	
	private int timesHit;
	private LevelManager levelManager;
	private int maxHits;
	private bool isBreakable;
	
	void Start () {
		isBreakable = (this.tag == "Breakable");
		
		//keep track of breakable bricks
		if (isBreakable){
			breakableCount++;
			print(breakableCount);
		}
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		AudioSource.PlayClipAtPoint (crack, transform.position, 0.25f);
		if (isBreakable){
			HandleHits();
		}
	}
	
	void HandleHits(){
		timesHit++;	
		maxHits = hitSprites.Length +1;
		if (timesHit >= maxHits){
			breakableCount--;
			//Debug.Log(breakableCount);
			levelManager.BrickDestroyed();
			PuffSmoke();
			Destroy(gameObject);
		}else{
			LoadSprites();
		}
	}
	
	void PuffSmoke(){
		GameObject smokepuff = Instantiate (smoke, transform.position, Quaternion.identity) as GameObject;
		smokepuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		
		if (hitSprites[spriteIndex] != null){
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}else{
			Debug.LogError("Brick sprite missing");
		}
	}
	//TODO Remove this method once we can actually win!
	void SimulateWin(){
		levelManager.LoadNextLevel();
	}
}
