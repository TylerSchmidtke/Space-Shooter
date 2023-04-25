using Environment;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float Speed = 4.0f;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * Speed));


        if (transform.position.y < Level.BottomBound)
        {
            transform.position = SpawnManager.GetRandomSpawnPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case Tag.Laser:
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
            case Tag.Player:
                var player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.Damage();
                }
                
                break;
            default:
                Debug.Log("Collision with unknown object");
                break;
        }
    }
}