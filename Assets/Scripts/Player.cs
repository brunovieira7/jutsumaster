using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;
	public AudioClip sealCast;
	public AudioClip sealComplete;

	private Jutsu[] jutsus;

	void Start () {
		animator = GetComponent<Animator>();
		jutsus = new Jutsu[1];
		jutsus [0] = new Jutsu ("HoHoHo","Chidori",2f);
	}
	
	void Update () {
	
	}

	public void castSeal() {
		animator.SetTrigger ("Seal");
		SoundManager.instance.PlaySingle (sealCast);
	}

	public Jutsu isCastSuccessful(string currentCast) {
		foreach (Jutsu jutsu in jutsus) {
			Debug.Log ("jutsucode: " + jutsu.getSealcode () + " curr:" + currentCast);			
			if (jutsu.match (currentCast)) {
				SoundManager.instance.PlaySingle (sealComplete);
				animator.SetTrigger (jutsu.getTrigger() );

				return jutsu;
			}
		}
		return null;
	}
}
