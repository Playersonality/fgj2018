using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addSign : MonoBehaviour {

    public GameObject exitsign;

    private void OnMouseUp()
    {
        Debug.Log("Drag ended!");
        Instantiate(exitsign, position: transform.position, rotation: Quaternion.identity);
    }       
}
        
