using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    private static LevelSwitcher _switcher;

    public static void Inject(LevelSwitcher switcher)
    {
        _switcher = switcher;
    }

    public static void LoadScene(int scene)
    {
        _switcher.LoadScene(scene);
    }
}
