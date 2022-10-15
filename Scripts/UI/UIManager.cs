using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [SerializeField] AudioSource source;
    [SerializeField] GameObject levelEnd;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject swordForAd;
    [SerializeField] GameObject adWindow;
    [SerializeField] GameObject skipLevelAd;

    public void Replay()
    {
        SoundManager.instance.PlaySound(clickSound);
        StartCoroutine(WaitBeforeReload());
    }

    public void DisableCanvas()
    {
        SoundManager.instance.PlaySound(clickSound);
        gameObject.SetActive(false);
    }

    public void GoToStore()
    {
        SoundManager.instance.PlaySound(clickSound);
        StartCoroutine(WaitBeforeStore());
    }

    public void ResetAllInfo()
    {
        SoundManager.instance.PlaySound(clickSound);
        PlayerPrefs.DeleteAll();
    }

    public void AddMoney()
    {
        SoundManager.instance.PlaySound(clickSound);

        int _countOfMoney = PlayerPrefs.GetInt("Money");
        _countOfMoney += 250;
        PlayerPrefs.SetInt("Money", _countOfMoney);
    }

    public void NextLevelForTest()
    {
        SoundManager.instance.PlaySound(clickSound);

        int _currentLevel = PlayerPrefs.GetInt("levelNumber");
        _currentLevel++;
        PlayerPrefs.SetInt("levelNumber", _currentLevel);
    }
    
    public void PrevoiusLevel()
    {
        SoundManager.instance.PlaySound(clickSound);

        int _currentLevel = PlayerPrefs.GetInt("levelNumber");
        _currentLevel--;
        PlayerPrefs.SetInt("levelNumber", _currentLevel);
    }

    public void Next()
    {
        SoundManager.instance.PlaySound(clickSound);

        int _waitCounter = PlayerPrefs.GetInt("WaitBeforeNewAd");
        if (_waitCounter > 0)
        {
            Debug.Log("Minus waiting");
            _waitCounter--;
            PlayerPrefs.SetInt("WaitBeforeNewAd", _waitCounter);
            Replay();
            return;
        }

        int _levelNumber = PlayerPrefs.GetInt("levelNumber");
        int _currentAdSword;

        switch (_levelNumber)
        {
            case < 11:
                _currentAdSword = 2;
                break;

            case < 25:
                _currentAdSword = 4;
                break;

            case < 35:
                _currentAdSword = 6;
                break;

            case < 45:
                _currentAdSword = 9;
                break;

            case >= 45:
                _currentAdSword = 14;
                break;
        }

        if (PlayerPrefs.GetInt("GameWithSword") == 0 && PlayerPrefs.GetInt("IsPurchased" + _currentAdSword) == 0 && PlayerPrefs.GetInt("levelNumber") >= 3)
        {
            SoundManager.instance.PlaySound(clickSound);
            nextButton.SetActive(false);
            levelEnd.SetActive(false);
            swordForAd.SetActive(true);
        }
        else
        {
            Replay();
        }
    }

    public void CheckAdForSword()
    {
        PlayerPrefs.SetInt("CheckAdForSwordCounter", PlayerPrefs.GetInt("CheckAdForSwordCounter") + 1);

        SoundManager.instance.PlaySound(clickSound);

        int _levelNumber = PlayerPrefs.GetInt("levelNumber");
        int _currentAdSword;

        switch (_levelNumber)
        {
            case < 11:
                _currentAdSword = 2;
                break;

            case < 25:
                _currentAdSword = 4;
                break;

            case < 35:
                _currentAdSword = 6;
                break;

            case < 45:
                _currentAdSword = 9;
                break;

            case >= 45:
                _currentAdSword = 14;
                break;
        }

        swordForAd.SetActive(false);
        adWindow.SetActive(true);
        PlayerPrefs.SetInt("GameWithSword", 4);
        PlayerPrefs.SetInt("IsPurchased" + _currentAdSword, 1);
        //PlayerPrefs.SetInt("ActiveSword", _currentAdSword);
        PlayerPrefs.SetInt("ReceivedSword", _currentAdSword);
        PlayerPrefs.SetInt("ChosenSword", _currentAdSword);
    }

    public void CheckAdForSkipLevel()
    {
        PlayerPrefs.SetInt("CheckAdForSwordCounter", PlayerPrefs.GetInt("CheckAdForSkipLevel") + 1);

        SoundManager.instance.PlaySound(clickSound);
        skipLevelAd.SetActive(false);
        adWindow.SetActive(true);
        PlayerPrefs.SetInt("levelNumber", PlayerPrefs.GetInt("levelNumber") + 1);
    }

    private IEnumerator WaitBeforeReload()
    {
        yield return new WaitForSeconds(0.1f);

        Transitions.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitBeforeStore()
    {
        yield return new WaitForSeconds(0.1f);

        Transitions.LoadScene(2);
    }
}
