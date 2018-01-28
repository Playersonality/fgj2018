using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSign : MonoBehaviour {

    public GameObject exitsign;

    
    public void CreateSign()
    {

        if (Input.GetKeyDown (KeyCode.Space))
        {
            Debug.Log(message: "space pressed");
            var mousePos = Input.mousePosition;
            Instantiate(exitsign, mousePos, Quaternion.identity);

        }
    
    }       
}
        
