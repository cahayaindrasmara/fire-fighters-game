using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Fire fire = other.GetComponent<Fire>();
            if (fire != null)
            {
                fire.ReduceIntensity(0.5f * Time.deltaTime); // padamkan perlahan
                Debug.Log("kena api");
            }
        }
    }
}
