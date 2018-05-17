using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {
	public int enemiesKilled = 0;
	public TextMesh ralphCounterText;
    public TextMesh instructionText;
    private const string toggleWorldInstruction = "alt / h : switch worlds";


    void Start () {
		ralphCounterText = gameObject.GetComponent<TextMesh> ();		
	}

	public void enemyKilled() {
		enemiesKilled++;
		ralphCounterText.text = "Ralphs Bullied: " + enemiesKilled;
        if(enemiesKilled == 1)
        {
            ShowWorldToggleInstruction();
        } else if (enemiesKilled == 2)
        {
            HideWorldToggleInstruction();
        }
	}

    private void ShowWorldToggleInstruction()
    {
        instructionText.text = toggleWorldInstruction;
    }

    private void HideWorldToggleInstruction()
    {
        instructionText.text = "";
    }
}
