using System.Collections;
using UnityEngine;

public class TrailActivator : MonoBehaviour
{
    private TrailRenderer trailRenderer;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }

    private void OnEnable()
    {
        SwipeDetection.SwipeEvent += OnSwipe;
    }

    private void OnDisable()
    {
        SwipeDetection.SwipeEvent -= OnSwipe;
    }

    private void OnSwipe(Vector2 _direction)
    {
        if (_direction == Vector2.up) return;

        StartCoroutine(TrailActivity());
    }

    private IEnumerator TrailActivity()
    {
        trailRenderer.enabled = !trailRenderer.enabled;

        yield return new WaitForSeconds(0.35f);

        trailRenderer.enabled = !trailRenderer.enabled;
    }
}
