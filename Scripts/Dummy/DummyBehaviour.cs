using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class DummyBehaviour : MonoBehaviour
{
    [SerializeField] private LevelProcess levelProcess;
    [SerializeField] private DummyLogic[] prefab;

    [Header("Sounds")]
    [SerializeField] private AudioClip failedAttackHelmet;
    [SerializeField] private AudioClip failedAttackShield;
    [SerializeField] private AudioClip successfulAttack;
    [SerializeField] private AudioClip breakWoodShield;
    [SerializeField] private AudioClip penetrationSound;

    private float[] armorChance;
    private float[] dummyChance;
    private readonly List<DummyLogic> _alive = new();
    private StumpCreator _stumpCreator;
    private bool ignoreWoodShield;

    private void Start()
    {
        _stumpCreator = gameObject.GetComponent<StumpCreator>();
        float[] _firstArmorChance = new float[] { 0f, 0f, 1f };
        float[] _firstDummyChance = new float[] { 1f, 0f, 0f, 0f };
        for (int i = 1; i < 4; i++)
            CreateNewDummy(i, _firstArmorChance, _firstDummyChance);
    }

    private void CreateNewDummy(int _positionNumber, float[] _armorChance, float[] _dummyChance)
    {
        var _dummyNumber = ChooseDummy(_dummyChance);

        DummyLogic _newDummy = Instantiate(prefab[_dummyNumber]);

        _newDummy.DestroyArmor(_armorChance);

        _newDummy.SetAnimatorPosition(_positionNumber);

        _alive.Add(_newDummy);
    }

    public int Kill(int dummyNumber, Vector2 direction, int penetration, int damage)
    {
        DummyLogic target = _alive[dummyNumber];
        bool isPenetration = false;
        bool isBreakWoodShield = false;


        if ((direction == Vector2.up && target.HitTop() == false) ||
            (direction == Vector2.left && target.HitLeft() == false) ||
            (direction == Vector2.right && target.HitRight() == false))
        {
            if (target.GetComponent<TwoShieldDummy>() == null || direction == Vector2.up || ignoreWoodShield == false)
            {
                if (target.GetComponent<TwoShieldDummy>() != null && direction != Vector2.up)
                {
                    SoundManager.instance.PlaySound(breakWoodShield);
                    //Vibrator.Vibrate(80);
                    return penetration;
                }
                else if (penetration <= 0)
                {
                    if (direction == Vector2.up) SoundManager.instance.PlaySound(failedAttackHelmet);
                    else SoundManager.instance.PlaySound(failedAttackShield);
                    //Vibrator.Vibrate(100);
                    return penetration;
                }
                else
                {
                    penetration--;
                    isPenetration = true;
                    SoundManager.instance.PlaySound(penetrationSound);
                    //Vibrator.Vibrate(80);
                }    
            }
        }
        if (isPenetration == false && isBreakWoodShield == false)
        {
            //Vibrator.Vibrate(30);
            SoundManager.instance.PlaySound(successfulAttack);
        }

        if (target.Kill(damage) <= 0)
        {
            _stumpCreator.CreateDummyStump(direction, target.tag);
            
            _alive.Remove(target);
            levelProcess.DummyDead();
            int _counter = 3 - _alive.Count;
            if (levelProcess.GetCountOfDummy() == 1) _counter--;

            foreach (var _dummy in _alive)
            {
                _dummy.SetAnimatorPosition(_counter);
                _counter++;
            }
            
            if(levelProcess.GetCountOfDummy() >= 3)
            CreateNewDummy(3, armorChance, dummyChance);
        }

        levelProcess.ResetTime();

        return penetration;
    }

    public bool PossibleToKillDummy(Vector2 _direction)
    {
        DummyLogic target = _alive[0];

        if ((_direction == Vector2.down && target.IsTopUnprotected() == false) ||
            (_direction == Vector2.right && target.IsLeftUnprotected(ignoreWoodShield) == false) ||
            (_direction == Vector2.left && target.IsRightUnprotected(ignoreWoodShield) == false))
            return false;

        return true;
    }

    public bool PossibleToKillDummy(Vector2 _direction, int _numberOfPenetrations)
    {
        DummyLogic target = _alive[0];

        if (_numberOfPenetrations > 0 && (_direction == Vector2.up || target.gameObject.GetComponent<TwoShieldDummy>() == null))
            return true;

        if ((_direction == Vector2.down && target.IsTopUnprotected() == false) ||
            (_direction == Vector2.right && target.IsLeftUnprotected(ignoreWoodShield) == false) ||
            (_direction == Vector2.left && target.IsRightUnprotected(ignoreWoodShield) == false))
            return false;

        return true;
    }

    public void SetChances(float[] _armorChance, float[] _dummyChance)
    {   
        armorChance = _armorChance;
        dummyChance = _dummyChance;
    }


    public int GetDummyHealth (int _dummyNumber)
    {
        return _alive[0].GetDummyHealth();
    }

    private int ChooseDummy(float[] _dumyChance)
    {
        float _sumChance = 0;
        float _value;

        for (int i = 0; i < _dumyChance.Length; i++)
            _sumChance += _dumyChance[i];

        _value = Random.Range(0, _sumChance);

        _sumChance = 0;

        for (int i = 1; i < _dumyChance.Length + 1; i++)
        {
            _sumChance += _dumyChance[i - 1];

            if (_value < _sumChance)
            {
                return i-1;
            }
        }

        return 0;
    }

    public DummyLogic GetDummy(int dummyNumber)
    {
        if (dummyNumber > _alive.Count - 1) return null;

        DummyLogic target = _alive[dummyNumber];

        return target;
    }

    public string GetDummyTag(int dummyNumber)
    {
        if (dummyNumber > _alive.Count - 1) return null;

        DummyLogic target = _alive[dummyNumber];

        return target.GetDummyTag();
    }

    public void SetIgnoreWoodShield(bool _ignoreWoodShield)
    {
        ignoreWoodShield = _ignoreWoodShield;
    }
}
