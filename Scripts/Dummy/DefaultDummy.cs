using UnityEngine;

public class DefaultDummy : MonoBehaviour
{

    [SerializeField] private int _health;

    private DummyLogic _dummyLogic;
    private bool _isHelmetExists = true;
    private bool _isLeftShieldExists = true;
    private bool _isRightShieldExists = true;

    void Start()
    {
        _dummyLogic = gameObject.GetComponent<DummyLogic>();

        _dummyLogic.SetDummyHealth(_health);
    }

    public void SetArmor (GameObject _helmet, GameObject _leftShield, GameObject _rightShield)
    {
        if (_helmet == null) _isHelmetExists = false;
        else _isHelmetExists = true;

        if (_leftShield == null) _isLeftShieldExists = false;
        else _isLeftShieldExists = true;

        if (_rightShield == null) _isRightShieldExists = false;
        else _isRightShieldExists = true;

        _dummyLogic.SetDummyArmor(_isHelmetExists, _isLeftShieldExists, _isRightShieldExists);
    }
}
