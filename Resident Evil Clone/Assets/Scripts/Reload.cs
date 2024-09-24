using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [SerializeField] protected int currentSpareAmmo;
    [SerializeField] protected int ammoCapacity;
    [SerializeField] protected int currentLoadedAmmo;
    [SerializeField] protected bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        if (currentLoadedAmmo < ammoCapacity) {
            if (currentSpareAmmo > 0) {
                int bulletsToLoad = ammoCapacity - currentLoadedAmmo;
                if (currentSpareAmmo >= bulletsToLoad) {
                    currentLoadedAmmo = ammoCapacity;
                    currentSpareAmmo -= bulletsToLoad;
                } else {
                    currentLoadedAmmo += currentSpareAmmo;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
