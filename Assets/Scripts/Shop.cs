using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Dialogue dialogue;
    public PlayerController playerController;

    public void OpenShop()
    {
        gameObject.SetActive(true);
        dialogue.CloseDialogue();
        playerController.SetDisableMoviment(true);
    }
    
    public void CloseShop()
    {
        gameObject.SetActive(false);
        playerController.SetDisableMoviment(false);
    }
}
