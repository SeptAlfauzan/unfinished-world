﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody playerRb;
    public Vector3 movement;

    public GameObject playerSprite;

    GameObject playerpointLight;
    bool isFlipped = false; 

    bool IsGrounded = false;//boolean when player collide with ground
    Vector3 jumpVector = new Vector3(0,1,0);
    Vector3 newPlayerLocalScale;//reference for new player localscale
    public float jumpForce;

    public ParticleSystem landingParticle;
    // Quaternion rotateOffset = new Quaternion(70f, 180f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        newPlayerLocalScale = new Vector3(1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        

        RotateDirection(movement.x);
        movement*= speed;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            IsGrounded = false;
            Jump();
        }
        Debug.Log(IsGrounded);
    }

    private void FixedUpdate() {
        playerRb.velocity = new Vector3(movement.x, playerRb.velocity.y, movement.z);
    }
    private void RotateDirection(float xAxis) {
        if (xAxis < 0 && !isFlipped)
        {//change variable reference value base on player x axis value(right or left move)
            isFlipped = true;
            newPlayerLocalScale = new Vector3(-1,1,1); 
        }
        if(xAxis > 0 && isFlipped){
            isFlipped = false;
            newPlayerLocalScale = new Vector3(1,1,1); 
        }
        FlipPlayerObject(newPlayerLocalScale);
    }


    private void FlipPlayerObject(Vector3 newLocalScale){
        playerSprite.transform.localScale = newLocalScale;
    }


    private void OnCollisionStay(Collision collider) {
        if (collider.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collider) {
        if (collider.gameObject.tag == "Ground")
        {
            IsGrounded = false;
            Debug.Log("Player on Air");
        }
    }

    private void OnCollisionEnter(Collision collider) {
        if (collider.gameObject.tag == "Ground")
        {
            if (!landingParticle.isEmitting)//check wheter landing particle is not emiting 
            {
                landingParticle.Play();//play landing particle
            }
        }
    }



    private void Jump(){
        playerRb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
    }
    


}
