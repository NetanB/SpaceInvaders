using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public Vector3 direction;

public System.Action onDestroyed;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
{
    // Only react to valid targets
    if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
        other.gameObject.layer == LayerMask.NameToLayer("Bunker") ||
        other.gameObject.layer == LayerMask.NameToLayer("Missile"))
    {
        onDestroyed?.Invoke();
        Destroy(gameObject);
    }
}

    
}
