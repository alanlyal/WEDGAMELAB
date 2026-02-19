using UnityEngine;

public class Bullet : MonoBehaviour
{
  

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
