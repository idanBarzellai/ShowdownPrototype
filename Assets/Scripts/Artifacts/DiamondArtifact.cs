using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondArtifact : Artifact
{
    // Adding 1 life point back
    int addHp = 1;


    protected override void Start()
    {
        base.Start();
        thisArtifactType = ArtifactType.Buffer;
    }
    public override void UseArtifact()
    {
        base.UseArtifact();
        if (onThisPlayer.GetComponent<Player>().health < onThisPlayer.GetComponent<Player>(). baseHealth)
            onThisPlayer.GetComponent<Player>().health += addHp;
        DestoryArtifact();
    }
}