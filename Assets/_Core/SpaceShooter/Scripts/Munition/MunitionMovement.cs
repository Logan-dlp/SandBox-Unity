using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MunitionMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _TimeToUnspawn;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        StartCoroutine(UnspawnObjectAfterTime(_TimeToUnspawn));
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector3.forward * _speedMovement;
    }

    private IEnumerator UnspawnObjectAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
