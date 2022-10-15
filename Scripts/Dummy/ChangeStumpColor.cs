using UnityEngine;

public class ChangeStumpColor : MonoBehaviour
{
    [SerializeField] MeshRenderer[] _stumps;

    public void ChangeStumpsColor(Color color)
    {
        foreach (var stump in _stumps)
        {
            foreach (var material in stump.materials)
            {
                material.color = color;
            }
        }
    }
}
