using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class DummyLogic : MonoBehaviour
{
    private const string _positionParameter = "Position";

    [SerializeField] private GameObject _helmet;
    private bool _isHelmetExists = true;
    [SerializeField] private GameObject _leftShield;
    private bool _isLeftShieldExists = true;
    [SerializeField] private GameObject _rightShield;
    private bool _isRightShieldExists = true;

    [Header("Effects")]
    [SerializeField] private Material _deathParticleColor;
    [SerializeField] private Transform _particlesPoint;
    [SerializeField] private GameObject _damageParticles;

    private int _health;
    private Animator _animator;
    private string _tag;

    private void Awake() 
    {
        _animator = GetComponent<Animator>();
        _tag = gameObject.tag;
    }

    public void SetAnimatorPosition(int position) 
    {
        _animator.SetInteger(_positionParameter, position);
    }

    public bool HitTop()
    {
        if (GetComponent<DefaultDummy>() != null)
        {
            if (_helmet == null) _isHelmetExists = true;
            else _isHelmetExists = false;
        }
        return _isHelmetExists;
    }

    public bool IsTopUnprotected()
    {
        if (GetComponent<DefaultDummy>() != null)
        {
            if (_helmet == null) _isHelmetExists = true;
            else _isHelmetExists = false;
        }
        return _isHelmetExists;
    }

    public bool HitRight()
    {
        if (GetComponent<DefaultDummy>() != null)
        {
            if (_rightShield == null) _isRightShieldExists = true;
            else _isRightShieldExists = false;
        }

        var isExist = _isRightShieldExists;

        if ((gameObject.GetComponent<TwoShieldDummy>() != null) && _isRightShieldExists == false)
            gameObject.GetComponent<TwoShieldDummy>().BreakRightShield();

        return isExist;
    }

    public bool IsRightUnprotected(bool _ignoreWoodShield)
    {
        if (_ignoreWoodShield && GetComponent<TwoShieldDummy>() != null) return true;

        if (GetComponent<DefaultDummy>() != null)
        {
            if (_rightShield == null) _isRightShieldExists = true;
            else _isRightShieldExists = false;
        }

        return _isRightShieldExists;
    }

    public bool HitLeft()
    {
        if (GetComponent<DefaultDummy>() != null)
        {
            if (_leftShield == null) _isLeftShieldExists = true;
            else _isLeftShieldExists = false;
        }

        var isExist = _isLeftShieldExists;

        if ((gameObject.GetComponent<TwoShieldDummy>() != null) && _isLeftShieldExists == false)
            gameObject.GetComponent<TwoShieldDummy>().BreakLeftSheild();

        return isExist;
    }

    public bool IsLeftUnprotected(bool _ignoreWoodShield)
    {
        if (_ignoreWoodShield && GetComponent<TwoShieldDummy>() != null) return true;

        if (GetComponent<DefaultDummy>() != null)
        {
            if (_leftShield == null) _isLeftShieldExists = true;
            else _isLeftShieldExists = false;
        }

        return _isLeftShieldExists;
    }

    public int Kill(int damage)
    {
        _health -= damage;
        var particle = Instantiate(_damageParticles, _particlesPoint.position, _particlesPoint.rotation);
        particle.GetComponent<ParticleSystem>().startColor = _deathParticleColor.color;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }


        return _health;
    }

    public string GetDummyTag()
    {
        return _tag;
    }

    public void DestroyArmor(float[] _armorCount)
    {
        if (gameObject.GetComponent<ShieldChangeDummy>() != null) return;
        if (gameObject.GetComponent<TwoShieldDummy>() != null) return;
        if (gameObject.GetComponent<WithoutHelmetDummy>() != null) return;

        float _sumChance = 0;
        float _value;

        for (int i = 0; i < _armorCount.Length; i++)
            _sumChance += _armorCount[i];

        _value = Random.Range(0, _sumChance);

        _sumChance = 0;

        for (int i = 1; i < _armorCount.Length+1; i++)
        {
            _sumChance += _armorCount[i-1];

            if (_value < _sumChance)
            {
                ApplyDestruction(i);

                return;
            }
        }

        ApplyDestruction(_armorCount.Length);
    }

    private void ApplyDestruction(int needToDestroy)
    {
        List<GameObject> available = new List<GameObject> { _helmet, _leftShield, _rightShield};

        for (int i = 0; i < needToDestroy; i++)
        {
            int random = Random.Range(0, available.Count);
            GameObject target = available[random];

            available.Remove(target);
            Destroy(target);
        }
    }

    public void SetDummyHealth(int health)
    {
        _health = health;
    }

    public int GetDummyHealth()
    {
        return _health;
    }

    public void SetDummyArmor(bool helmet, bool leftShield, bool rightShield)
    {
        _isHelmetExists = helmet;
        _isLeftShieldExists = leftShield;
        _isRightShieldExists = rightShield;
    }
}