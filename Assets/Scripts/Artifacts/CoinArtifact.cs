using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinArtifact : Artifact
{
    // teleports the player to the exsiting artifact spawn place;
    Vector3 teleportToThisPoint;
    GameObject[] artifactSpawnPlaces;
    bool thereIsAnArtifactOnMap = false;

    protected override void Start()
    {
        base.Start();
        thisArtifactType = ArtifactType.Global;
    }
    public override void UseArtifact()
    {
        base.UseArtifact();
        artifactSpawnPlaces = GameObject.FindGameObjectsWithTag("ArtifactSpawnPlace");
        foreach (var artifactSpawnPlace in artifactSpawnPlaces)
        {
            if (artifactSpawnPlace.transform.childCount > 0)
            {
                teleportToThisPoint = artifactSpawnPlace.transform.position;
                thereIsAnArtifactOnMap = true;
            }
        }
        if(thereIsAnArtifactOnMap)
            onThisPlayer.transform.position = teleportToThisPoint;
        DestoryArtifact();
    }

}