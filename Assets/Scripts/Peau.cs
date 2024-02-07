using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peau : MonoBehaviour
{
    public GameObject spherePrefab;
    public int lignes = 10;  // lignes
    public int colonnes = 10;  // colonnes

    //initialiser matrices : spheres et vitesses
    public GameObject[,] spheres;
    public Vector3[,] forces;
    public Vector3[,] vitesses;

    public float espacement = 1.0f;  // espacement entre les spheres

    // Start is called before the first frame update
    void Start()
    {
        //construire matrice de spheres ( g�n�rer les sph�res )
        spheres = new GameObject[colonnes, lignes];
        vitesses = new Vector3[colonnes, lignes];
        forces = new Vector3[colonnes, lignes];

        for (int i = 0; i < colonnes; i++)
        {
            for (int j = 0; j < lignes; j++)
            {
                // j lignes  i colonnes
                Vector3 position = new Vector3(i * espacement, 0, j * espacement);
                spheres[i, j] = Instantiate(spherePrefab, position, Quaternion.identity);
                spheres[i, j].name = "Point [" + i + "," + j + "]";
            }
        }

    }
}
