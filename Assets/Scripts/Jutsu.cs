using UnityEngine;
using System.Collections;

public class Jutsu {

	enum JutsuType { DMG, BUFF }

	private int id;
	private string sealCode;
	private string trigger;
	private JutsuType type;
	private float recordTimer = 100f;

	private float activeTimer;

	public Jutsu (int id, string sealCode, string trigger, float activeTimer) {
		getRecordTimer ();

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

	public float getRecordTimer() {
		// call API

		return recordTimer;
	}

	public bool newBestTime(float newTimer) {
		if (newTimer < recordTimer) {
			recordTimer = newTimer;
			return true;
		}
		return false;
	}

	public void playSound() {
		AudioClip audio = Resources.Load("chidori") as AudioClip;
		SoundManager.instance.PlaySingle (audio);
	}
}
