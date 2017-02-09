using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject[] seals;
	public Player player;
	private string currentCast;

	// Use this for initialization
	void Start () {
		shuffleSeals ();
		currentCast = "";
	}

	// Update is called once per frame
	void Update () {
		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){
	         if(Input.touchCount > 0) {
	             if(Input.GetTouch(0).phase == TouchPhase.Began){
	                 checkTouch(Input.GetTouch(0).position);
	             }
	         }
		}
		else if(Application.platform == RuntimePlatform.WindowsEditor){
	         if(Input.GetMouseButtonDown(0)) {
	             checkTouch(Input.mousePosition);
	         }
	     }
	}

	private void checkTouch(Vector3 pos){
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
	     var hit = Physics2D.OverlapPoint(touchPos);
	     
	     if(hit){
			string sealCode = hit.GetComponent<SealClick> ().getSealCode ();
			player.GetComponent<Player> ().castSeal ();

			Debug.Log ("got code " + sealCode);
			currentCast = currentCast + sealCode;
			bool success = player.GetComponent<Player> ().isCastSuccessful (currentCast);
			//Debug.Log(hit.transform.gameObject.name);
			//Debug.Log ("DOWNZZZ" + sealNum);


	     }
	 }

	void shuffleSeals() {

		GameObject[] sealClone = (GameObject[])seals.Clone ();

		float y = 1.1f;
		int x = -2;


		for (int i = 0; i < seals.Length; i++) {
			GameObject instance = getRandomSeal (sealClone);

			List<GameObject> list = new List<GameObject>(sealClone);
			list.Remove (instance);
			sealClone = list.ToArray ();

			float xOffset = (float) ((i % 4) * 0.3);

			if (i % 4 == 0) {
				y -= 1.7f;
				x = -2;
			}
			instance = Instantiate (instance, new Vector3(x + xOffset,y,0), Quaternion.identity) as GameObject;
			x++;
		}

		//GameObject instance = Instantiate (bullet, start, Quaternion.identity) as GameObject;

		//GameObject toInstantiate = seals[Random.Range (0,seals.Length)];
	}

	GameObject getRandomSeal(GameObject[] seals) {
		return seals[Random.Range (0,seals.Length)];
	}
}