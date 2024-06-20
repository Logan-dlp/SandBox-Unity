using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShipMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;
    
    private Rigidbody _rb;
    private Vector3 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        
        _rb.velocity = _direction * _speedMovement;
    }
}
