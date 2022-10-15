using UnityEngine;

public class SwordAbilities : MonoBehaviour
{
    [SerializeField] private SwordInformation information;

    private bool _fasterAttack;
    private bool _ignoreWoodShield;
    private float _multiplyTime;
    private float _moneyIncrease;
    private int _numberOfPenetrations;
    private int _damage;

    private void Start()
    {
        IdentifyAbilities();

        var _swordLogic = GetComponentInParent<SwordLogic>();

        if (_fasterAttack)   GetComponentInParent<Animator>().SetFloat("AttackSpeed", 1.5f);

        if (_ignoreWoodShield) _swordLogic.SetIgnoreWoodShield(_ignoreWoodShield);

        if (_multiplyTime != 0) _swordLogic.MultiplyTime(_multiplyTime);

        if (_moneyIncrease != 0) _swordLogic.SetMoneyIncrease(_moneyIncrease);

        _swordLogic.SetNumberOfPenetrations(_numberOfPenetrations);

        _swordLogic.SetSwordDamage(_damage);
    }

    private void IdentifyAbilities()
    {
        _fasterAttack = information.fasterAttack;
        _ignoreWoodShield = information.ignoreWoodShield;
        _multiplyTime = information.multiplyTime;
        _moneyIncrease = information.moneyIncrease;
        _numberOfPenetrations = information.numberOfPenetrations;
        _damage = information.damage;
    }
}
