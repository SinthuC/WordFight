using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	
	void Awake(){
		Application.targetFrameRate = 120;
	}

	public soundManager soundManager;
	public void playVocabMode(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
		soundManager.PlaySoundMenu();
	}

	public void playPictureMode(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+2);
		soundManager.PlaySoundMenu();
	}

	public void playQuestionMode(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+3);
		soundManager.PlaySoundMenu();
	}

}
