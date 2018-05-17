using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TextMesh instructionText;
    private const string toggleWorldInstruction = "alt / h : switch worlds";
    private int levelStage = 0;

    public void FirstNetherSpawn()
    {
        levelStage = 1;
        ShowWorldToggleInstruction();
    }

    public void EnemyKilled()
    {
        if(levelStage == 1)
        {
            levelStage = 2;
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
