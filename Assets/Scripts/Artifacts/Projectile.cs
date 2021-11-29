using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    bool explode= false;
    protected float projectileSpeed = 5f;
    public float sideForVelocity = 1;
    protected Rigidbody2D projectileRb;
    protected Collider2D projectileColl;
    protected int damagePoint = 1;
    public int projectileCount = 3;
    public Animator anim;

    void Start()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        projectileRb.velocity = new Vector2(projectileSpeed * sideForVelocity, 0);
        if(explode)
            Invoke("DestoryTheProjectileGameObject", 0.1f);

    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Portal" && other.gameObject.tag != "Artifact")
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().TakeDamage(damagePoint);
            }
            anim.SetTrigger("Collide");
            explode = true;
        }
    }

    void DestoryTheProjectileGameObject()
    {
        Destroy(this.gameObject);
        
    }
}
