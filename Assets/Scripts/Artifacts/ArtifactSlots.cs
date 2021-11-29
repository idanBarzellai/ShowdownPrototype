using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactSlots : MonoBehaviour
{
    public Transform slotPosition;
    private Sprite fullArtifactImage;
    private GameObject artifactOnPlayer;

    private void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "ArtifactOnPlayer")
                artifactOnPlayer = child.gameObject;
        }
        if (artifactOnPlayer != null)
        {
            fullArtifactImage = artifactOnPlayer.GetComponent<SpriteRenderer>().sprite;
            
            slotPosition.GetComponent<SpriteRenderer>().sprite = fullArtifactImage;
        }
    }
}
