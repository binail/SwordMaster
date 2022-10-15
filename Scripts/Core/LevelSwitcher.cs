using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    public void LoadScene(int _scene)
    {
        StopAllCoroutines();

        StartCoroutine(LoadAsync(_scene));
    }

    private IEnumerator LoadAsync(int _scene)
    {
        loadingScreen.SetActive(true);

        var operation = SceneManager.LoadSceneAsync(_scene);

        while (operation.isDone == false)
            yield return null;

       yield return new WaitForSeconds(0.2f);

        loadingScreen.SetActive(false);
    }
}
