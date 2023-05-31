using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public GameObject extraction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            extraction.SetActive(true);
            CanvasController.instance.gameText.text = "Take the loot back to the start!";
            PlayerController.instance.LootCollected = true;
            Destroy(gameObject);
        }
    }
}
