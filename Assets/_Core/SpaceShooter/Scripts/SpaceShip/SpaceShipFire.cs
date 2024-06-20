using UnityEngine;

public class SpaceShipFire : MonoBehaviour
{
    [SerializeField] private KeyCode _fireInput = KeyCode.Space;
    [SerializeField] private Transform[] _spawnFireArray;
    [SerializeField] private GameObject _munitionGameObject;
    [SerializeField] private float _fireRate;

    private float _timeRate;

    private void Awake()
    {
        _timeRate = _fireRate;
    }

    private void Update()
    {
        if (_timeRate < _fireRate)
        {
            _timeRate += Time.deltaTime;
        }
        
        if (Input.GetKey(_fireInput))
        {
            if (_timeRate >= _fireRate)
            {
                foreach (Transform spawnFireItem in _spawnFireArray)
                {
                    Instantiate(_munitionGameObject, spawnFireItem.position, spawnFireItem.rotation);
                }
                _timeRate = 0;
            }
        }
    }
}
