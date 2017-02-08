using UnityEngine;
using System.Collections;

public class Jutsu {

	private string sealCode;
	private string trigger;

	public Jutsu (string sealCode, string trigger) { 
		this.sealCode = sealCode;
		this.trigger = trigger;
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
}
