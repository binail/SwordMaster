using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class LevelInformation : MonoBehaviour
{
    [SerializeField] private Text levelNumber;
    [SerializeField] private ConfigDataProcessor dataProcessor;
    [SerializeField] private DummyBehaviour dummyBehaviour;
    [SerializeField] private Text remainingDummies;

    private InformationConverter informationConverter = new InformationConverter();

    private const string tableId = "1eH5t0jiPOwL7Cnu7Z5V7clN-u9Kw4WbzYTwByPOjYRU";

    private int currentLevel;
    private bool isFirstLevel;

    private LevelData[] data;
    private LevelData firstLevelData;

    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("levelNumber");
        levelNumber.text += currentLevel+1;

        if (PlayerPrefs.GetInt("levelNumber") == 0) isFirstLevel = true;
        else isFirstLevel = false;

        if (isFirstLevel)
        {
            float[] _armorChance = new float[] { 0f, 0f, 1f };
            float[] _dummyChance = new float[] { 1f, 0f, 0f, 0f };
            string _complexity = "Easy";
            firstLevelData = new LevelData(0, 6, 20f, _complexity, _armorChance, _dummyChance);
        }
        else
        {
            string s = PlayerPrefs.GetString("levelInformation");
            data = informationConverter.ConvertData(s);
        }

        dataProcessor.DownloadTable(tableId);
        SetInformation();
    }

    public void NextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("levelNumber", currentLevel);
    }

    private void SetInformation()
    {
        if (isFirstLevel)
        {
            dummyBehaviour.SetChances(firstLevelData.ArmorChance, firstLevelData.DummyChance);
            GetComponent<LevelProcess>().SetInfo(firstLevelData.CountDummy, firstLevelData.TimePerDummy);
            GetComponent<MoneyForGame>().SetComplexity("Easy");
            remainingDummies.text = "Easy";
        }
        else
        {
            int i = currentLevel;

            if (i > data.Length - 1)
            {
                i = data.Length - (5 - (i % 5)); 
            }

            dummyBehaviour.SetChances(data[i].ArmorChance, data[i].DummyChance);
            GetComponent<LevelProcess>().SetInfo(data[i].CountDummy, data[i].TimePerDummy);
            GetComponent<MoneyForGame>().SetComplexity(data[i].Complexity);
            remainingDummies.text = data[i].Complexity;
        }
    }
}
