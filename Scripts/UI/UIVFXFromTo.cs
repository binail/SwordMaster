using System.Collections;
using UnityEngine;

public class UIVFXFromTo : MonoBehaviour
{
    [SerializeField] private iTween.EaseType easeType;
    [SerializeField] private GameObject particle;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] private float time;
    [SerializeField] private float dispersion;

    public void FromTo (int _countOfParticles, float _time)
    {
        StartCoroutine(FromToCo(_countOfParticles, _time));
    }

    IEnumerator FromToCo(int _countOfParticles, float _time)
    {
        for (int i = 0; i < _countOfParticles; i++)
        {
            var _vfx = Instantiate(particle, (startPoint.position + CreateRandomVector()), Quaternion.identity) as GameObject;
            _vfx.transform.SetParent(startPoint.transform);
            iTween.MoveTo(_vfx, iTween.Hash("position", endPoint.transform.position, "easetype", easeType, "ignoretimescale", true, "time", time));
            Destroy(_vfx, time + 1);
            Vibrator.Vibrate(20);
            yield return new WaitForSeconds(_time / _countOfParticles);
        }

        yield return null;
    }

    private Vector3 CreateRandomVector()
    {
        return new Vector3(Random.Range(-dispersion, dispersion), 
            Random.Range(-dispersion, dispersion), 
            Random.Range(-dispersion, dispersion));
    }

}
