using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]

public class Tutorial : MonoBehaviour
{
    [SerializeField] private DummyBehaviour _behaviour;
    [SerializeField] private iTween.EaseType _path;
    [SerializeField] private float waiting;

    [Header("Cursor Properties")]
    [SerializeField] private GameObject _cursorPartice;
    [SerializeField] private float _timeForSwipe;
    [SerializeField] private float _timeBetweenSwipe;

    [Header("Points For Cursor")]
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _middlePoint;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rigtPoint;

    private readonly string _default = "DefaultDummyTutorial";
    private bool _defaultTutorial;

    private readonly string _shieldChange = "ShieldChangeDummyTutorial";
    private bool _shieldChangeTutorial;

    private readonly string _withoutHelmet = "WithoutHelmetDummyTutorial";
    private bool _withoutHelmetTutorial;

    private readonly string _twoShield = "TwoShieldDummyTutorial";
    private bool _twoShieldTutorial;

    private readonly string _tutorial = "Tutorial";

    private bool _isTutorialActive;


    private void Start()
    {
        _defaultTutorial = CheckNecessityOfTutorial(_default);
        _shieldChangeTutorial = CheckNecessityOfTutorial(_shieldChange);
        _withoutHelmetTutorial = CheckNecessityOfTutorial(_withoutHelmet);
        _twoShieldTutorial = CheckNecessityOfTutorial(_twoShield);

        
        if (_defaultTutorial == false && _shieldChangeTutorial == false &&
            _withoutHelmetTutorial == false && _twoShieldTutorial == false)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (waiting > 0)
        {
            waiting -= Time.deltaTime;
            return;
        }



        string currentName = _behaviour.GetDummyTag(0);

        if (currentName + _tutorial == _default && _defaultTutorial)
        {
            _isTutorialActive = true;
            SuggestDirection();
            ConfirmTutorial(_default);
            _defaultTutorial = false;
            return;
        }
        else if (currentName + _tutorial == _shieldChange && _shieldChangeTutorial)
        {
            _isTutorialActive = true;
            SuggestDirection();
            ConfirmTutorial(_shieldChange);
            _shieldChangeTutorial = false;
            return;
        }
        else if (currentName + _tutorial == _withoutHelmet && _withoutHelmetTutorial)
        {
            _isTutorialActive = true;
            SuggestDirection();
            ConfirmTutorial(_withoutHelmet);
            _withoutHelmetTutorial = false;
            return;
        }
        else if (currentName + _tutorial == _twoShield && _twoShieldTutorial)
        {
            _isTutorialActive = true;
            StartCoroutine(FromToCo(_leftPoint, _rigtPoint));
            ConfirmTutorial(_twoShield);
            _twoShieldTutorial = false;
            return;
        }
    }

    private void ConfirmTutorial(string key)
    {
        PlayerPrefs.SetInt(key, 1);
    }

    private void SuggestDirection()
    {
        if (_behaviour.PossibleToKillDummy(Vector2.right))
        {
            StartCoroutine(FromToCo(_leftPoint, _rigtPoint));
            return;
        }

        if (_behaviour.PossibleToKillDummy(Vector2.left))
        {
            StartCoroutine(FromToCo(_rigtPoint, _leftPoint));
            return;
        }

        if (_behaviour.PossibleToKillDummy(Vector2.down))
        {
            StartCoroutine(FromToCo(_topPoint, _middlePoint));
            return;
        }
    }
    private bool CheckNecessityOfTutorial(string key)
    {
        if (PlayerPrefs.GetInt(key) == 0) return true;
        else return false;
    }

    IEnumerator FromToCo(Transform startPoint, Transform endPoint)
    {
        while (_isTutorialActive)
        {
            var _vfx = Instantiate(_cursorPartice, startPoint.position, Quaternion.identity) as GameObject;
            _vfx.transform.SetParent(startPoint.transform);
            iTween.MoveTo(_vfx, iTween.Hash("position", endPoint.transform.position, "easetype", _path, "ignoretimescale", true, "time", _timeForSwipe));
            Destroy(_vfx, _timeForSwipe + 1);
            yield return new WaitForSeconds(_timeBetweenSwipe);
        }
    }

    private void OnEnable()
    {
        SwipeDetection.SwipeEvent += OnSwipe;
    }

    private void OnDisable()
    {
        SwipeDetection.SwipeEvent -= OnSwipe;
    }

    private void OnSwipe(Vector2 _direction)
    {
        if (_isTutorialActive == true) _isTutorialActive = false;
    }

}
