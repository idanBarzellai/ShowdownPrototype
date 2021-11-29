using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOptionsMenu : MonoBehaviour
{
    public TMP_Dropdown artifactSpawnDropdown;
    public TMP_Dropdown healthPointsDropdown;
    public TMP_Dropdown numOfPlayersDropdown;

    GameManager gameManager;
    List<int> artifactSetPauseTimesInt;
    List<int> hpSetValuesInt;
    private void Start()
    {
        gameManager = GameManager.instance;
        List<TMP_Dropdown.OptionData> hpSetValuesString = healthPointsDropdown.options;
        hpSetValuesInt = new List<int>();

        for (int i = 0; i < hpSetValuesString.Count; i++)
        {
            string option = hpSetValuesString[i].text;
            int hpOptionInt = int.Parse(option.Split(' ')[0]);
            hpSetValuesInt.Add(hpOptionInt);
        }

        List<TMP_Dropdown.OptionData> artifactPauseTimesString = artifactSpawnDropdown.options;
        artifactSetPauseTimesInt = new List<int>();

        for (int i = 0; i < artifactPauseTimesString.Count; i++)
        {
            string option = artifactPauseTimesString[i].text;
            int pauseTimeOptionInt = int.Parse(option.Split(' ')[0]);
            artifactSetPauseTimesInt.Add(pauseTimeOptionInt);
        }

        gameManager.baseHealth = hpSetValuesInt[2];
        gameManager.artifactDeployPause = artifactSetPauseTimesInt[0];

        healthPointsDropdown.value = 2;
        healthPointsDropdown.RefreshShownValue();

    }
    
    public void SetHpForGame(int hpValueIndex)
    {
        gameManager.baseHealth = hpSetValuesInt[hpValueIndex];
    }

    public void SetNumOfPlayers()
    {

    }

    public void SetArtifactSpawnTIme(int spawnTimePauseIndex)
    {
        gameManager.artifactDeployPause = artifactSetPauseTimesInt[spawnTimePauseIndex];
    }
}
