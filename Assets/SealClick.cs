using UnityEngine;
using System.Collections;

public class SealClick : MonoBehaviour {

	public int sealNumber;

	void OnClick() {
		Debug.Log ("CLICK");
	}

	void OnMouseDown() { 
		Debug.Log ("mousedown");
	}

	public int getSealNumber() {
		Debug.Log ("DOWN" + sealNumber);
		return sealNumber;
	}
		
}
