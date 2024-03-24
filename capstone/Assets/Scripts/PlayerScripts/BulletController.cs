using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //[SerializeField] private GameObject bulletDecal;    // Bullet hole

    private float speed = 50f;
    private float timeToDestroy = 2f;   // Timer before bullet is destroyed

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // If no target and close to "target", destroy
        if (!hit && Vector3.Distance(transform.position, target) < .01f)
        {
            Destroy(gameObject);
        }

        // If there is a target
    }

    private void OnCollisionEnter(Collision collision)
    {
        // For bullet holes (2 lines of code)
        ContactPoint contact = collision.GetContact(0);
        // arg 2: point + 
        //GameObject.Instantiate(bulletDecal, contact.point, Quaternion.LookRotation(contact.normal));

        Destroy(gameObject);
    }
}
