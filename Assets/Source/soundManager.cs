using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {
	public AudioClip[] sounds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

private void PlayAudioClip(int index){
	GameObject go = new GameObject("AudioClip"); 
	MonoBehaviour.DontDestroyOnLoad(go);;
	AudioSource source = go.AddComponent<AudioSource>(); 
	source.clip = sounds[index];
 	source.Play();
  	MonoBehaviour.Destroy(go, sounds[index].length); 
	}

public void PlaySoundAttack()
{
        PlayAudioClip(0);
}

public void PlaySoundCorrect()
{
        PlayAudioClip(1);
}

public void PlaySoundFalse()
{
        PlayAudioClip(2);
}

public void PlaySoundGameOver()
{
        PlayAudioClip(3);
}
public void PlaySoundSkip()
{
        PlayAudioClip(4);
}
public void PlaySoundCleared()
{
        PlayAudioClip(5);
}
public void PlaySoundMenu()
{
        PlayAudioClip(6);
}
}
	