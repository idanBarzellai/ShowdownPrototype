using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEgg : Artifact
{
    // Adding jumpforce for 20secs
    float addedJumpForce = 5f;
    float baseJumpForce;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isCurrentlyUsed)
            onThisPlayer.GetComponent<Player>().jumpForce = addedJumpForce;
    }
    public override void UseArtifact()
    {
        baseJumpForce = onThisPlayer.GetComponent<Player>().jumpForce;
        base.UseArtifact();
        Invoke("DestoryArtifact", 20f);
    }
    public override void DestoryArtifact()
    {
        onThisPlayer.GetComponent<Player>().jumpForce = baseJumpForce;
        base.DestoryArtifact();
    }
}
