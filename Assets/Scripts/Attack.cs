using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject ammo;
    [SerializeField] private bool isPlayer = false;

    private int ammoCount = 0;
    private Transform fireTransform;
    private float fireRate = 0.5f;
    private AudioClip clipToPlay;
    private AudioSource audioSource;
    private int maxAmmoCount = 5;


    private float currentFireRate = 0f;

    private GameManager gameManager;
    public AudioClip GetClipToPlay
    {
        get { return clipToPlay; }
        set { clipToPlay = value; }
    }
    public float GetCurrentFireRate
    {
        get
        {
            return currentFireRate;
        }
        set
        {
            currentFireRate = value;
        }
    }

    public int GetAmmo
    {
        get
        {
            return ammoCount;
        }
        set
        {
            ammoCount = value;
            if (ammoCount > maxAmmoCount) ammoCount = maxAmmoCount;
        }
    }


    public int GetClipSize
    {
        get
        {
            return maxAmmoCount;
        }
        set
        {
            maxAmmoCount = value;
        }
    }

    public Transform GetFireTransform
    {
        get { return fireTransform; }
        set { fireTransform = value;}
    }

    public float GetFireRate
    {
        get { return fireRate; } set { fireRate = value; }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        //ammoCount = maxAmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFireRate > 0f)
        {
            currentFireRate -= Time.deltaTime;
        }
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (isPlayer && !gameManager.GetlevelFinish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentFireRate <= 0 && ammoCount > 0)
                {
                    Fire();
                }
            }

            switch (Input.inputString)
            {
                case "1":
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (i == 0)
                        {
                            weapons[1].gameObject.GetComponent<Weapon>().GetCurrentWeaponAmmoCount = ammoCount;
                            weapons[0].gameObject.SetActive(true);
                            continue;
                        }
                        weapons[i].gameObject.SetActive(false);
                    }

                    break;
                case "2":
                    weapons[1].gameObject.GetComponent<Weapon>().GetCurrentWeaponAmmoCount = ammoCount;
                    weapons[0].gameObject.SetActive(false);
                    weapons[1].gameObject.SetActive(true);
                    break;

            }
        }
    }

    public void Fire()
    {
        float difference = 180f - transform.eulerAngles.y;
        float targetRotation = -90f;

        if (difference >= 90f)
        {
            targetRotation = -90f;
        }
        else if (difference <= 90f)
        {
            targetRotation = 90f;
        }
        ammoCount--;
        currentFireRate = fireRate;
        audioSource.PlayOneShot(clipToPlay);
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation));
        bulletClone.GetComponent<Bullet>().owner = gameObject;

    }
}
