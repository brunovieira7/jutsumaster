using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}
	
	void Update () {
	
	}

	public void CastSeal() {
		animator.SetTrigger ("Seal");
	}
}
