using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
        
    public AudioClip loadSound;
    private AudioSource loading;
	
    
    void Start () {
        loading = GetComponent<AudioSource>();
		
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.J) ) {
            Play ();
		}
	}

    IEnumerator WaitForSound(){
        yield return new WaitForSeconds(3.2f);
        LoadLevel();
    }
    
	private void Play() {
			Pause.paused = false;
            loading.PlayOneShot(loadSound,1);
			StartCoroutine (WaitForSound());
	}
    
    private void LoadLevel () {
		SceneManager.LoadScene ("simpsonswave");
    }
}
