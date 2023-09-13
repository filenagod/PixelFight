using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Attack attack;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private float fireRate;
    [SerializeField] private int clipSize;
    [SerializeField] private AudioClip clip;
    private int currentAmmocount;

    public int GetCurrentWeaponAmmoCount
    {
        get 
        { 
            return currentAmmocount; 
        }
        set 
        { 
            currentAmmocount = value; 
        }
    }
    
    private void Awake()
    {
        currentAmmocount = clipSize;
    }
    void Start()
    {

    }

    
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if(attack != null)
        {
            attack.GetFireTransform = fireTransform;
            attack.GetClipSize = clipSize;
            attack.GetFireRate = fireRate;
            attack.GetAmmo = currentAmmocount;
            attack.GetClipToPlay = clip;
        }
    }
}
