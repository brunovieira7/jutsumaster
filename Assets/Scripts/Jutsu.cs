using UnityEngine;
using System.Collections;

public class Jutsu {

	enum JutsuType { DMG, BUFF }

	private string sealCode;
	private string trigger;
	private JutsuType type;

	private float activeTimer;

	public Jutsu (string sealCode, string trigger, float activeTimer) { 
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

	public void playSound() {
		AudioClip audio = Resources.Load("chidori") as AudioClip;
		SoundManager.instance.PlaySingle (audio);
	}
}
