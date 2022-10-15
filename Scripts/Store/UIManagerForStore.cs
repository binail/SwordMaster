using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerForStore : MonoBehaviour
{
    [SerializeField] private StoreSelectors storeSelectors;
    [SerializeField] AudioSource source;
    [SerializeField] private AudioClip click;

    public void OnRightButton()
    {
        SoundManager.instance.PlaySound(click);
        storeSelectors.OnRightButton();
    }

    public void OnLeftButton()
    {
        SoundManager.instance.PlaySound(click);
        storeSelectors.OnLeftButton();
    }

    public void ChooseButton()
    {
        SoundManager.instance.PlaySound(click);
        storeSelectors.ChoseSword();
    }

    public void BuySword()
    {
        SoundManager.instance.PlaySound(click);
        storeSelectors.BuySword();
    }

    public void Return()
    {
        SoundManager.instance.PlaySound(click);
        StartCoroutine(WaitBeforeReturn());
    }

    private IEnumerator WaitBeforeReturn()
    {
        yield return new WaitForSeconds(0.1f);

        Transitions.LoadScene(1);
    }
}
