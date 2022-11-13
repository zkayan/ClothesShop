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
        updateSellClothes();
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
        dialogue.CloseDialogue();
        playerController.SetDisableMoviment(true);
    }

    public void updateSellClothes()
    {
        foreach (Transform child in sellContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (SO_BodyPart cloth in inventory.clothes)
        {
            AddClothSell(cloth);
        }
    }

    private void AddClothShop(SO_BodyPart cloth, GameObject content)
    {
        _newItem = Instantiate(itemPrefab, content.transform.position, Quaternion.identity);
        _newItem.GetComponentInChildren<TextMeshProUGUI>().text = "$" + cloth.ClothPrice.ToString();
        _newItem.transform.Find("Icon").GetComponent<Image>().sprite = cloth.Icon;
        _newItem.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyCloth(cloth); });
        _newItem.transform.SetParent(content.transform, true);
    }

    private void AddClothSell(SO_BodyPart cloth)
    {
        _newItem = Instantiate(itemPrefab, sellContent.transform.position, Quaternion.identity);
        _newItem.GetComponentInChildren<TextMeshProUGUI>().text = "$" + cloth.ClothPrice.ToString();
        _newItem.transform.Find("Icon").GetComponent<Image>().sprite = cloth.Icon;
        _newItem.GetComponentInChildren<Button>().onClick.AddListener(delegate { SellCloth(cloth); });
        _newItem.transform.SetParent(sellContent.transform, true);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        playerController.SetDisableMoviment(false);
    }

    public void BuyCloth(SO_BodyPart cloth)
    {
        if (inventory.money >= cloth.ClothPrice)
        {
            inventory.money -= cloth.ClothPrice;
            inventory.clothes.Add(cloth);

            inventory.AddClothUI(cloth);
            updateSellClothes();
        }
    }

    public void SellCloth(SO_BodyPart cloth)
    {
        for (int i = 0; i < inventory.clothes.Count; i++)
        {
            if (inventory.clothes[i] == cloth)
            {
                inventory.clothes.RemoveAt(i);
                inventory.money += cloth.ClothPrice * 0.7f;
                updateSellClothes();
                return;
            }
        }
    }
}
