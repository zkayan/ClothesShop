using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Dialogue dialogue;
    public PlayerController playerController;
    public SO_Clothes clothes;
    public GameObject topsContent;
    public GameObject bottomsContent;
    public GameObject shoesContent;
    public GameObject sellContent;
    public GameObject itemPrefab;
    private GameObject _newItem;
    public Inventory inventory;

    private void Start()
    {
        GetAllClothes();
    }

    public void GetAllClothes()
    {
        foreach (Clothes clothType in clothes.clothesTypes)
        {
            if (clothType.bodyPartType == "Top")
            {
                foreach (SO_BodyPart cloth in clothType.clothes)
                {
                    AddClothShop(cloth, topsContent);
                }
            }
            if (clothType.bodyPartType == "Bottom")
            {
                foreach (SO_BodyPart cloth in clothType.clothes)
                {
                    AddClothShop(cloth, bottomsContent);
                }
            }
            if (clothType.bodyPartType == "Shoes")
            {
                foreach (SO_BodyPart cloth in clothType.clothes)
                {
                    AddClothShop(cloth, shoesContent);
                }
            }
        }
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
        dialogue.CloseDialogue();
        playerController.SetDisableMoviment(true);
    }

    public void AddClothShop(SO_BodyPart cloth, GameObject content)
    {
        _newItem = Instantiate(itemPrefab, content.transform.position, Quaternion.identity);
        _newItem.GetComponentInChildren<TextMeshProUGUI>().text = "$" + cloth.ClothPrice.ToString();
        _newItem.transform.Find("Icon").GetComponent<Image>().sprite = cloth.Icon;
        //_newItem.GetComponentInChildren<Image>().sprite = cloth.Icon;
        _newItem.GetComponentInChildren<Button>().onClick.AddListener(delegate { inventory.BuyCloth(cloth); });
        _newItem.transform.SetParent(content.transform, true);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        playerController.SetDisableMoviment(false);
    }
}
