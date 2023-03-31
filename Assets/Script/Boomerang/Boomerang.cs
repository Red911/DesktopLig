using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float speed = 10f;
    public float distance = 10f;
    public GameObject playerOwner;
    private bool returning = false;
    public float rotationSpeed = 10f;
    private Vector3 originalPosition;
    private Rigidbody rb;
    private Vector3 startPosition; 
    

    [Header("Bounce")] 
    [HideInInspector] public bool activeBounce;
    public float bounceSpeed = 7f;
    [Range(0.1f, 0.9f)] [Tooltip("Ex : si tu met 0.9 ça fera 10% de perte donc si tu veux faire 90% de pert tu met 0.1")]
    public float percentagePertVelocity = 0.9f;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        
        // transform.Rotate(90f, 0f, 0f, Space.Self);
        
        // rb.velocity = transform.forward * speed;
        // rb.angularVelocity = new Vector3(0, 0, 20);
    }

    void FixedUpdate()
    {
        if (returning)
        {
            // Le boomerang revient vers le joueur
            Vector3 direction = (playerOwner.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

            // Rotation du boomerang pendant le retour
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            
        }
        else
        {
            // Le boomerang est lancé
            rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);

            // Rotation du boomerang pendant le lancer
            // transform.Rotate(Vector3.forward, rotationSpeed * Time.fixedDeltaTime * 1000f, Space.Self);

            // Si le boomerang a parcouru la distance maximale, il commence à revenir
            if (Vector3.Distance(transform.position, startPosition) > distance)
            {
                returning = true;
            }
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == playerOwner)
        {
            Destroy(this.gameObject);
        }
        else if (col.gameObject.CompareTag("Player") && col.gameObject != playerOwner && col.gameObject.GetComponent<PlayerInputHandler>().whichTeam != playerOwner.GetComponent<PlayerInputHandler>().whichTeam)
        {
            col.gameObject.GetComponent<PlayerMovement>().isDead = true;
            GameManager.Instance.UpdateScore(playerOwner.GetComponent<PlayerInputHandler>().whichTeam);
            Destroy(this.gameObject);
        }

        else if(col.gameObject.CompareTag("Wall"))
        {
            if (activeBounce)
            {
                Vector3 normal = col.contacts[0].normal;
                var direction = Vector3.Reflect(transform.forward, normal);
                rb.AddForce(direction * bounceSpeed, ForceMode.Impulse);
                rb.velocity *= percentagePertVelocity;
                Destroy(this.gameObject, 5f);
                
            }
            else
            {
                /*
                // Calculate the force of the collision
                float collisionForce = col.impulse.magnitude;
                
                // Apply a force to the projectile in the opposite direction of the collision
                rb.AddForce(-col.impulse.normalized * collisionForce, ForceMode.VelocityChange);
                */
                
                // If shouldBounce is false, stick the projectile to the wall
                rb.constraints = RigidbodyConstraints.FreezeAll;
                Destroy(this.gameObject, 2f);
                
            }
            
        }
        else if(col.gameObject.CompareTag("Shuriken"))
        {
            Destroy(this.gameObject);
        }
    }
    
}
