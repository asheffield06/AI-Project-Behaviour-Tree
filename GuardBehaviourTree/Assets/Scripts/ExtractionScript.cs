using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        CanvasController.instance.gameText.text = "Well done, you were extracted with the loot";

        PlayerController.instance.movementLocked = true;
        PlayerController.instance.rb.useGravity = false;
        PlayerController.instance.rb.velocity = new Vector3(0, 2f, 0);
    }
}
