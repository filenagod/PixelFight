using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float reloadTime = 5f;
    [SerializeField] private float shootRange = 10f ;
    [SerializeField] private LayerMask shootLayer;
    private Transform aimTransform;

    private bool canMoveRight = false;
    private bool isReloaded = false;
    private Attack attack;

    private void Awake()
    {
        attack = GetComponent<Attack>();
        aimTransform = attack.GetFireTransform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttack();

        CheckCanMoveRight();

        MoveTowards();

        Aim();
    }

    private void Reload()
    {
        attack.GetAmmo = attack.GetClipSize;
        isReloaded = false;
    }

    private void EnemyAttack()
    {
        if(attack.GetAmmo <= 0 && isReloaded == false)
        {
            Invoke(nameof(Reload), reloadTime);
            isReloaded = true;
        }
        // nameof yazýlan string ifadede hata almamak için kullanýlýr
        if (attack.GetCurrentFireRate <= 0 && attack.GetAmmo > 0 && Aim())
        {
            attack.Fire();
        }
    }

    private void MoveTowards()
    {
        if(Aim() && attack.GetAmmo > 0)
        {
            return;
        }

        if (!canMoveRight) //sol
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[0].position.x,
                transform.position.y, movePoints[0].position.z), speed * Time.deltaTime);
            LookAtTheTarget(movePoints[0].position);
        }
        else //sað
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[1].position.x,
                transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            LookAtTheTarget(movePoints[1].position );
        }
    }

    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position, new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z)) <= 0.1f)
        {
            canMoveRight = true;
        }
        else if (Vector3.Distance(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z)) <= 0.1f)
        {
            canMoveRight = false;
            print("Move Left");
        }
    }

    private bool Aim()
    {
        if(aimTransform == null)
        {
            aimTransform = attack.GetFireTransform;
        }

        bool hit = Physics.Raycast(aimTransform.position, transform.forward, shootRange, shootLayer);

        Debug.DrawRay(aimTransform.position, transform.forward * shootRange, Color.blue);
        return hit;
    }

    private void LookAtTheTarget(Vector3 newtarget)
    {
        Vector3 newLookPoisition = new Vector3(newtarget.x, transform.position.y, newtarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newLookPoisition - transform.position);


        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        
    }
}
