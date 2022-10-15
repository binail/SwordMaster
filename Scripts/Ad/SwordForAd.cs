using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordForAd : MonoBehaviour
{
    [SerializeField] private GameObject[] swordsForeground;
    [SerializeField] private GameObject[] swordsBackground;
    [SerializeField] private GameObject remained;
    [SerializeField] private GameObject checkAd;

    public static int needForSword = 5;
    private int countOfGames;

    private Dictionary<int, int> currentSword = new Dictionary<int, int>()
    {
        { 3, 0},
        { 11, 1},
        { 25, 2},
        { 35, 3},
        { 45, 4},
    };

    private void OnEnable()
    {
        int _levelNumber = PlayerPrefs.GetInt("levelNumber");
        int currentAdSword;

        switch (_levelNumber)
        {
            case < 11:
                currentAdSword = 0;
                break;

            case < 25:
                currentAdSword = 1;
                break;

            case < 35:
                currentAdSword = 2;
                break;

            case < 45:
                currentAdSword = 3;
                break;

            case >= 45:
                currentAdSword = 4;
                break;
        }

        if (_levelNumber == 3 || _levelNumber == 11 || _levelNumber == 25 || _levelNumber == 35 || _levelNumber == 45)
        {
            PlayerPrefs.SetInt("GamesForSword", 0);
        }

        countOfGames = PlayerPrefs.GetInt("GamesForSword");

        if (countOfGames == 0) countOfGames++;

        swordsForeground[currentAdSword].SetActive(true);
        swordsBackground[currentAdSword].SetActive(true);
        
        if(countOfGames < needForSword)
        {
            remained.SetActive(true);
            remained.GetComponent<Text>().text = countOfGames + "/" + needForSword + " ready!";
            
            swordsForeground[currentAdSword].GetComponent<Image>().fillAmount = (countOfGames * 1.0f) / (needForSword * 1.0f);

            countOfGames++;
            PlayerPrefs.SetInt("GamesForSword", countOfGames);
        }
        else
        {
            checkAd.SetActive(true);
            PlayerPrefs.SetInt("GamesForSword", 0);
        }
    }
}
