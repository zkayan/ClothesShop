using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float money = 100f;
    public List<SO_Cloth> clothes = new List<SO_Cloth>();

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
        _newPanel = Instantiate(panelPrefab, content.transform.position, Quaternion.identity);
        _newPanel.transform.Find("Icon").GetComponent<Image>().sprite = cloth.Icon;
        _newPanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { _changeClothes.Change(cloth); });
        _newPanel.transform.SetParent(content.transform, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            GetAllClothes();
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            shop.CloseShop();
        }
    }

    public void CloseInventory()
    {
            inventoryPanel.SetActive(false);
    }
}
