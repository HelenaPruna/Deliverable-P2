using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; // 1
    public float gotHayDestroyDelay; // 2
    private bool hitByHay; // 3


    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    private SheepSpawner sheepSpawner;


    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        hitByHay = true; // 1
        runSpeed = 0; // 2

        Destroy(gameObject, gotHayDestroyDelay); // 3
        sheepSpawner.RemoveSheepFromList(gameObject);

    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }


    private void OnTriggerEnter(Collider other) // 1
    {
        if (other.CompareTag("Hay") && !hitByHay) // 2
        {
            Destroy(other.gameObject); // 3
            HitByHay(); // 4
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    private void Drop()
    {
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);
        sheepSpawner.RemoveSheepFromList(gameObject);

    }


}
