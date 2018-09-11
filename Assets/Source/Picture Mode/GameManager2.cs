using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;	

public class GameManager2 : MonoBehaviour {
	public soundManager soundManager;
	public playerController player;
	public enemyController enemy;
	public Animator[] animation = new Animator[2];
	public Picture[] pics;
	private static List<Picture> unSelectPictures ;
	private static List<Picture> unSelectChoices ;
	private Picture currentPicture;
	private Picture currentChoice;
	private int currentIndexPicture;
	private bool oneTime = false;

	[SerializeField]
	private Image image;

	[SerializeField]
	private Text[] ChoiceText = new Text[4];

	[SerializeField]
	private Text Alert;

	private float timeBetweenPicture = 0.5f;

	void Start(){
		RandomPic ();
		SetChoiceList ();
	}

	void Update(){
		if (player.playerHp <= 0) {
			player.isAlive = false;
			player.animation.SetTrigger ("dead");
			Invoke ("showGameOver", 1.8f);
			//player.ans1.interactable = false;
			//player.ans2.interactable = false;

		}

		if (enemy.enemyHp <= 0) {
			enemy.isAlive = false;
			enemy.animation.SetTrigger ("dead");
			Invoke ("showCleared", 1.8f);
			//player.ans1.interactable = false;
			//player.ans2.interactable = false;

		}
	}


	void RandomPic(){
		//ReChoices
		unSelectChoices = pics.ToList<Picture>();
		//ReWord
		if(unSelectPictures==null || unSelectPictures.Count == 0){
			unSelectPictures = pics.ToList<Picture>();
		}
		//RandomIndex
		int randomPictureIndex = Random.Range (0, unSelectPictures.Count);
		currentPicture = unSelectPictures [randomPictureIndex];
		//SetText
		image.sprite = currentPicture.picture;
		//SetCurrentIndex
		currentIndexPicture = randomPictureIndex;
	}
		
	int SetChoice(){
		int ChoiseAt = Random.Range (0, 4);
		Debug.Log ("ตอบข้อ : "+(ChoiseAt+1));
		//SetText
		ChoiceText[ChoiseAt].text = currentPicture.wordTh;
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
		while(currentPicture.wordTh==unSelectChoices[randomChoiseIndex].wordTh){
			randomChoiseIndex = Random.Range (0, unSelectChoices.Count);
		}
		//SetText
		currentChoice = unSelectChoices [randomChoiseIndex];
		ChoiceText[i].text = currentChoice.wordTh;
		//RemoveUsedChoiceFromList
		unSelectChoices.RemoveAt (randomChoiseIndex);	
	}



	IEnumerator TransitionToNextWord(){
		//RemovecurrentPictureFromList
		unSelectPictures.Remove (currentPicture); 
		//WaitTime
		yield return new WaitForSeconds (timeBetweenPicture);
		Alert.text ="";
		image.gameObject.SetActive(true);
		//Start
		RandomPic ();
		SetChoiceList();
		//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	void Skip(){
		//ReChoices
		soundManager.PlaySoundSkip();
		//ReWord
		unSelectPictures = pics.ToList<Picture>();
		unSelectChoices = pics.ToList<Picture>();
		//RandomWord With Not Same 
		int randomPictureIndex = Random.Range (0, unSelectPictures.Count);
		while(currentPicture.wordTh==unSelectPictures[randomPictureIndex].wordTh){
			randomPictureIndex = Random.Range (0, unSelectPictures.Count);
		}
		currentPicture = unSelectPictures [randomPictureIndex];
		image.sprite = currentPicture.picture;
		currentIndexPicture = randomPictureIndex;
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
		if (ChoiceText [postion].text == currentPicture.wordTh) {
			soundManager.PlaySoundCorrect();
			Debug.Log ("TRUE!!"); 
			image.gameObject.SetActive(false);
			Alert.text  = "CORRECT!!";
			player.attack ();
		} else {
			soundManager.PlaySoundFalse();
			Debug.Log ("FALSE!!");
			image.gameObject.SetActive(false);
			Alert.text = "WRONG!!";
			enemy.attack ();
		}
		StartCoroutine (TransitionToNextWord());
	}

	public void showGameOver() {
		animation[0].SetBool ("isOver", true);
		animation[0].gameObject.SetActive (true);
		if(!oneTime){
			soundManager.PlaySoundGameOver();
			oneTime=true;
		}
	}

	public void showCleared() {
		animation[1].SetBool ("isCleared", true);
		animation[1].gameObject.SetActive (true);
		if(!oneTime){
			soundManager.PlaySoundCleared();
			oneTime=true;
		}
	}

	public void restartGame () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
		
	public void mainMenu(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex-2);
	}
}