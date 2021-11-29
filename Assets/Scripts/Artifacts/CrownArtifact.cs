using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownArtifact : Artifact
{
    // Adding Attack damage for 20secs
    int addedAttackDamage = 2;
    int baseAttackDamage;

    protected override void Start()
    {
        base.Start();
        thisArtifactType = ArtifactType.Damage;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isCurrentlyUsed)
            onThisPlayer.GetComponent<Player>().attackDamage = addedAttackDamage;
    }
    public override void UseArtifact()
    {
        baseAttackDamage = onThisPlayer.GetComponent<Player>().attackDamage;
        base.UseArtifact();
        Invoke("DestoryArtifact", 20f);
    }
    public override void DestoryArtifact()
    {
        onThisPlayer.GetComponent<Player>().jumpForce = baseAttackDamage;
        base.DestoryArtifact();
    }
}
