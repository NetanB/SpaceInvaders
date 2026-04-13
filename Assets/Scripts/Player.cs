using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public Projectile projectilePrefab;
    private bool canShoot;

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(inputX, 0, 0) * speed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!canShoot)
        {
             Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler (0, 0, 90)  );
            projectile.onDestroyed += LaserDestroyed;
            canShoot = true;
        }

        
       
    }
    private void LaserDestroyed()
    {
        canShoot = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") || other.gameObject.layer == LayerMask.NameToLayer("Invader")    )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}