using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<Player>().setCheckpoint(transform.position);
        }
    }
}
