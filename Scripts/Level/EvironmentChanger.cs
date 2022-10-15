using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvironmentChanger : MonoBehaviour
{
    [SerializeReference] private Camera _camera;
    [SerializeReference] private Color[] _backgroundColor;
    [SerializeReference] private GameObject[] _environment;

    private int _levelNumber;

    void Start()
    {
        _levelNumber = PlayerPrefs.GetInt("levelNumber");
        _levelNumber %= 40;

        switch (_levelNumber)
        {
            case < 10:
                {
                    _camera.backgroundColor = _backgroundColor[0];
                    _environment[0].SetActive(true);
                    break;
                }
            case < 20:
                {
                    _camera.backgroundColor = _backgroundColor[1];
                    _environment[1].SetActive(true);
                    break;
                }
            case < 30:
                {
                    _camera.backgroundColor = _backgroundColor[2];
                    _environment[2].SetActive(true);
                    break;
                }
            case <= 40:
                {
                    _camera.backgroundColor = _backgroundColor[3];
                    _environment[3].SetActive(true);
                    break;
                }
        }
    }
}
