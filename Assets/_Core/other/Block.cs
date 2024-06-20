using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private Transform _unspawnTransform;
    
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _rb.useGravity = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _unspawnTransform.position) <= .5f)
        {
            transform.position = _spawnTransform.position;
        }
        
        _rb.velocity = _direction * _speed;
    }
}
