using UnityEngine;

[CreateAssetMenu(fileName ="New Sword", menuName ="Sword")]
public class SwordInformation : ScriptableObject
{
    public string swordName;
    public string swordAbilities;

    public int number;
    public int prise;

    [Header ("Abilities")]
    public bool fasterAttack;
    public bool ignoreWoodShield;
    public float multiplyTime;
    public float moneyIncrease;
    public int numberOfPenetrations;
    public int damage;
}
