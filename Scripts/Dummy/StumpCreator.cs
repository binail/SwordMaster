using UnityEngine;
using System.Collections;

public class StumpCreator : MonoBehaviour
{
    [SerializeField] private GameObject _headStumpDummy;
    [SerializeField] private GameObject _bodyStumpDummy;

    [SerializeField] private float _dispersion;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    [Header ("Points")]
    [SerializeField] private Transform _firstDummyPosition;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private Transform _topPoint;

    [Header("BodyMaterials")]
    [SerializeField] private Material _defaultDummyMaterial;
    [SerializeField] private Material _woodShieldDummyMaterial;
    [SerializeField] private Material _shieldChangeDummyMaterial;
    [SerializeField] private Material _withoutHelmetDummyMaterial;


    private Color _defaultDummy;
    private Color _woodShieldDummy;
    private Color _shieldChangeDummy;
    private Color _withoutHelmetDummy;

    private void Start()
    {
        _defaultDummy = _defaultDummyMaterial.color;
        _woodShieldDummy = _woodShieldDummyMaterial.color;
        _shieldChangeDummy = _shieldChangeDummyMaterial.color;
        _withoutHelmetDummy = _withoutHelmetDummyMaterial.color;
    }

    public void CreateDummyStump (Vector3 direction, string tag)
    {
        Color color = ChooseColor(tag);

        if (direction == Vector3.up)
        {
            var stump = Instantiate(_headStumpDummy, _firstDummyPosition.position, Quaternion.identity);
            stump.GetComponent<ChangeStumpColor>().ChangeStumpsColor(color);
            Explode(_topPoint.position, stump);
        }
        if (direction == Vector3.left)
        {
            var stump = Instantiate(_bodyStumpDummy, _firstDummyPosition.position, Quaternion.identity);
            stump.GetComponent<ChangeStumpColor>().ChangeStumpsColor(color);
            Explode(_leftPoint.position, stump);
        }
        if (direction == Vector3.right)
        {
            var stump = Instantiate(_bodyStumpDummy, _firstDummyPosition.position, Quaternion.identity);
            stump.GetComponent<ChangeStumpColor>().ChangeStumpsColor(color);
            Explode(_rightPoint.position, stump);
        }
    }

    private Color ChooseColor(string tag)
    {
        if (tag == "DefaultDummy") return _defaultDummy;
        if (tag == "WithoutHelmetDummy") return _withoutHelmetDummy;
        if (tag == "TwoShieldDummy") return _woodShieldDummy;
        if (tag == "ShieldChangeDummy") return _shieldChangeDummy;

        return _defaultDummy;
    }

    private void Explode(Vector3 position, GameObject stump)
    {
        position.x += Random.Range(-_dispersion, _dispersion);
        position.y += Random.Range(-_dispersion, _dispersion);
        position.z += Random.Range(-_dispersion, _dispersion);

        for (int i = 0; i < 3; i++)
        {
            Rigidbody rigidbody = stump.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
            stump.transform.GetChild(0).SetParent(null);
            rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
        }
    }
}
