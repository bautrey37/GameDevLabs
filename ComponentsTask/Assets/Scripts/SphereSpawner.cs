using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereSpawner : MonoBehaviour
{

    public int SphereCounter;
    public Text SphereCounterUI;

    public CircleMovement SpherePrefab;
    public CircleMovement[] Spheres;

	// Use this for initialization
	void Start ()
	{
	    SphereCounterUI.text = SphereCounter.ToString();
        SpawnSpheres(SphereCounter);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnSpheres(int count)
    {
        if (count < 1)
            return;

        Spheres = new CircleMovement[count];
        //Todo Spawn the required amount of spheres
        //Distance between spheres should be uniform.

        //Instantiates a prefab, sets parent of new object to transform and repositions the obj based on parents location.
        //Spheres[0] = Instantiate(SpherePrefab, transform, false);
        for (int i = Spheres.Length - 1; i >= 0; i--)
        {
            Spheres[i] = Instantiate(SpherePrefab, transform, false);
            Debug.Log(string.Format("Spawn sphere {0}"));
        }
    }

    public void ClearSpheres()
    {
        if(Spheres == null || Spheres.Length < 1)
            return;

        for (int i = Spheres.Length - 1; i >= 0; i--)
        {
            if(Spheres[i] != null)
                Destroy(Spheres[i].gameObject);
        }
        Spheres = null;
    }

    public void AddSphere()
    {
        print("adding");
        ClearSpheres();
        SphereCounter = Mathf.Min(++SphereCounter, 64);
        SpawnSpheres(SphereCounter);
        SphereCounterUI.text = SphereCounter.ToString();
    }

    public void RemoveSphere()
    {
        ClearSpheres();
        SphereCounter = Mathf.Max(--SphereCounter, 0);
        SpawnSpheres(SphereCounter);
        SphereCounterUI.text = SphereCounter.ToString();
    }
}
