using UnityEngine;
using UnityEngine.SceneManagement;
public class Invader : MonoBehaviour
{
  public Sprite[] animationSprites;
  public float animationTime = 1.0f;
  private SpriteRenderer spriteRenderer;
  private int animationFrame;

  public System.Action Killed;

    private void Awake()
    {
     spriteRenderer = GetComponent<SpriteRenderer>();
    } 

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        spriteRenderer.sprite = animationSprites[animationFrame];
    }
private void OnTriggerEnter2D(Collider2D other)
{
    int layer = other.gameObject.layer;

    if (layer == LayerMask.NameToLayer("Laser"))
    {
        Killed?.Invoke();
        gameObject.SetActive(false);
    }
    else if (layer == LayerMask.NameToLayer("Bunker"))
    {
        // destroy bunker on contact (classic behavior)
        Destroy(other.gameObject);
    }
    else if (layer == LayerMask.NameToLayer("Player"))
    {
        // trigger game over
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
}