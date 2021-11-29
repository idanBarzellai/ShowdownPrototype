
using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    public enum ArtifactType
    {
        Wearable,
        Buffer,
        Damage,
        Hazard,
        Global, 
        Weapon
    }

    //public ArtifactType artifactType;
    //public int amount;
    public ArtifactType thisArtifactType;
    public GameObject particleEffect;
    protected Player onThisPlayer;
    public bool isCurrentlyUsed = false;
    Rigidbody2D rb;
    SpriteRenderer artifactSprite;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        artifactSprite = GetComponent<SpriteRenderer>();
    }
    
    protected virtual void Update()
    {
    }
    //Wearable

    public void Attach(Transform artifactSlot, Player player, bool updating)
    {
        if (tag == "Artifact")
        {
            tag = "ArtifactOnPlayer";
            transform.parent = artifactSlot;
            artifactSlot.tag = "FullSlot";
            artifactSlot.GetComponent<Image>().enabled = true;
            artifactSlot.GetComponent<Image>().sprite = artifactSprite.sprite;
            Destroy(rb);
            GetComponent<Collider2D>().enabled = false;
            artifactSprite.enabled = false;
            particleEffect.SetActive(false);
            onThisPlayer = player;
            player.artifactToUse = this;
        }
        if(tag == "ArtifactOnPlayer")
        {
            transform.parent = artifactSlot;
            artifactSlot.tag = "FullSlot";
            artifactSlot.GetComponent<Image>().enabled = true;
            artifactSlot.GetComponent<Image>().sprite = artifactSprite.sprite;
            if (updating)
            {
                player.artifactToUse = this;
            }
        }
    }
    public void UnAttach()
    {
        transform.parent.tag = "EmptySlot";
        transform.parent.GetComponent<Image>().enabled = false;
        onThisPlayer.artifactToUse = null;
    }

    public virtual void UseArtifact()
    {
        //if wearable the war it , if buffer then buff etc.
        isCurrentlyUsed = true;
    }

    public virtual void AttackWithArtifact()
    {
    }

    public virtual void DestoryArtifact()
    {
        UnAttach();
        Destroy(this);
        isCurrentlyUsed = false;
    }
}
