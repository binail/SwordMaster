using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyForStore : MonoBehaviour
{
    [SerializeField] private UIVFXFromTo fromTo;

    private int countOfMoney;

    private void Start()
    {
        countOfMoney = PlayerPrefs.GetInt("Money");
        GetComponentInChildren<Text>().text = countOfMoney.ToString();
    }

    public void BuySword(int _price)
    {
        StartCoroutine(Decreasing‹oney(_price));
    }

    private IEnumerator Decreasing‹oney(int _price)
    {
        float _timeFromBegin = 0f;
        float _timeForDeacrease = 1f;
        Text _UIMoney = GetComponentInChildren<Text>();
        int _startMoney = int.Parse(_UIMoney.text);
        
        fromTo.FromTo((15), _timeForDeacrease);

        _UIMoney.text = countOfMoney.ToString();

        while (_timeFromBegin < _timeForDeacrease)
        {
            yield return new WaitForEndOfFrame();
            _timeFromBegin += Time.deltaTime;

            if (_timeFromBegin > _timeForDeacrease) _timeFromBegin = _timeForDeacrease;
            _UIMoney.text = (_startMoney - (int)(_price * _timeFromBegin)).ToString();
        }

        countOfMoney = PlayerPrefs.GetInt("Money");
        _UIMoney.text = countOfMoney.ToString();
    }
}
