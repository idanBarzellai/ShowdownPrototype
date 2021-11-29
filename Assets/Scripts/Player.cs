using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Stats
    public int baseHealth;
    public int health;
    public bool isAlive;
    public float walkSpeed = 3f;
    public float jumpForce = 3f;
    //Attack
    public int attackDamage = 1;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    //DamageTaken
    bool gotAttacked = false;
    //public GameObject damageEffect;
    float dazedTime = 2f;
    float lastDazedTime;
    //Artifact
    public Transform artifactSlot;
    //public bool hasArtifact;
    protected Transform[] artifactInventory;
    protected float artifactTakingTimePause = 2f;
    protected float artifactTakingTimeStart = 0;
    public Artifact artifactToUse;
    //Other
    public LayerMask playerLayers;
    public LayerMask artifactLayers;
    public Animator anim;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected CapsuleCollider2D playerCollider;
    GameManager gameManager;
    //private Inventory inventory;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        gameManager = GameManager.instance;
        health = gameManager.baseHealth;
        baseHealth = gameManager.baseHealth;
        isAlive = true;
        if (this.name == "Player_1")
            artifactInventory = GameManager.instance.artifactSlotsP1;
        else if (this.name == "Player_2")
            artifactInventory = GameManager.instance.artifactSlotsP2;
        else if (this.name == "Player_3")
            artifactInventory = GameManager.instance.artifactSlotsP3;
        else if (this.name == "Player_4")
            artifactInventory = GameManager.instance.artifactSlotsP4;
    }
    public void Attack()
    {
        anim.SetTrigger("Attacking");
        if (artifactToUse != null && artifactToUse.isCurrentlyUsed && artifactToUse.thisArtifactType == Artifact.ArtifactType.Weapon)
            artifactToUse.AttackWithArtifact();
        else
        {
            //detect nearby enemies
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
            // do damage
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        //knockback
        lastDazedTime = Time.time;
        gotAttacked = true;
        anim.SetTrigger("TakingDmg");
        //Instantiate(damageEffect, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage taken");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    protected virtual void Update()
    {
        //Recover Animation
        anim.SetFloat("TookDmg", Time.time - lastDazedTime);
        if (gotAttacked && Time.time - lastDazedTime <= dazedTime)
        {
            walkSpeed = 0;
        }
        else
        {
            gotAttacked = false;
            walkSpeed = 3f;
        }

        if (health <= 0)
        {
            Death();
        }
        
        
        
    }
    void Death()
    {
        //make player half visible and motionless
        health = 0;
        isAlive = false;
        anim.SetBool("Dead", true);
        playerCollider.enabled = false;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
    }

    protected void PickUp(Collider2D[] artifacts)
    {
        //Picks up an articat if possible. 
        foreach (Collider2D artifact in artifacts)
        {
            //if player doesnt have an artifact in his use slot then put it there
            if (artifactToUse == null) //  !hasArtifact
                artifact.GetComponent<Artifact>().Attach(artifactSlot, this, false);
            //else put in his inventory' if inventory full say it to him
            else
            {
                if (!GameManager.instance.isInventoryFull(artifactInventory))
                    for (int i = 0; i < artifactInventory.Length; i++)
                    {
                        if (artifactInventory[i].tag == "EmptySlot")
                        {
                            artifact.GetComponent<Artifact>().Attach(artifactInventory[i].transform, this, false);
                            break;
                        }
                    }
                else
                {
                    Debug.Log("Inventory Is Full");
                    artifact.GetComponent<Artifact>().particleEffect.SetActive(false);
                }
            }
        }
    }

    protected void UseArtifact()
    {
        if(artifactToUse != null)
        {
            artifactToUse.UseArtifact();
        }
        if (!GameManager.instance.isInventoryEmpty(artifactInventory) && artifactToUse == null)
        {
            GameManager.instance.GetNextArtifactAndRefresh(artifactInventory, this);

        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        baseHealth = data.baseHealth;
        health = data.health;
        isAlive = data.isAlive;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

    }
}
