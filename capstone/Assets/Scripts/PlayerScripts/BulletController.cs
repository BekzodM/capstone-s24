using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //[SerializeField] private GameObject bulletDecal;    // Bullet hole

    [SerializeField] private float speed = 500f;
    [SerializeField] private float timeToDestroy = .1f;   // Timer before bullet is destroyed
    public int damage { get; set; }

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
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // For bullet holes (2 lines of code)
    //    ContactPoint contact = collision.GetContact(0);
    //    // arg 2: point + slightly above surface of collided object
    //    //GameObject.Instantiate(bulletDecal, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal));
    //    Debug.Log("on collision");
    //    Debug.Log(collision.gameObject.tag);

    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    //        Debug.Log("damage enemy");
    //    }

    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter(Collider collision)
    {
        // For bullet holes (2 lines of code)
        //ContactPoint contact = collision.GetContact(0);
        // arg 2: point + slightly above surface of collided object
        //GameObject.Instantiate(bulletDecal, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal));
        Debug.Log("on collision");
        Debug.Log(collision.gameObject.tag);
        Debug.Log(collision.gameObject.name);

        // Enemy script attached to root parent enemy object
        if (collision.transform.root.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            collision.transform.root.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("damage enemy");
            Destroy(gameObject);
        }

        //Destroy(gameObject);
    }
}
