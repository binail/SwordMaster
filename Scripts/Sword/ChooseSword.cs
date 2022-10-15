using UnityEngine;

public class ChooseSword : MonoBehaviour
{
    [SerializeField] GameObject[] swords;

    private int chosenSword;

    private void Start()
    {
        chosenSword = PlayerPrefs.GetInt("ChosenSword");
        
        swords[chosenSword].SetActive(true);
    }
}
