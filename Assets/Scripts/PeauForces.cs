using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeauForces : MonoBehaviour
{
    public Peau peau;
    public PeauLineDraw draw;
    public SphereForce forceBallScript;

    public float K = 20;  //Ks de ressort 
    //formule de pytagore
    public float longueurRessortAvide1 = 1f;// horizontale et verticale
    public float longueurRessortAvide2 = 1.41f;// pour les diagonales    

    public float masse = 0.05f;  // masse des spheres
                                 //public float masse = 0.05f;  

    public float amortissementRessort = 0.05f;   //coefficient d'amortissement
    
    private Vector3 forceAmorti;

    //
    public GameObject ball;

    public Vector3 forceKora;
    public float coefficient = 5f;
    public Vector3 hapticForce;

    List<Vector3> spherePositionsList = new List<Vector3>();
   
    Vector3 HapticFeedbackForce(Vector3 actualP, Vector3 initialP)
    {      
        Vector3 displacement = actualP - initialP;
        Debug.Log(displacement);
        Vector3 averageDisplacement = displacement / (peau.colonnes * peau.lignes);
        
        hapticForce = -coefficient * averageDisplacement;
        Debug.Log("haptic "+ hapticForce);
        return hapticForce;

    }
    public void forceBall(int i, int j)
    {
        float distance = Vector3.Distance(ball.transform.position, peau.spheres[i, j].transform.position);       
       // Debug.Log("distance" + distance);

        if (distance < 0.5f)
        { 
            Debug.Log("Distance entre la balle et la peau : " + distance);
            Debug.Log("collision" + i + j);

            //ajouter la difference entre points et balle this ci dessous (best one)
            // peau.spheres[i, j].transform.position -= new Vector3(0f, distance, 0f);

            spherePositionsList.Add(peau.spheres[i, j].transform.position);

            Debug.Log("Position initiale : " + peau.spheres[i, j].transform.position);

            //the best one (same)
            peau.spheres[i, j].transform.position -= new Vector3(0f, distance, 0f);

            Debug.Log("Position actuelle: " + peau.spheres[i, j].transform.position);

            foreach (Vector3 position in spherePositionsList)
            {
                HapticFeedbackForce(peau.spheres[i, j].transform.position, position);
            }    

        }
    }

    public void CalculateForces(int i, int j)
    {
        peau.forces[i, j] = Vector3.zero;

        //calculer les forces des ressorts
        peau.forces[i, j] += ForceRessorts(i, j);       
    }

    //calculer les forces des ressorts
    Vector3 ForceRessorts(int i, int j)
    {
        Vector3 forceRessort = Vector3.zero;

        //type 1 : structural springs
        //springs linking [i,j] and [i+1,j] : point lié aux 2 voisins horiz

        if (i > 0)
        {
            forceRessort += CalculateforceRessort(i, j, i - 1, j, longueurRessortAvide1, K, amortissementRessort);
             draw.DrawSpring(i, j, i - 1, j);
        }
        if (i < peau.colonnes - 1)
        {
            forceRessort += CalculateforceRessort(i, j, i + 1, j, longueurRessortAvide1, K, amortissementRessort);
             draw.DrawSpring(i, j, i + 1, j);
        }
        // springs linking [i,j] and [i,j+1] : liés aux 2 voisins verticales
        if (j > 0)
        {
            forceRessort += CalculateforceRessort(i, j, i, j - 1, longueurRessortAvide1, K, amortissementRessort);
             draw.DrawSpring(i, j, i, j - 1);
        }
        if (j < peau.lignes - 1)
        {
            forceRessort += CalculateforceRessort(i, j, i, j + 1, longueurRessortAvide1, K, amortissementRessort);
             draw.DrawSpring(i, j, i, j + 1);
        }

        //les diagonales 
        //diagonale 1
        if (j > 0 && i > 0 && (i % 2 == 0) && !(j % 2 == 0))
        {
            forceRessort += CalculateforceRessort(i, j, i - 1, j - 1, longueurRessortAvide2, K, amortissementRessort);
            draw.DrawSpring(i, j, i - 1, j - 1);
        } 
        //diagonale 2
        if (j < peau.lignes - 1 && i > 0 && (i % 2 == 0) && !(j % 2 == 0))
        {
            forceRessort += CalculateforceRessort(i, j, i - 1, j + 1, longueurRessortAvide2, K, amortissementRessort);
             draw.DrawSpring(i, j, i - 1, j + 1);
        }
        //diagonale 3
        if (j > 0 && (i < peau.colonnes - 1) && (i % 2 == 0) && !(j % 2 == 0))
        {
            forceRessort += CalculateforceRessort(i, j, i + 1, j - 1, longueurRessortAvide2, K, amortissementRessort);
            draw.DrawSpring(i, j, i + 1, j - 1);
        }
        //diagonale 4
        if (j < peau.lignes - 1 && i < peau.colonnes - 1 && (i % 2 == 0) && !(j % 2 == 0))
        {
            forceRessort += CalculateforceRessort(i, j, i + 1, j + 1, longueurRessortAvide2, K, amortissementRessort);
            draw.DrawSpring(i, j, i + 1, j + 1);
        }


        return forceRessort;
    }

    Vector3 CalculateforceRessort(int x1, int x2, int x3, int x4, float longueurRessortAvide, float Ks, float amortissementRessort)
    {
        Vector3 vecteurDistance = peau.spheres[x3, x4].transform.position - peau.spheres[x1, x2].transform.position;

        float distance = vecteurDistance.magnitude;

        Vector3 direction = vecteurDistance.normalized;

        // Loi de hook (force de rappel de ressort)
        Vector3 tensionRessort = Ks * (distance - longueurRessortAvide) * direction;


        forceAmorti = -amortissementRessort * peau.vitesses[x1, x2]; // viscous damping F = -C * v(i,j)

        tensionRessort += forceAmorti;

        return tensionRessort;
    }
}
