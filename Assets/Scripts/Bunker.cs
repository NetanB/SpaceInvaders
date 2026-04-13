using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int maxHits = 5;
    private int currentHits;
    private Vector3 originalScale;
    
    private void Awake()
    {
        originalScale = transform.localScale;
        currentHits = 0;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser") || 
            other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            Destroy(other.gameObject);
            TakeHit();
        }
    }
    
    private void TakeHit()
    {
        currentHits++;
        
        if (currentHits >= maxHits)
        {
            Destroy(gameObject);
        }
        else
        {
            // Scale decreases with each hit
            float scalePercent = 1f - ((float)currentHits / maxHits);
            transform.localScale = originalScale * scalePercent;
        }
    }
}