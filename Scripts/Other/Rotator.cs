using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _speed;

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, -_speed * Time.deltaTime));
    }
}
