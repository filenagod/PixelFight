using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;
    [Header("Health Settings")]
    public bool healthPowerUp = false;
    public int healthAmount = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp = false;
    public int ammoAmount = 5;
    [Header("Transform Settings")]
    [SerializeField] private Vector3 trunVector = Vector3.zero;
    [Header("Scale Ssettings")]
    [SerializeField] private float period = 2f;
    [SerializeField] Vector3 scaleVecctor;
    [SerializeField] private float scaleFactor;
    private Vector3 startScale;
    // Start is called before the first frame update

    private void Awake()
    {
        startScale = transform.localScale;
        //clipToPlay = GetComponent<AudioSource>();
    }
    void Start()
    {
        // Aþaðýdaki kod objenin büyüklüðünü tutuyor
        
        if(healthPowerUp && ammoPowerUp)
        {
            healthPowerUp = false;
            ammoPowerUp=false;
        }
        else if (healthPowerUp)
        {
            ammoPowerUp = false;
        }
        else if(ammoPowerUp)
        {
            healthPowerUp = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(trunVector);
        SinusWawe();
    }

    private void SinusWawe()
    {
        //bu metotta ki kodu merminin büyüyüp küçülebilmesi için yazdým
        if(period <= 0)
        {
            period = 0.1f;
        }
        float cycles = Time.timeSinceLevelLoad / period;

        //const ile atanan deðer oyun boyunca deðiþmez
        const float  piX2 = Mathf.PI * 2;

        float sinusWare = Mathf.Sin( cycles * piX2);

        //0 ile 1 arasýnda deðer döndürmek için yapýlan matematik iþlemi
        scaleFactor = sinusWare / 2 + 0.5f;

        Vector3 offset = scaleFactor * scaleVecctor;

        transform.localScale = startScale + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }

        AudioSource.PlayClipAtPoint(clipToPlay, transform.position);

        if (healthPowerUp)
        {
            other.gameObject.GetComponent<Target>().GetHealth += healthAmount;
        }
        else if (ammoPowerUp) 
        {
            other.gameObject.GetComponent<Attack>().GetAmmo += ammoAmount;
        }

        Destroy(gameObject);
    }
}
