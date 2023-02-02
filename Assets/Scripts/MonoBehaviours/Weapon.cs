using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Reference to ammo prefab
    public GameObject ammoPrefab;

    //Velocity of ammo fired 
    public float weaponVelocity;

    //Ammo pool; only one copy will exist
    static List<GameObject> ammoPool;

    //Number of objects to place in pool
    public int poolSize;

    //Defines Maximum Distance
    public float maxDistance;
    
    
    // Called when script is being loaded
    void Awake()
    {
        if (ammoPool == null)
        {
            //Create pool
            ammoPool = new List<GameObject>();
        }

        //Loop for adding created ammo objects to pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    //Is called when gameObject is destroyed
    private void OnDestroy()
    {
        ammoPool = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see if player has clicked mouse to fire magic orb
        //Parameter 0 checks for left mouse click, 1 checks for right click
        if (Input.GetMouseButtonDown(0))
        {
            FireAmmo();
        }
    }

    //Retrieves and returns activated AmmoObject from object pool
    //Location is where to place AmmoObject retrieved
    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            //Checks to examine if current objective is inactive
            if (ammo.activeSelf == false)
            {
                //Activate ammo
                ammo.SetActive(true);

                //Change position
                ammo.transform.position = location;

                return ammo;
            }
        }

        //Returns null if all objects in pool are currently being used
        return null;
    }

    //Moves AmmoObject from spawn location to where mouse was clicked
    private void FireAmmo()
    {
        //Converts mouse position from screen space to game world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Calculate the distance between the weapon and the mouse position
        float distance = Vector3.Distance(transform.position, mousePosition);
        Debug.Log(distance);
        //Only fire the ammo if the distance is less than or equal to the maximum distance
        if (distance <= maxDistance)
        {
            //Get new ammo object from weapon current position
            GameObject ammo = SpawnAmmo(transform.position);

            //Ensure player has ammo
            if (ammo != null)
            {
                //Receive reference to arc script
                Arc arcScript = ammo.GetComponent<Arc>();

                //Calculates travel time for ammo
                float travelDuration = 1.0f / weaponVelocity;

                StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration));
            }
        }
    }
}
