using UnityEngine;

public class WithoutHelmetDummy : MonoBehaviour
{
    [SerializeField] private int _health;
    [Header("Body")]
    [SerializeField] private GameObject[] _body;

    private int _currentHealth;
    private DummyLogic _dummyLogic;

    private void Start()
    {
        _dummyLogic = gameObject.GetComponent<DummyLogic>();

        _currentHealth = _health;
        _dummyLogic.SetDummyHealth(_health);
        _dummyLogic.SetDummyArmor(true, false, false);
    }

    private void Update()
    {
        _currentHealth = _dummyLogic.GetDummyHealth();

        switch (_currentHealth)
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
}
