using UnityEngine;
using System.Collections;

public class SealClick : MonoBehaviour {

	public string sealCode;

	void OnClick() {
		Debug.Log ("CLICK");
	}

	void OnMouseDown() { 
		Debug.Log ("mousedown");
	}

	public string getSealCode() {
		return sealCode;
	}
		
}
