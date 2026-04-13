using Unity.VectorGraphics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;

    public int rows = 5;
    public int columns = 10;

    public AnimationCurve speed;
    public int amountAlive => totalInvaders - amountKilled;
    public int amountKilled { get; private set; }
    public int totalInvaders => rows * columns;
    public float percentKilled => (float)amountKilled / (float)totalInvaders;
    public float missileAttackRate = 1.0f;

    public Projectile projectilePrefab;
    private Vector3 direction = Vector3.right;



    private void Awake()
    {
        for(int row = 0; row < rows; row++)
        {
            float width = 2.0f * (columns - 1);
            float height = 2.0f * (rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x,centering.y + ( row * 2.0f), 0.0f);

            for (int col = 0; col < columns; col++)
            {
                Invader invader  = Instantiate(prefabs[row], transform);
                invader.Killed += OnInvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;

            }
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);
    }

    private void Update()
    {
        transform.position += direction * speed.Evaluate(percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && invader.position.x >= rightEdge.x - 1.0f)
            {
                AdvanceRow();
            }
            else if (direction == Vector3.left && invader.position.x <= leftEdge.x + 1.0f)
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        direction.x *= -1.0f;
        Vector3 position = transform.position;
        position.y -= 0.5f;
        transform.position = position;
    }

    private void OnInvaderKilled()
    {
        if (AllInvadersKilled())
        {
            Debug.Log("You win!");
        }
    }

    private bool AllInvadersKilled()
    {
        amountKilled++;
        if(amountKilled >= totalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return true;
        }
        return false;
    }


    private void MissileAttack()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            
            if (Random.value < (0.1f/ (float)amountAlive))
                {
                    Projectile missile = Instantiate(projectilePrefab, invader.position, Quaternion.Euler(0, 0, -90));
                    missile.direction = Vector3.down;
                    break;
                }
            }
        
    }
}