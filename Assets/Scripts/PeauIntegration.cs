using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeauIntegration : MonoBehaviour
{
    public Peau peau;
    public PeauForces calculForces;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < peau.colonnes; i++)
        {
            for (int j = 0; j < peau.lignes; j++)
            {
                calculForces.CalculateForces(i, j);
                calculForces.forceBall(i, j);
            }
        }

        for (int i = 0; i < peau.colonnes; i++)
        {
            for (int j = 0; j < peau.lignes; j++)
            {

                AppliquerForces(i, j, peau.forces[i, j]);

            }
        }

    }

    void AppliquerForces(int i, int j, Vector3 force)
    {
        if (j == 0 || j == peau.lignes - 1 || i == 0 || i == peau.colonnes - 1)             
            return;

        //Euler method
        float deltaT = Time.deltaTime;

        //loi fondamentale de la dynamique F = m*a
        //calcul d'acceleration : a = F(i,j) / m
        Vector3 acceleration = peau.forces[i, j] / calculForces.masse;

        //mettre à jour vitesse = vitesse + acc * deltaTime
        peau.vitesses[i, j] += acceleration * deltaT;

        //mettre à jour position = position + vitesse * deltaTime
        peau.spheres[i, j].transform.position += peau.vitesses[i, j] * deltaT;
    }
}
