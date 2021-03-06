﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

	public GameObject[] seals;
	public Player player;
	public Enemy enemy;
	private string currentCast;

	public Image timeBar;
	public float maxCastTime;
	private float elapsedTime = 0f;

	private bool jutsuStage = true;
	private Jutsu castingJutsu;
	private float castingJutsuTimer = 0f;

	public Text recordText;
	public List<GameObject> instanceSeals;

	// Use this for initialization
	void Start () {
		shuffleSeals ();
		currentCast = "";
	}

	// Update is called once per frame
	void Update () {

		updateBars ();

		if (jutsuStage) {
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
				if (Input.touchCount > 0) {
					if (Input.GetTouch (0).phase == TouchPhase.Began) {
						checkTouch (Input.GetTouch (0).position);
					}
				}
			} else if (Application.platform == RuntimePlatform.WindowsEditor) {
				if (Input.GetMouseButtonDown (0)) {
					checkTouch (Input.mousePosition);
				}
			}
		}

		if (castingJutsu != null) {
			castingJutsuTimer += Time.deltaTime;
			if (castingJutsuTimer >= castingJutsu.getActiveTimer ()) {
				Enemy enemyScript = enemy.GetComponent<Enemy> ();
				enemyScript.takeDamage ();
				castingJutsu = null;

				restartRound ();
			}
		}
	}

	void restartRound() {
		destroySeals ();
		shuffleSeals ();
		elapsedTime = 0f;
		jutsuStage = true;
		castingJutsuTimer = 0f;
		currentCast = "";
	}
		
	void updateBars() {
		if (!jutsuStage) {
			return;
		}

		elapsedTime += Time.deltaTime;

		RectTransform rect = timeBar.GetComponent<RectTransform> ();

		float newScale = (float) elapsedTime / maxCastTime;

		Vector3 currScale = rect.localScale;
		currScale.x = newScale;

		rect.localScale = currScale;

		if (elapsedTime >= maxCastTime) {
			jutsuStage = false;
			elapsedTime = 0f;
		}
	}

	private void checkTouch(Vector3 pos){
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
	     var hit = Physics2D.OverlapPoint(touchPos);
	     
	     if (hit) {
			string sealCode = hit.GetComponent<SealClick> ().getSealCode ();
			player.GetComponent<Player> ().castSeal ();

			//Debug.Log ("got code " + sealCode);
			currentCast = currentCast + sealCode;
			Jutsu jutsu = player.GetComponent<Player> ().isCastSuccessful (currentCast);
			if (jutsu != null) {
				jutsuStage = false;
				castingJutsu = jutsu;
				//jutsu.playSound ();

				if (jutsu.newBestTime (elapsedTime)) {
					recordText.text = "New  Best  Time:  " + System.Math.Round (elapsedTime, 2);
					recordText.enabled = true;
				} 
				else {
					recordText.enabled = false;
				}
			}
			//Debug.Log(hit.transform.gameObject.name);
			//Debug.Log ("DOWNZZZ" + sealNum);


	     }
	 }

	void destroySeals() { 
		foreach (GameObject go in instanceSeals) {
			Destroy (go);
		}
	}

	void shuffleSeals() {
		instanceSeals = new List<GameObject> ();
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
			instanceSeals.Add (instance);
			x++;
		}

		//GameObject instance = Instantiate (bullet, start, Quaternion.identity) as GameObject;

		//GameObject toInstantiate = seals[Random.Range (0,seals.Length)];
	}

	GameObject getRandomSeal(GameObject[] seals) {
		return seals[Random.Range (0,seals.Length)];
	}
}