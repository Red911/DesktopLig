using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorjectileKill : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    
    /* fonction appelé quand l'objet avec le script collisions un autre gameobject */
    private void OnCollisionEnter(Collision col) 
    {
        // quand la collision se fait cela vérifie si le gameobject collisionner au tag Player
        if (col.gameObject.CompareTag("Player")) 
        {
            // Si le gameobject collisionné a bien le tag Player alors le script rentré dans la condition if
             // Appelé une fonction avec pour paramètre l'objet collisionné donc le joueur
        }
    }

   

   
}
