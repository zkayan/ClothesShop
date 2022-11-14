using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float money = 100f;
    public List<SO_Cloth> clothes = new List<SO_Cloth>();
    public List<SO_Cloth> equipedClothes = new List<SO_Cloth>();

    public GameObject content;
    public GameObject panelPrefab;
    public TextMeshProUGUI textMoney;
    private GameObject _newPanel;

    private ChangeClothes _changeClothes;

    public GameObject inventoryPanel;

    public Shop shop;

    private void Start()
    {
        _changeClothes = GetComponent<ChangeClothes>();
        foreach(SO_Cloth cloth in clothes)
        {
            equipedClothes.Add(cloth);
        }
        UpdateUiMoney(money);
        GetAllClothes();
    }

    public void GetAllClothes()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (SO_Cloth cloth in clothes)
        {
            AddClothUI(cloth);
        }
    }

    public void UpdateUiMoney(float value)
    {
        textMoney.text = "$" + value.ToString("N2");
    }

    public void AddClothUI(SO_Cloth cloth)
    {
        if (!equipedClothes.Find(eqCloth => eqCloth.ClothName == cloth.ClothName))
        {
            _newPanel = Instantiate(panelPrefab, content.transform.position, Quaternion.identity);
            _newPanel.transform.Find("Icon").GetComponent<Image>().sprite = cloth.Icon;
            _newPanel.name = cloth.ClothName;
            _newPanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { ChangeCloth(cloth); });
            _newPanel.transform.SetParent(content.transform, true);
        }
    }

    private void ChangeCloth(SO_Cloth cloth)
    {
        for (int i = 0; i < equipedClothes.Count; i++)
        {
            SO_Cloth oldCloath = equipedClothes[i];
            if (oldCloath.bodyPartType == cloth.bodyPartType)
            {
                equipedClothes.RemoveAt(i);
                equipedClothes.Add(cloth);
                AddClothUI(oldCloath);
                GameObject.Destroy(content.transform.Find(cloth.ClothName).gameObject);
                shop.updateSellClothes();
            }
        }

        _changeClothes.Change(cloth);
    }

    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            shop.CloseShop();
        }
    }

    public void CloseInventory()
    {
            inventoryPanel.SetActive(false);
    }
}
