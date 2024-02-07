using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeauLineDraw : MonoBehaviour
{
    public Peau peau;

    public void DrawSpring(int i1, int j1, int i2, int j2)
    {
        Transform sphere1 = peau.spheres[i1, j1].transform;
        Transform sphere2 = peau.spheres[i2, j2].transform;

        Color color = Color.black;

        Debug.DrawLine(sphere1.position, sphere2.position, color);


    }
}
