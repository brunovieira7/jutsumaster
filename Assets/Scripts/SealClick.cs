using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SealClick : MonoBehaviour {

	public string sealCode;
	public string sealName;
	public Text text;

	void Start() {
		text.text = sealName;
	}

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
