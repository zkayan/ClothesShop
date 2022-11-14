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
                foreach (SO_Cloth cloth in clothType.clothes)
                {
                    AddClothShop(cloth, topsContent);
                }
            }
            if (clothType.bodyPartType == "Bottom")
            {
                foreach (SO_Cloth cloth in clothType.clothes)
                {
                    AddClothShop(cloth, bottomsContent);
                }
            }
            if (clothType.bodyPartType == "Shoes")
            {
                foreach (SO_Cloth cloth in clothType.clothes)
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
        inventory.CloseInventory();
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        playerController.SetDisableMoviment(false);
    }

    public void updateSellClothes()
    {
        foreach (Transform child in sellContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (SO_Cloth cloth in inventory.clothes)
        {
            AddClothSell(cloth);
        }
    }

    private void AddClothShop(SO_Cloth cloth, GameObject content)
    {
        if (!inventory.clothes.Find(inventoryCloth => inventoryCloth.ClothName == cloth.ClothName))
        {
            _newItem = Instantiate(itemPrefab, content.transform.position, Quaternion.identity);
            _newItem.GetComponentInChildren<TextMeshProUGUI>().text = "$" + cloth.ClothPrice.ToString("N2");
            _newItem.name = cloth.ClothName;
            _newItem.transform.Find("Icon").GetComponent<Image>().sprite = cloth.Icon;
            _newItem.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyCloth(cloth); });
            _newItem.transform.SetParent(content.transform, true);
        }
    }

    private void AddClothSell(SO_Cloth cloth)
    {
        if (!inventory.equipedClothes.Find(eqCloth => eqCloth.ClothName == cloth.ClothName))
        {
            _newItem = Instantiate(itemPrefab, sellContent.transform.position, Quaternion.identity);
            _newItem.GetComponentInChildren<TextMeshProUGUI>().text = "$" + (cloth.ClothPrice * 0.7f).ToString("N2");
            _newItem.name = cloth.ClothName;
            _newItem.transform.Find("Icon").GetComponent<Image>().sprite = cloth.Icon;
            _newItem.GetComponentInChildren<Button>().onClick.AddListener(delegate { SellCloth(cloth); });
            _newItem.transform.SetParent(sellContent.transform, true);
        }
    }
    
    public void RemoveItem(string itemName)
    {
        if (topsContent.transform.Find(itemName))
        {
            GameObject.Destroy(topsContent.transform.Find(itemName).gameObject);
        }
        else if (bottomsContent.transform.Find(itemName))
        {
            GameObject.Destroy(bottomsContent.transform.Find(itemName).gameObject);
        }
        else if (shoesContent.transform.Find(itemName))
        {
            GameObject.Destroy(shoesContent.transform.Find(itemName).gameObject);
        }
        else if (sellContent.transform.Find(itemName))
        {
            GameObject.Destroy(sellContent.transform.Find(itemName).gameObject);
        }
    }

    public void AddItem(SO_Cloth cloth)
    {
        if (cloth.bodyPartType == "Top")
        {
            AddClothShop(cloth, topsContent);
        }
        else if (cloth.bodyPartType == "Bottom")
        {
            AddClothShop(cloth, bottomsContent);
        }
        else if (cloth.bodyPartType == "Shoes")
        {
            AddClothShop(cloth, shoesContent);
        }
    }

    public void BuyCloth(SO_Cloth cloth)
    {
        if (inventory.money >= cloth.ClothPrice)
        {
            inventory.money -= cloth.ClothPrice;
            inventory.UpdateUiMoney(inventory.money);
            inventory.clothes.Add(cloth);

            inventory.AddClothUI(cloth);
            RemoveItem(cloth.ClothName);
            updateSellClothes();
        }
    }

    public void SellCloth(SO_Cloth cloth)
    {
        for (int i = 0; i < inventory.clothes.Count; i++)
        {
            if (inventory.clothes[i] == cloth)
            {
                inventory.clothes.RemoveAt(i);
                inventory.money += cloth.ClothPrice * 0.7f;
                inventory.UpdateUiMoney(inventory.money);
                AddItem(cloth);
                updateSellClothes();
            }
        }
    }
}
