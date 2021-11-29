using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform exitPortal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Artifact" || collision.gameObject.tag == "Projectile")
        {
            collision.gameObject.transform.position = exitPortal.position;
        }
    }
}
