using Environment;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private const float Speed = 8.0f;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.up * (Speed * Time.deltaTime));

        // if the laser is past the top of the screen
        if (transform.position.y > Level.TopBound)
        {
            Destroy(this.gameObject);
        }
    }
}