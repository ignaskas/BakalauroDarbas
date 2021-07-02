using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingLogic : MonoBehaviour
{
    public Main_Behaviour mainBehaviour;
    public Button restart;
    [SerializeField]
    private int chanceToWin = 50;


    //show restart button add click listener to it
    public void GameEnding()
    {
        this.gameObject.SetActive(true);
        restart.onClick.AddListener(buttonCallBackRestart);
        RandomOutcomeGenerator(mainBehaviour.PlayerPoint, chanceToWin);
    }

    //restart the game
    private void buttonCallBackRestart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
    //generate a int value from 0 to 100 compare it to chanceToWin variable to determine the outcome 
    private void RandomOutcomeGenerator(Main_Behaviour.GameState ending, int winChance)
    {
        var endingRoll = UnityEngine.Random.Range(0, 100);// Random.Range is inclusive only if its int for some reason
        mainBehaviour.storyPlate.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Story_Board/Story_Template");
        mainBehaviour.storyPlate.SetActive(true);
        switch (ending.Name)
        {
            case "miningCampStealEnding":
                ChangeStoryPlateForEnding(endingRoll >= winChance ? "BitCoinStealSucces" : "BitCoinStealFail");
                break;
            case "failToWinEnding":
                
                break;
            case "FleeEnding":
                ChangeStoryPlateForEnding(endingRoll >= winChance ? "FleeDeath" : "FleeWin");
                break;
            case "mountainsEnding":
                ChangeStoryPlateForEnding(endingRoll >= winChance ? "MountainsEndDeath" : "MountainsEnd");
                break;
            default:
                Debug.Log("Wrong State past to ending script" + ending.Name);
                break;
        }
    }

    private void ChangeStoryPlateForEnding(string endingName)
    {
        int putTextInToArrayIndex = 0;
        foreach (var textLine in mainBehaviour.StoryDictionary[endingName].text.SplitToLines())
        {
            mainBehaviour.StoryText[putTextInToArrayIndex].text = textLine;
            putTextInToArrayIndex++;
        }
    }
    
}
