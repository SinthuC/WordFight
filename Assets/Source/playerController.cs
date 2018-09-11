using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

	// Use this for initialization
	public soundManager soundManager;
	public  Text hpPoint;
	public  int playerHp = 20;
	public int playerMaxHp = 20;
	public bool isAlive = true;
	public  Rigidbody2D rigid;
	public  Animator animation;
	public enemyController enemy;
	public static bool isPlayerAttack;
	public Button[] choiceBtn = new Button[4];
	public Button[] skillBtn = new Button[3];
	public Button skipBtn;
	void Start () {
		hpPoint.text = ""+playerHp+"/"+playerMaxHp;
		isPlayerAttack = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public  void attack () {
		isPlayerAttack = true;
		animation.SetTrigger ("attack");
		for (int i = 0; i < choiceBtn.Length; i++) {
			choiceBtn[i].interactable = false;
		}
		//enemy.CounterAttackTime = Time.time;
		StartCoroutine(WaitAnswerBack());
		StartCoroutine(enemy.WaitAutoAttack());
	
	}

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log (isPlayerAttack);
		if (isPlayerAttack) {
			enemy.enemyHp -= 5;
			enemy.hpPoint.text = "" + enemy.enemyHp+"/"+enemy.enemyMaxHp;


		}
			//playerHp -= 5;
			//hpPoint.text = "" + playerHp;
		

	}

	IEnumerator WaitAnswerBack() {
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < choiceBtn.Length; i++) {
			choiceBtn[i].interactable = true;
		}

}
}