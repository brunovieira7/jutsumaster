using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class ApiInfo : MonoBehaviour {

	private float jutsuTime;

	public Text startGame;

	private bool canStart = false;

	// Use this for initialization
	void Start () {
		startGame.enabled = false;
		StartCoroutine(GetText());

		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator GetText()
	{
		using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/timer/1"))
		{
			yield return www.Send();

			if (www.isError)
			{
				Debug.Log(www.error + " " + www.responseCode);
			}
			else
			{
				// Show results as text

				Debug.Log(www.downloadHandler.text);

				var responseJson = JSON.Parse (www.downloadHandler.text);
				float time = responseJson ["time"].AsFloat;

				jutsuTime = time;

				// Or retrieve results as binary data
				byte[] results = www.downloadHandler.data;

				canStart = true;
				startGame.text = "Click to Start";
				startGame.enabled = true;
			}
		}
	}

	public bool canStartgame() { 
		return canStart;
	}

	public float getJutsuTime() {
		return jutsuTime;
	}
}
