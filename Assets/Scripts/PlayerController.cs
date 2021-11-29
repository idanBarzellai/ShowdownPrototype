using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    public string walkInput;
    public string ghostInput;
    public KeyCode jumpInput;
    public KeyCode attackInput;
    public KeyCode interactInput;
    // dashInput;
    public KeyCode useInput;
    bool isGrounded;

    Collider2D[] artifactsNearby;
    float lastAttackTime = 0f;
    float attackPause = 2f;

    // need to gravity control
    //
    protected override void Update()
    {
        base.Update();
        if (Input.GetKey(interactInput))
        {
            anim.SetBool("isTakingArtifact", true);
            artifactsNearby = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, artifactLayers);
            if (artifactsNearby.Length > 0)
            {
                artifactTakingTimeStart += Time.deltaTime;
                artifactsNearby[0].GetComponent<Artifact>().particleEffect.SetActive(true);
            }
            //if the artifactwaiting time is over you can pickup the artifact without having to letgo of the interact key
        }
        if (Input.GetKeyUp(interactInput))
        {
            if (artifactsNearby.Length > 0)
            {
                if (artifactTakingTimeStart >= artifactTakingTimePause)
                    PickUp(artifactsNearby);
                else
                    artifactsNearby[0].GetComponent<Artifact>().particleEffect.SetActive(false);
            }
            artifactTakingTimeStart = 0;
            anim.SetBool("isTakingArtifact", false);
        }

        if (Input.GetKey(useInput) && artifactToUse != null)
        {
            if(!artifactToUse.isCurrentlyUsed)
                UseArtifact();
        }
    }
    void FixedUpdate()
    {
        float walk = Input.GetAxis(walkInput);
        float ghost = Input.GetAxis(ghostInput);

        if (isAlive)
        {
            transform.position += new Vector3(walk, 0, 0) * Time.deltaTime * walkSpeed;
            //Walking Animation
            if (Mathf.Abs(walk) > 0)
                anim.SetBool("isWalking", true);
            else
                anim.SetBool("isWalking", false);
            //Walking to both sides transform
            if (!Mathf.Approximately(0, walk))
                transform.rotation = walk > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            //Jump
            if (Input.GetKey(jumpInput) && isGrounded)
            {
                anim.SetBool("isGrounded", false);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            //Attack
            if (Time.time - lastAttackTime >= attackPause)
            {
                if (Input.GetKey(attackInput))
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            //Attack animation
            anim.SetFloat("Attacked", Time.time - lastAttackTime);
        }
        //Dead
        else
        {
            //transform.position += new Vector3(walk, ghost, 0) * Time.deltaTime * walkSpeed; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    
}
