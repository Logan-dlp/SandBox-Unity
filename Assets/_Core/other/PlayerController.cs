using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private KeyCode _jumpInput = KeyCode.Space;
    [SerializeField, Range(100, 500)] private float _force;

    private bool _isGrounded = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKey(_jumpInput) && _isGrounded)
        {
            _rb.AddForce(transform.up * _force);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "CollisionFloor")
        {
            _isGrounded = true;
        }

        if (other.gameObject.tag == "Block")
        {
            Debug.Log("Death");
            Time.timeScale = 0;
        }
    }
}