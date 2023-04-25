using Environment;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    private const float Speed = 5.0f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private SpawnManager spawnManager;
    private const float LaserOffset = 0.8f;
    public float fireRate = 0.5f;
    private float _nextFire = -1f;

    // Damage
    public void Damage()
    {
        lives--;
        if (lives > 1)
        {
            Debug.Log("Player damaged");
            return;
        }
        Destroy(this.gameObject);
        if (spawnManager != null)
        {
            spawnManager.OnPlayerDeath();
        }
        Debug.Log("Player destroyed");
    }

    // Start is called before the first frame update
    private void Start()
    {
        // take the current position and assign the start position
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find(Obj.SpawnManager).GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();

        if ((Input.GetKeyDown(KeyCode.Space) && (Time.time > _nextFire)))
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        _nextFire = Time.time + fireRate;
        Instantiate(laserPrefab,
            transform.position + new Vector3(0, LaserOffset, 0),
            Quaternion.identity);
    }

    private void CalculateMovement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontalInput, verticalInput, 0);
        var pos = transform.position;
        transform.Translate(direction * (Speed * Time.deltaTime));

        // if the player is on the right side of the screen
        if (transform.position.x > Level.RightBound)
        {
            // move the player to the left side of the screen
            transform.position = new Vector3(Level.LeftBound, pos.y, 0);
        }
        // if the player is on the left side of the screen
        else if (transform.position.x < Level.LeftBound)
        {
            // move the player to the right side of the screen
            transform.position = new Vector3(Level.RightBound, pos.y, 0);
        }

        pos = transform.position;
        transform.position =
            new Vector3(pos.x, Mathf.Clamp(pos.y, Level.BottomBound, Level.TopBound), 0);
    }
}