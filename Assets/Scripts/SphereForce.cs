using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereForce : MonoBehaviour
{/*
    //private Vector3 gravity;
    public float amortissement = 0.05f;
    SphereMovement ballScript;

    public Vector3 forcePousse = new Vector3(0, -0.05f, 0);
    private Vector3 forces;

    void Start()
    {
        forces = Vector3.zero;
        ballScript = GetComponent<SphereMovement>();
    }

    public Vector3 Force()
    {
        forces += forcePousse;

        // Debug.Log(forces);
        if(ballScript.vitesse != null)
        {

            forces += -amortissement * ballScript.vitesse;

            Debug.Log(forces);
            Debug.Log(-amortissement * ballScript.vitesse);


        }
      
        return forces;
    }*/

    public Vector3 force = new Vector3(0f, -0.2f, 0f);
    public float forceReductionRate = 0.1f;
    public float minY = -1f;
    public float maxY = 1f;

   

    public Vector3 Force()
    {
        // Appliquer la force
        transform.Translate(force * Time.deltaTime);

        // Réduire progressivement la force
        force *= 1 - forceReductionRate * Time.deltaTime;

        // Arrêter la force lorsque la magnitude devient suffisamment petite
        if (force.magnitude < 0.01f)
        {
            force = Vector3.zero;
        }

        return force;
    }
}
