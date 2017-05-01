using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Jutsu {

	enum JutsuType { DMG, BUFF }

	private int id;
	private string sealCode;
	private string trigger;
	private JutsuType type;
	private float recordTimer = 100f;

	private float activeTimer;

	public Jutsu (int id, string sealCode, string trigger, float activeTimer) {
		setRecordTimer ();

		this.id = id;
		this.sealCode = sealCode;
		this.trigger = trigger;
		this.activeTimer = activeTimer;
	}

	public string getSealcode() {
		return sealCode;
	}

	public string getTrigger() {
		return trigger;
	}

	public bool match(string cast) {
		if (cast.Contains (sealCode)) {
			return true;
		}
		return false;
	}

	public float getActiveTimer() {
		return activeTimer;
	}

	public void setRecordTimer() {
		GameObject apiInfo = GameObject.Find ("ApiInfo");
		recordTimer = apiInfo.GetComponent<ApiInfo> ().getJutsuTime ();
	}

	public bool newBestTime(float newTimer) {
		if (newTimer < recordTimer) {
			recordTimer = newTimer;
			TaskOnClick (newTimer);
			return true;
		}
		return false;
	}

	public void playSound() {
		AudioClip audio = Resources.Load("chidori") as AudioClip;
		SoundManager.instance.PlaySingle (audio);
	}
		
	private void TaskOnClick(float newTimer) {
		try	{
			string ourPostData = "{\"id\":"+ id +", \"time\": " + newTimer + " }";
			Dictionary<string,string> headers = new Dictionary<string, string>();
			headers.Add("Content-Type", "application/json");

			byte[] pData = System.Text.Encoding.ASCII.GetBytes(ourPostData.ToCharArray());

			WWW api = new WWW("http://localhost:8080/timer", pData, headers);
			//WaitForWWW(api);
		}
			catch (UnityException ex) { Debug.Log(ex.Message);
		}
	}
}
