using System;
using Environment;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private const float Speed = 4.0f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * Speed));


        if (transform.position.y < Level.BottomBound)
        {
            transform.position = new Vector3(Random.Range(Level.LeftBound, Level.RightBound), Level.TopBound, 0);
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

                Debug.Log("Player damaged");
                break;
            default:
                Debug.Log("Collision with unknown object");
                break;
        }
    }
}