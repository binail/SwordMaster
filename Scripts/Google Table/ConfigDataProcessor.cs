using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[DisallowMultipleComponent]
public class ConfigDataProcessor : MonoBehaviour
{
    private const string _url = "https://docs.google.com/spreadsheets/d/*/export?format=csv";

    public void DownloadTable(string tableId)
    {
        string actualUrl = _url.Replace("*", tableId);

        StartCoroutine(DownloadRawTable(actualUrl));
    }
    
    private IEnumerator DownloadRawTable(string actualUrl)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(actualUrl))
        {
            yield return request.SendWebRequest();
        
            if (request.result == UnityWebRequest.Result.ConnectionError || 
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                PlayerPrefs.SetString("levelInformation", request.downloadHandler.text);
            }
        }
    }
}
