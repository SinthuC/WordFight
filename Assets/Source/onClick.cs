using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : MonoBehaviour {

	public static int isAttack;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void correct () {
		isAttack = 1;
		
}
	public void notCorrect () {
		isAttack = 2;
}

	public void restartGame () {
		Debug.Log ("dfdf");
		Application.LoadLevel("QuizGame");
	}

}
