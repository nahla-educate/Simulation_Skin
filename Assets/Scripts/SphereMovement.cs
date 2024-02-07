using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public Vector3 vitesse;
    public Vector3 acceleration;
    public SphereForce forceScript;
    public bool enableForce = false;
    public float masse = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        vitesse = Vector3.zero;
        acceleration = Vector3.zero;
    }
    void Update()
    {
        if(enableForce == true)
        {
            Lancer();
        }

    }
    public void enableVar()
    {
        enableForce = true;
    }    
    public void Lancer()
    {
        //Euler method
        float deltaT = Time.deltaTime;

        //loi fondamentale de la dynamique F = m*a
        //calcul d'acceleration : a = Forces / m
        acceleration = (forceScript.Force())/ masse;

        //mettre à jour vitesse = vitesse + acc * deltaTime
        vitesse += acceleration * deltaT;

        //mettre à jour position = position + vitesse * deltaTime
        transform.position += vitesse * deltaT;

    }
}
