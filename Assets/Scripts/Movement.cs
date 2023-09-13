using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 13f;
    [SerializeField] private float trunSepped = 15f;
    [SerializeField] private Transform[] rayStartPoints;
    private GameManager gameManager;

    // Start is called before the first frame update

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetlevelFinish)
        {
            TakeInput();
        }
    }
     
    private void TakeInput()
    {
       if(Input.GetKeyDown(KeyCode.Space) && OnGroundCheck())
       {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp((jumpPower * 100) * Time.deltaTime,0f ,15f), 0f);
            print("jump");
       }

       if (Input.GetKey(KeyCode.A))
       {
           // rigidbody.velocity.y olmasýnýn sebebi hareket ederken ayný zamanda zýplayabilmek için
           rigidbody.velocity = new Vector3(Mathf.Clamp((speed *100) * Time.deltaTime,0f,5f), rigidbody.velocity.y, 0f);
           //transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            //yumuþak dönüþ için
           transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0f,0f,0f), trunSepped * Time.deltaTime);
       }

       else if (Input.GetKey(KeyCode.D))
       {
           rigidbody.velocity = new Vector3(Mathf.Clamp((-speed *100) * Time.deltaTime,-5f, 0f), rigidbody.velocity. y, 0f);
           //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 180f, 0f), trunSepped * Time.deltaTime);
       }

       else
       {
            rigidbody.velocity = new Vector3(0f,rigidbody.velocity.y, 0f);
       }
    }

    private bool OnGroundCheck()
    {
        bool hit = false;
        for (int i = 0; i < rayStartPoints.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoints[i].position, -rayStartPoints[i].transform.up, 0.50f);
            Debug.DrawRay(rayStartPoints[i].position, -rayStartPoints[i].transform.up * 0.50f, Color.red);
        }
        
        if(hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
