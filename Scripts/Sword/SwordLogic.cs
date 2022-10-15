using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]

public class SwordLogic : MonoBehaviour
{
    [SerializeField] private DummyBehaviour dummyBehaviour;
    [SerializeField] private LevelProcess levelProcess;
    [SerializeField] private MoneyForGame moneyForGame;

    private Vector2 atackDirection;

    private Animator swordAnimator;

    private Penetration penetration;

    private bool isGameStart = false;
    private bool ignoreWoodShield;
    private int numberOfPenetrations;
    private int swordDamage = 1;

    private void Start()
    {
        penetration = GetComponent<Penetration>();
        swordAnimator = GetComponent<Animator>();
    }

    private void OnEnable() 
    {
        StartCoroutine(SetSwipeAfterSecond());
    }

    private void OnDisable() 
    {
        SwipeDetection.SwipeEvent -= OnSwipe;
    }

    private void OnSwipe(Vector2 _direction)
    {
        atackDirection = _direction;

        if (_direction == Vector2.up) return;

        if (isGameStart == false)
        {
            levelProcess.StartGame();
            isGameStart = true;
        }

        if (levelProcess.IsGameOver() == true)
            return;

        if (_direction == Vector2.down)
        {
            swordAnimator.SetTrigger("UpperAttack");
        }
        if (_direction == Vector2.left)
        {
            swordAnimator.SetTrigger("RightAttack");
        }
        if (_direction == Vector2.right)
        {
            swordAnimator.SetTrigger("LeftAttack");
        }
    }

    private void KillDummyLeft()
    {
        int _counterOfPenetrations = numberOfPenetrations;
        numberOfPenetrations = dummyBehaviour.Kill(0, Vector2.left, numberOfPenetrations, swordDamage);
        if (_counterOfPenetrations != numberOfPenetrations) penetration.IllustratePenetration(Vector2.left);
    }

    private void KillDummyRight()
    {
        int _counterOfPenetrations = numberOfPenetrations;
        numberOfPenetrations = dummyBehaviour.Kill(0, Vector2.right, numberOfPenetrations, swordDamage);
        if (_counterOfPenetrations != numberOfPenetrations) penetration.IllustratePenetration(Vector2.right);
    }

    private void KillDummyUpper()
    {
        int _counterOfPenetrations = numberOfPenetrations;
        numberOfPenetrations = dummyBehaviour.Kill(0, Vector2.up, numberOfPenetrations, swordDamage);
        if (_counterOfPenetrations != numberOfPenetrations) penetration.IllustratePenetration(Vector2.up);
    }

    public void lastHit()
    {
        if (levelProcess.GetCountOfDummy() == 1 && dummyBehaviour.PossibleToKillDummy(atackDirection, numberOfPenetrations) && dummyBehaviour.GetDummyHealth(0) == 1) 
            StartCoroutine(LastHitSlowMotion());
    }

    private IEnumerator LastHitSlowMotion()
    {
        yield return new WaitForSeconds(0.03f);

        Time.timeScale /= 6;

        yield return new WaitForSeconds(0.13f);

        Time.timeScale *= 6;
    }

    private IEnumerator SetSwipeAfterSecond()
    {
        yield return new WaitForSeconds(0.2f);

        SwipeDetection.SwipeEvent += OnSwipe;
    }

    public void MultiplyTime(float _timeMultiplier)
    {
        levelProcess.SetTimeMultiplier(_timeMultiplier);
    }

    public void SetMoneyIncrease(float _moneyIncrease)
    {
        moneyForGame.SetMoneyIncrease(_moneyIncrease);
    }

    public void SetNumberOfPenetrations(int _numberOfPenetrations)
    {
        numberOfPenetrations = _numberOfPenetrations;
    }

    public void SetIgnoreWoodShield(bool _ignoreWoodShield)
    {
        ignoreWoodShield = _ignoreWoodShield;
        dummyBehaviour.SetIgnoreWoodShield(_ignoreWoodShield);
    }

    public void SetSwordDamage(int _damage)
    {
        swordDamage = _damage;
    }
}
