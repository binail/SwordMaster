using System.Collections.Generic;
using UnityEngine;

public class InformationConverter
{
    private const int _levelNumber = 0;
    private const int _countDummy = 1;
    private const int _timePerDummy = 2;
    private const int _complexity = 3;
    private const int _noArmor = 4;
    private const int _oneArmor = 5;
    private const int _twoArmor = 6;
    private const int _defaultDummy = 7;
    private const int _withoutHelmetDummy = 8;
    private const int _twoShieldDummy = 9;
    private const int _changeShieldDummy = 10;

    private const char _cellSeporator = ',';
    private const char _inCellSeporator = ';';

    public LevelData[] ConvertData(string tableString)
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = tableString.Split(lineEnding);
        int dataStartRawIndex = 1;
        List<LevelData> datass = new List<LevelData>();

        for (int i = dataStartRawIndex; i < rows.Length; i++)
        {
            string[] cells = rows[i].Split(_cellSeporator);

            int levelNumber = ParseInt(cells[_levelNumber]);
            int countDummy = ParseInt(cells[_countDummy]);
            float timePerDummy = ParseFloat(cells[_timePerDummy]) * 1f;
            string complexity = cells[_complexity];
            float noArmor = ParseFloat(cells[_noArmor]) * 1f;
            float oneArmor = ParseFloat(cells[_oneArmor]) * 1f;
            float twoArmor = ParseFloat(cells[_twoArmor]) * 1f;
            float defaultDummy = ParseFloat(cells[_defaultDummy]) * 1f;
            float withoutHelmetDummy = ParseFloat(cells[_withoutHelmetDummy]) * 1f;
            float twoShieldDummy = ParseFloat(cells[_twoShieldDummy]) * 1f;
            float changeShieldDummy = ParseFloat(cells[_changeShieldDummy]) * 1f;

            float[] armorChance = new float[]{ twoArmor, oneArmor, noArmor };
            float[] dummyChance = new float[]{ defaultDummy, withoutHelmetDummy, twoShieldDummy, changeShieldDummy };

            LevelData data = new LevelData(levelNumber, countDummy, timePerDummy, complexity, armorChance, dummyChance);

            datass.Add(data);
        }

        return datass.ToArray();
    }

    private int ParseInt(string s)
    {
        if (int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out int result) == false)
            Debug.Log("Can't parse int, wrong text");

        return result;
    }

    private float ParseFloat(string s)
    {
        float result = -1;
        if (!float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
        {
            Debug.Log("Can't pars float,wrong text ");
        }

        return result;
    }

    private char GetPlatformSpecificLineEnd()
        {
            char lineEnding = '\n';
#if UNITY_IOS
        lineEnding = '\r';
#endif
            return lineEnding;
        }

}

public readonly struct LevelData
{
    public LevelData(int levelNumber, int countDummy, float timePerDummy, string complexity, float[] armorChance, float[] dummyChance)
    {
        LevelNumber = levelNumber;
        CountDummy = countDummy;
        TimePerDummy = timePerDummy;
        Complexity = complexity;
        ArmorChance = armorChance;
        DummyChance = dummyChance;
    }

    public readonly int LevelNumber;
    public readonly int CountDummy;
    public readonly float TimePerDummy;
    public readonly string Complexity;
    public readonly float[] ArmorChance;
    public readonly float[] DummyChance;
}