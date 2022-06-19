using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyself : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroy",0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void destroy()
    {
        Destroy(gameObject);
    }
}
