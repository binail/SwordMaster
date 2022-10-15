using UnityEngine;

public class Penetration : MonoBehaviour
{
    [SerializeField] GameObject _sparks;
    [SerializeField] Transform _topPoint;
    [SerializeField] Transform _leftPoint;
    [SerializeField] Transform _rightPoint;

    public void IllustratePenetration (Vector2 direction)
    {
        if (direction == Vector2.up)    Instantiate(_sparks, _topPoint.position, Quaternion.Euler(-130, 0, 0));
        else if (direction == Vector2.left)     Instantiate(_sparks, _leftPoint.position, Quaternion.Euler(-180, 10, 90));
        else if (direction == Vector2.right)     Instantiate(_sparks, _rightPoint.position, Quaternion.Euler(-180, -10, 90));
    }
}
