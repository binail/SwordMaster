using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private Sprite[] _images;

    private Image _sourseImage;
    private int _condition;
    private readonly string _prefsName = "SoundCondition";

    void Start()
    {
        _sourseImage = GetComponent<Image>();

        _condition = PlayerPrefs.GetInt(_prefsName);

        ConfirmVolume(_condition);
    }

    private void ConfirmVolume(int condition)
    {
        switch (condition)
        {
            case 0:
                _sourseImage.sprite = _images[condition];
                _audioSource.volume = 0.66f;
                break;
            case 1:
                _sourseImage.sprite = _images[condition];
                _audioSource.volume = 0.33f;
                break;
            case 2:
                _sourseImage.sprite = _images[condition];
                _audioSource.volume = 0f;
                break;
            case 3:
                _sourseImage.sprite = _images[condition];
                _audioSource.volume = 1f;
                break;
        }
    }
    public void ChangeVolume()
    {
        SoundManager.instance.PlaySound(_clickSound);
        _condition++;
        if (_condition > 3) _condition = 0;
        ConfirmVolume(_condition);
        PlayerPrefs.SetInt(_prefsName, _condition);
    }
    

}
