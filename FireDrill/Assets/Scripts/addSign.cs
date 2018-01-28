using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSign : MonoBehaviour {

    public GameObject exitsign;

    
    public void CreateSign()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(message: "Sign Added!");
            var mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePositionInWorld.z = 0;
            Instantiate(exitsign, mousePositionInWorld, Quaternion.identity);

        }
    
    }       
}
        
