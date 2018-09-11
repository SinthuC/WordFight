using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour {

	public soundManager soundManager;
	public  Text hpPoint;
	public  int enemyHp = 20;
	public  int enemyMaxHp = 20;
	public  int enemyDmg = 5;
	public bool isAlive = true;
	public  static float CounterAttackTime; 
	public  Rigidbody2D rigid;
	public Animator animation;
	public playerController player;
	public Button[] choiceBtn = new Button[4];
	public Button[] skillBtn = new Button[3];
	public Button skipBtn;
	void Start () {
		hpPoint.text = ""+enemyHp+"/"+enemyMaxHp;
		//InvokeRepeating("attack", 5, 5);
		StartCoroutine (WaitAutoAttack ());
	}

	// Update is called once per frame
	void Update () {

	}

	public  void attack () {
		playerController.isPlayerAttack = false;
		for (int i = 0; i < choiceBtn.Length; i++) {
			choiceBtn[i].interactable = false;
		}
		skipBtn.interactable = false;
		animation.SetTrigger ("eAttack");
		StartCoroutine(WaitAnswerBack());
		StartCoroutine (WaitAutoAttack ());

	}

	void OnTriggerEnter2D(Collider2D other){
		soundManager.PlaySoundAttack();
		if (!playerController.isPlayerAttack) {
			player.playerHp -= enemyDmg;
			player.hpPoint.text = "" + player.playerHp+"/"+player.playerMaxHp;;
			enemyDmg = 5;
		}

	}
		
	IEnumerator WaitAnswerBack() {
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < choiceBtn.Length; i++) {
			choiceBtn[i].interactable = true;
		}
		skipBtn.interactable = true;
}

	 public IEnumerator WaitAutoAttack() {
		CounterAttackTime = Time.time;
		yield return new WaitForSeconds(5f);
		if (Time.time >= CounterAttackTime + 5f && (isAlive && player.isAlive)) {
			Debug.Log (Time.time);
			attack ();
		}
	}




}
