using UnityEngine;

public class Top : MonoBehaviour
{
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider other)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }
}
