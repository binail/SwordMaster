using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private LevelSwitcher _transitions;

    [SerializeField] private bool _isTransitingToMenu = true;

    private void Awake()
    {
        DontDestroyOnLoad(_transitions);

        Transitions.Inject(_transitions);
    }

    private void Start()
    {
        if (_isTransitingToMenu == true)
            Transitions.LoadScene(1);
    }
}
