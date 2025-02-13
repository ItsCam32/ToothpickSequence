using UnityEngine;

public class EndCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
