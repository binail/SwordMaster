using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DisallowMultipleComponent]
public class LevelProcess : MonoBehaviour
{
    [Header("Game Lose")]
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject skipLevelPanel;

    [Header("Game Win")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject congratulations;
    [SerializeField] private GameObject winGameParticles;
    [SerializeField] private Transform particlesPoint;

    [Header("Other")]
    [SerializeField] private Animator swordAnimator;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private GameObject remainingDummies;
    [SerializeField] private GameObject storeButton;
    [SerializeField] private GameObject soundButton;
    [SerializeField] private Image timeLineImage;
    
    private float timeForDummy;
    private float timeMultiplier = 1;
    private int countOfDummy;
    private float leftTime;

    private bool isGameStart = false;
    private bool gameOver = false;
    private bool timeOver = false;

    private const string gameOverCounter = "GameOverCounter";

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
    }

    private void Update()
    {
        if (isGameStart == false) return;

        if (timeOver) return;

        remainingDummies.GetComponent<Text>().text = countOfDummy.ToString();

        timeLineImage.fillAmount = leftTime / (timeForDummy * timeMultiplier);

        leftTime -= Time.deltaTime;
        
        if (leftTime <= 0)
        {
            GameOver();
        }

        if (countOfDummy <= 0)
        {
            GameWin();
        }
    }

    public void ResetTime()
    {
        leftTime = timeForDummy * timeMultiplier;
    }

    private void GameOver()
    {
        Vibrator.Vibrate(360);
        StartCoroutine(EnableCanvas());

        PlayerPrefs.SetInt("LevelFailedCount", PlayerPrefs.GetInt("LevelFailedCount") + 1);
        var numberOfDefeats = PlayerPrefs.GetInt(gameOverCounter);

        SoundManager.instance.PlaySound(loseSound);

        swordAnimator.enabled = false;
        gameOver = true;
        timeOver = true;

        if (numberOfDefeats >= 2)
        {
            PlayerPrefs.SetInt(gameOverCounter, 0);
            skipLevelPanel.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt(gameOverCounter, numberOfDefeats + 1);
        }
        restartButton.SetActive(true);
        backgroundImage.SetActive(true);
    }

    private void GameWin()
    {
        Vibrator.Vibrate(444);
        StartCoroutine(EnableCanvas());

        PlayerPrefs.SetInt("LevelFailedCount", 0);
        PlayerPrefs.SetInt(gameOverCounter, 0);
        SoundManager.instance.PlaySound(winSound);

        gameOver = true;
        timeOver = true;

        Instantiate(winGameParticles, particlesPoint);
        nextLevelButton.SetActive(true);
        congratulations.SetActive(true);
        backgroundImage.SetActive(true);

        GetComponent<MoneyForGame>().EndOfLevel();

        GetComponent<LevelInformation>().NextLevel();
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void DummyDead()
    {
        countOfDummy--;
    }

    public int GetCountOfDummy()
    {
        return countOfDummy;
    }

    public void SetInfo(int _countOfDummy, float _timeForDummy)
    {
        countOfDummy = _countOfDummy;
        timeForDummy = _timeForDummy;
    }

    public void StartGame()
    {
        leftTime = timeForDummy * timeMultiplier;
        isGameStart = true;
        remainingDummies.SetActive(true);
        storeButton.SetActive(false);
        soundButton.SetActive(false);
        remainingDummies.GetComponent<Text>().text += countOfDummy;

        int _gameWithSword = PlayerPrefs.GetInt("GameWithSword");
        int _chosenSword = PlayerPrefs.GetInt("ChosenSword");

        if (_gameWithSword > 0)
        {
            _gameWithSword--;

            if (_gameWithSword == 0)
            {
                if (PlayerPrefs.GetInt("levelNumber") > 52)
                {
                    PlayerPrefs.SetInt("WaitBeforeNewAd", 5);
                    Debug.Log("Set Waiting");
                }

                PlayerPrefs.SetInt("IsPurchased" + PlayerPrefs.GetInt("ReceivedSword"), 0);
                if (PlayerPrefs.GetInt("ReceivedSword") == _chosenSword)
                {
                    PlayerPrefs.SetInt("ChosenSword", 0);
                }
            }

            PlayerPrefs.SetInt("GameWithSword", _gameWithSword);
        }
    }

    public void SetTimeMultiplier(float _timeMultiplier)
    {
        timeMultiplier = _timeMultiplier;
    }

    private IEnumerator EnableCanvas()
    {
        yield return new WaitForSeconds(2);

        canvas.SetActive(true);
    }
}
