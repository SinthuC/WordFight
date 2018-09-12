using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;	

public class GameManager : MonoBehaviour {
	public soundManager soundManager;
	public playerController player;
	public enemyController enemy;
	public Animator[] animation = new Animator[2];
	public Word[] words;
	public DatabaseController databaseController;

	private int correctCount = 0;
	private int incorrectCount = 0;
	private int isWinRecord;
	private static List<Word> unSelectWords ;
	private static List<Word> unSelectChoices ;
	private Word currentWord;
	private Word currentChoice;
	private int currentIndexWord;
	private bool oneTime = false;

	[SerializeField]
	private Text WordText;

	[SerializeField]
	private Text[] ChoiceText = new Text[4];

	[SerializeField]
	private Text Alert;

	private float timeBetweenWord = 0.5f;

	void Start(){
		RandomWord ();
		SetChoiceList ();
	}

	void Update(){
		if (player.playerHp <= 0) {
			player.isAlive = false;
			player.animation.SetTrigger ("dead");
			Invoke ("showGameOver", 1.6f);
			//player.ans1.interactable = false;
			//player.ans2.interactable = false;

		}

		if (enemy.enemyHp <= 0) {
			enemy.isAlive = false;
			enemy.animation.SetTrigger ("dead");
			Invoke ("showCleared", 1.6f);
			//player.ans1.interactable = false;
			//player.ans2.interactable = false;

		}
	}


	void RandomWord(){
		//ReChoices
		unSelectChoices = words.ToList<Word>();
		//ReWord
		if(unSelectWords==null || unSelectWords.Count == 0){
			unSelectWords = words.ToList<Word>();
		}
		//RandomIndex
		int randomWordIndex = Random.Range (0, unSelectWords.Count);
		currentWord = unSelectWords [randomWordIndex];
		//SetText
		WordText.text = currentWord.wordEng;
		//SetCurrentIndex
		currentIndexWord = randomWordIndex;
	}
		
	int SetChoice(){
		int ChoiseAt = Random.Range (0, 4);
		Debug.Log ("ตอบข้อ : "+(ChoiseAt+1));
		//SetText
		ChoiceText[ChoiseAt].text = currentWord.wordTh;
		return ChoiseAt;
	}

	void SetChoiceList(){
		int CorrectPostion = SetChoice ();
		for(int i=0;i<4;i++){
			if(i!=CorrectPostion){
				RandomChoice (i);
			}

		}
	}

	void RandomChoice(int i){
		//RandomIndex
		int randomChoiseIndex = Random.Range (0, unSelectChoices.Count);
		//Check RandomChoice must not like Word
		while(currentWord.wordTh==unSelectChoices[randomChoiseIndex].wordTh){
			randomChoiseIndex = Random.Range (0, unSelectChoices.Count);
		}
		//SetText
		currentChoice = unSelectChoices [randomChoiseIndex];
		ChoiceText[i].text = currentChoice.wordTh;
		//RemoveUsedChoiceFromList
		unSelectChoices.RemoveAt (randomChoiseIndex);	
	}



	IEnumerator TransitionToNextWord(){
		//RemoveCurrentWordFromList
		unSelectWords.Remove (currentWord); 
		//WaitTime
		yield return new WaitForSeconds (timeBetweenWord);
		Debug.Log (timeBetweenWord);
		//Start
		RandomWord ();
		SetChoiceList();
		//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	void Skip(){
		//ReChoices
		soundManager.PlaySoundSkip();
		unSelectWords = words.ToList<Word>();
		unSelectChoices = words.ToList<Word>();
		//RandomWord With Not Same 
		int randomWordIndex = Random.Range (0, unSelectWords.Count);
		while(currentWord.wordTh==unSelectWords[randomWordIndex].wordTh){
			randomWordIndex = Random.Range (0, unSelectWords.Count);
		}
		currentWord = unSelectWords [randomWordIndex];
		WordText.text = currentWord.wordEng;
		currentIndexWord = randomWordIndex;
		//SetChoice
		SetChoiceList();
		//SetDmg
		enemy.enemyDmg=Mathf.RoundToInt(enemy.enemyDmg*0.7f);
		enemy.attack ();
	}

	public void UserSelectSkip(){
		Skip();
	}

	public void UserSelect1(){
		isTrue (0);
	}

	public void UserSelect2(){
		isTrue (1);
	}

	public void UserSelect3(){
		isTrue (2);
	}

	public void UserSelect4(){
		isTrue (3);
	}

	public void isTrue(int postion){
		if (ChoiceText [postion].text == currentWord.wordTh) {
			soundManager.PlaySoundCorrect();
			Debug.Log ("TRUE!!");
			WordText.text  = "CORRECT!!";
			player.attack ();
			correctCount++;
		} else {
			soundManager.PlaySoundFalse();
			Debug.Log ("FALSE!!");
			WordText.text = "WRONG!!";
			enemy.attack ();
			incorrectCount++;
		}
		StartCoroutine (TransitionToNextWord());
	}

	public void showGameOver() {
		animation[0].SetBool ("isOver", true);
		animation[0].gameObject.SetActive (true);
		if(!oneTime){
		soundManager.PlaySoundGameOver();
		oneTime=true;
		databaseController.insertvalue(correctCount,incorrectCount,0); 
		}

	}

	public void showCleared() {
		animation[1].SetBool ("isCleared", true);
		animation[1].gameObject.SetActive (true);
		if(!oneTime){
		soundManager.PlaySoundCleared();
		oneTime=true;
		databaseController.insertvalue(correctCount,incorrectCount,1); 
		}

	}

	public void restartGame () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
		
	public void mainMenu(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex-1);
	}


}