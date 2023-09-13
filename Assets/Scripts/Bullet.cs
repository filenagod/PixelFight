 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Temas ettiði objede target scirpti yok ise
        if (other.gameObject.GetComponent<Target>() == false)
        {
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
