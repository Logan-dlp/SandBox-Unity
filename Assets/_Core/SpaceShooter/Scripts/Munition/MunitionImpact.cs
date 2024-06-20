using UnityEngine;

public class MunitionImpact : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out AsteroidMovement asteroidMovement))
        {
            Destroy(other.gameObject);
        }
    }
}
