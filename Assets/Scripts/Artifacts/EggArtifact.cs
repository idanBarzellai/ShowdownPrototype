using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggArtifact : Artifact
{
    // Adding walking speed for 20secs
    float addedWalkSpeeed = 5f;
    float baseWalkSpeed;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        thisArtifactType = ArtifactType.Buffer;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isCurrentlyUsed)
            onThisPlayer.GetComponent<Player>().walkSpeed = addedWalkSpeeed;
    }
    public override void UseArtifact()
    {
        baseWalkSpeed = onThisPlayer.GetComponent<Player>().jumpForce;
        base.UseArtifact();
        Invoke("DestoryArtifact", 20f);
    }
    public override void DestoryArtifact()
    {
        onThisPlayer.GetComponent<Player>().walkSpeed = baseWalkSpeed;
        base.DestoryArtifact();
    }
}