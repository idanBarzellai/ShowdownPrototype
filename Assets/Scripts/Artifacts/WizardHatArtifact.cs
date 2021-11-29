using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHatArtifact : Artifact
{
    // given 5 fireballs to shoot. can friendly fire
    public Projectile fireball;
    Projectile fireballClone;
    SpriteRenderer wizardHatTransformSprite;
    protected Sprite wizardHatToWearSprite; //put in artifacttowear
    //protected GameObject wizardHatToWear = new GameObject("wizardHatToWear");
    int fireballsLeft;
    bool used = false;

    protected override void Start()
    {
        base.Start();
        thisArtifactType = ArtifactType.Weapon;
        fireball.projectileCount = 5;
        fireballsLeft = fireball.projectileCount;
        wizardHatToWearSprite = GetComponent<SpriteRenderer>().sprite;
        /*
        wizardHatToWear.AddComponent<SpriteRenderer>();
        wizardHatToWear.GetComponent<SpriteRenderer>().sprite = wizardHatToWearSprite;
        //wizardHatTransformSprite = GetComponent<SpriteRenderer>();
        */
    }
    public override void UseArtifact()
    {
        if (!used)
        {
            base.UseArtifact();
            //Instantiate(wizardHatToWear, onThisPlayer.transform.position + new Vector3(-0.8f, 0.57f, 0), onThisPlayer.transform.rotation, onThisPlayer.transform);

            //wizardHatTransform.transform.position += new Vector3(-0.1f, 0.67f, 0);
            //wizardHatTransformSprite.enabled = true;
            // wizardHatToWear.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            //wizardHatToWear.transform.rotation = new Quaternion(0, 180f, 0, 0);
            used = true;
        }
    }
    public override void AttackWithArtifact()
    {
        if (fireballsLeft > 0)
        {
            base.AttackWithArtifact();
            fireballClone = Instantiate(fireball, onThisPlayer.attackPoint.position, onThisPlayer.attackPoint.rotation);
            fireballClone.transform.rotation = Quaternion.Euler(0, 0, 90);
            fireballsLeft -= 1;
            if (onThisPlayer.transform.rotation.y == 0)
                fireballClone.sideForVelocity = -1;
            else
                fireballClone.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
            fireballsLeft = 0;
    }

    protected override void Update()
    {
        base.Update();
        //plaayer loooking left

        if (fireballsLeft <= 0)
            DestoryArtifact();
    }
}
