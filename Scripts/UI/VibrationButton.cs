using UnityEngine;
using UnityEngine.UI;

public class VibrationButton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private Sprite[] _images;

    private Image _sourseImage;

    private void Start()
    {
        _sourseImage = GetComponent<Image>();

        if (PlayerPrefs.GetInt("Vibtation") == 0) _sourseImage.sprite = _images[0];
        else _sourseImage.sprite = _images[1];
    }

    public void SwitchVibration()
    {
        if (PlayerPrefs.GetInt("Vibtation") == 1)
        {
            PlayerPrefs.SetInt("Vibtation", 0);
            _sourseImage.sprite = _images[0];
            //Vibrator.Vibrate(100);
        }
        else
        { 
            PlayerPrefs.SetInt("Vibtation", 1);
            _sourseImage.sprite = _images[1];
        }

        SoundManager.instance.PlaySound(_clickSound);
    }
}
