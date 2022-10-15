using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class StoreSelectors : MonoBehaviour
{
    [SerializeField] SwordInformation[] swordParameters;
    [SerializeField] GameObject[] swords;
    [SerializeField] Text swordName;
    [SerializeField] Text swordAbilities;
    [SerializeField] MoneyForStore moneyForStore;

    [Header("Buttons")]
    [SerializeField] GameObject rightButton;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject selectSword;

    [Header("Buy Button")]
    [SerializeField] GameObject buySword;
    [SerializeField] AudioClip scrollsSound;
    [SerializeField] Color cantBuyColor;
    [SerializeField] Color canBuyColor;

    private int[] isPurchased;

    private static int chosenSword;

    private bool isFirstGame;

    void Start()
    {
        isPurchased = new int[swords.Length];

        for (int i = 0; i < swords.Length; i++)
        {
            isPurchased[i] = PlayerPrefs.GetInt("IsPurchased" + i);
            if (isPurchased[i] != 0)
            {
                isFirstGame = false;
            }
            else isFirstGame = true;
        }

        if(isFirstGame)
        {
            isPurchased[0] = 1;
            PlayerPrefs.SetInt("IsPurchased" + 0, isPurchased[0]);
        }

        chosenSword = PlayerPrefs.GetInt("ChosenSword");

        swords[chosenSword].SetActive(true);

        ButtonAvalable();
        ChangeSwordData();
        SelectSword();
    }

    public void OnRightButton()
    {
        chosenSword++;

        swords[chosenSword - 1].SetActive(false);
        swords[chosenSword].SetActive(true);

        ButtonAvalable();
        ChangeSwordData();
        SelectSword();
    }

    public void OnLeftButton()
    {
        chosenSword--;

        swords[chosenSword + 1].SetActive(false);
        swords[chosenSword].SetActive(true);

        ButtonAvalable();
        ChangeSwordData();
        SelectSword();
    }

    private void ButtonAvalable()
    {
        if (chosenSword <= 0)
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }

        if (chosenSword >= swords.Length-1)
        {
            rightButton.SetActive(false);
        }
        else
        {
            rightButton.SetActive(true);
        }
    }

    private void ChangeSwordData()
    {
        swordName.text = swordParameters[chosenSword].swordName;
        swordAbilities.text = swordParameters[chosenSword].swordAbilities;
    }

    private void SelectSword()
    {
        if (isPurchased[chosenSword] == 1 && chosenSword == PlayerPrefs.GetInt("ChosenSword"))
        {
            selectSword.SetActive(true);
            buySword.SetActive(false);
            selectSword.GetComponent<Image>().color = new Color(195 / 255.0f, 195 / 255.0f, 195 / 255.0f);
            selectSword.GetComponentInChildren<Text>().text = "Chosen";
        }
        else if (isPurchased[chosenSword] == 1 && chosenSword != PlayerPrefs.GetInt("ChosenSword"))
        {
            selectSword.SetActive(true);
            buySword.SetActive(false);
            selectSword.GetComponent<Image>().color = new Color(1, 1, 1);
            selectSword.GetComponentInChildren<Text>().text = "Select";
        }

        if (isPurchased[chosenSword] == 0)
        {
            buySword.SetActive(true);
            if (PlayerPrefs.GetInt("Money") < swordParameters[chosenSword].prise)
            {
                buySword.GetComponent<Image>().color = cantBuyColor;
            }
            else buySword.GetComponent<Image>().color = canBuyColor;
            selectSword.SetActive(false);
            buySword.GetComponentInChildren<Text>().text = swordParameters[chosenSword].prise.ToString();
        }
    }

    public void ChoseSword()
    {
        PlayerPrefs.SetInt("ChosenSword", chosenSword);

        SelectSword();
    }

    public void BuySword()
    {
        if (PlayerPrefs.GetInt("Money") < swordParameters[chosenSword].prise)
            return;

        SoundManager.instance.PlaySound(scrollsSound);
        isPurchased[chosenSword] = 1;

        moneyForStore.BuySword(swordParameters[chosenSword].prise);
        PlayerPrefs.SetInt("IsPurchased" + chosenSword, isPurchased[0]);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - swordParameters[chosenSword].prise);
        PlayerPrefs.SetInt("ChosenSword", chosenSword);

        SelectSword();
    }
}
