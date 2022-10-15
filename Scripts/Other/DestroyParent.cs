using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    void Start()
    {
        Destroy(transform.parent.gameObject);
    }
}
