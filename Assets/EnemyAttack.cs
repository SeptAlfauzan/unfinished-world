﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Attack(){
        rigidbody.velocity = Vector3.zero;//stop enemy moving
        animator.SetTrigger("Attack");
        Collider[] players = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (var player in players)
        {
            Debug.Log(player.name);
            player.GetComponent<Player>().TakeDamaged(2);
        }

        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}