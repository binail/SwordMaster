using UnityEngine;

public class ShieldChangeDummy : MonoBehaviour
{
    [SerializeField] private Transform _rightShieldPosition;
    [SerializeField] private Transform _leftShieldPosition;
    [SerializeField] private GameObject _shield;

    [SerializeField] private float _speed;
    [SerializeField] private int _health;

    [Header("Body")]
    [SerializeField] private GameObject[] _body;

    private int _currentHealth;
    private bool _isShieldOnLeftPosition;
    private bool _isShieldOnRightPosition;
    private DummyLogic _dummyLogic;

    private void Start()
    {
        _body[0].SetActive(true);
        _body[1].SetActive(false);
        _body[2].SetActive(false);

        _dummyLogic = gameObject.GetComponent<DummyLogic>();

        _currentHealth = _health;
        _dummyLogic.SetDummyHealth(_health);

        if(Random.Range(0, 2) == 0)
        {
            _isShieldOnLeftPosition = true;
            _isShieldOnRightPosition = false;
        }
        else
        {
            _isShieldOnRightPosition = true;
            _isShieldOnLeftPosition = false;
        }

        _dummyLogic.SetDummyArmor(false, _isShieldOnLeftPosition, _isShieldOnRightPosition);
    }

    private void Update()
    {
        var step = _speed * Time.deltaTime;

        if (_isShieldOnLeftPosition)
        {
            _shield.transform.localPosition = Vector3.MoveTowards(_shield.transform.localPosition, _leftShieldPosition.localPosition, step);
        }
        else if (_isShieldOnRightPosition)
        {
            _shield.transform.localPosition = Vector3.MoveTowards(_shield.transform.localPosition, _rightShieldPosition.localPosition, step);
        }

        if (_currentHealth != _dummyLogic.GetDummyHealth())
        {
            ChangeShieldLocation();
            _dummyLogic.SetDummyArmor(false, _isShieldOnLeftPosition, _isShieldOnRightPosition);
        }

        _currentHealth = _dummyLogic.GetDummyHealth();

        switch(_currentHealth)
        {
            case 2:
                _body[0].SetActive(false);
                _body[1].SetActive(true);
                _body[2].SetActive(false);
                break;
            case 1:
                _body[0].SetActive(false);
                _body[1].SetActive(false);
                _body[2].SetActive(true);
                break;
            default:
                break;
        }
    }

    private void ChangeShieldLocation()
    {
        _isShieldOnRightPosition = !_isShieldOnRightPosition;
        _isShieldOnLeftPosition = !_isShieldOnLeftPosition;
    }
}
