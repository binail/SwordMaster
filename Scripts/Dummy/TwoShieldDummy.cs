using UnityEngine;

public class TwoShieldDummy : MonoBehaviour
{
    [SerializeField] private GameObject _leftShield;
    [SerializeField] private GameObject _rightShield;
    [SerializeField] private int _health;

    private DummyLogic _dummyLogic;
    private bool _isLeftShieldExist;
    private bool _isRightShieldExist;

    private void Start()
    {
        _dummyLogic = gameObject.GetComponent<DummyLogic>();

        _isLeftShieldExist = false;
        _isRightShieldExist = false;
        _dummyLogic.SetDummyHealth(_health);
        _dummyLogic.SetDummyArmor(false, _isLeftShieldExist, _isRightShieldExist);
    }

    private void Update()
    {
        _dummyLogic.SetDummyArmor(false, _isLeftShieldExist, _isRightShieldExist);
    }

    public void BreakLeftSheild()
    {
        var levelProcess = GameObject.Find("LevelProcess");
        levelProcess.GetComponent<LevelProcess>().ResetTime();
        Destroy(_leftShield);
        _isLeftShieldExist = true;
        _dummyLogic.SetDummyArmor(false, _isLeftShieldExist, _isRightShieldExist);
    }

    public void BreakRightShield()
    {
        var levelProcess = GameObject.Find("LevelProcess");
        levelProcess.GetComponent<LevelProcess>().ResetTime();
        Destroy(_rightShield);
        _isRightShieldExist = true;
        _dummyLogic.SetDummyArmor(false, _isLeftShieldExist, _isRightShieldExist);
    }
}
