using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyForGame : MonoBehaviour
{
    [SerializeField] Text moneyText;
    [SerializeField] Text levelEndNotification;
    [SerializeField] UIVFXFromTo fromTo;
    [SerializeField] AudioClip scrollsCollect;

    private int countOfMoney;
    private string complexity;
    private float moneyIncrease = 1;

    private Dictionary<string, int> _moneyForLevel = new Dictionary<string, int>()
    {
        { "Easy", 5},
        { "Medium", 10},
        { "Hard", 25},
        { "Extreme", 50},
        { "Insane", 100},
    };

    private Dictionary<string, int> _countOfScrolls = new Dictionary<string, int>()
    {
        { "Easy", 1},
        { "Medium", 2},
        { "Hard", 5},
        { "Extreme", 10},
        { "Insane", 20},
    };

    private void Start()
    {
        countOfMoney = PlayerPrefs.GetInt("Money");
        moneyText.text = countOfMoney.ToString();
    }

    public void EndOfLevel()
    {
        StartCoroutine(AddMoney((int)(_moneyForLevel[complexity] * moneyIncrease)));
        SoundManager.instance.PlaySound(scrollsCollect);
    }

    public void SetComplexity(string _complexity)
    {
        complexity = _complexity;
    }

    public void SetMoneyIncrease(float _moneyIncrease)
    {
        moneyIncrease = _moneyIncrease;
    }

    private IEnumerator AddMoney (int _money)
    {
        levelEndNotification.text = "Congratulations!\n+" + _money.ToString(); 
        float _timeFromBegin = 0f;
        float _timeForAdd = 0.5f;
        int _currentMoney = countOfMoney + _money;

        fromTo.FromTo((int)(_countOfScrolls[complexity] * moneyIncrease), _timeForAdd);

        moneyText.text = countOfMoney.ToString();
        PlayerPrefs.SetInt("Money", _currentMoney);
        yield return new WaitForSeconds(1);

        while (_timeFromBegin < _timeForAdd)
        {
            yield return new WaitForEndOfFrame();
            _timeFromBegin += Time.deltaTime;

            if (_timeFromBegin > _timeForAdd) _timeFromBegin = _timeForAdd;
            moneyText.text = (countOfMoney + (int)(_money * _timeFromBegin)).ToString();
        }

        countOfMoney = PlayerPrefs.GetInt("Money");
        moneyText.text = countOfMoney.ToString();
    }
}
