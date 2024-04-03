using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //[SerializeField] private GameObject bulletDecal;    // Bullet hole

    private float speed = 50f;
    private float timeToDestroy = 1f;   // Timer before bullet is destroyed

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

        //Debug.Log("before destroy");

        // If no target and close enough to "target", destroy
        if (!hit && Vector3.Distance(transform.position, target) < .01f)
        {
            Destroy(gameObject);
            Debug.Log("destroy bullet");
        }

        // If there is a target
    }

    /* NEVER TRIGGERED, as this is bullet controller, not BULLET 
     OR because the collision needs to be with a rigidbody!!!
    
     most likely the second reason*/
    private void OnCollisionEnter(Collision collision)
    {
        // For bullet holes (2 lines of code)
        ContactPoint contact = collision.GetContact(0);
        // arg 2: point + slightly above surface of collided object
        //GameObject.Instantiate(bulletDecal, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal));
        Debug.Log("on collision");
        Destroy(gameObject);
    }
}