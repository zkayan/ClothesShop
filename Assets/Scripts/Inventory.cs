using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float money = 100f;
    public List<SO_BodyPart> clothes = new List<SO_BodyPart>();
    public SO_BodyPart test;

    public GameObject content;
    public GameObject panelPrefab;
    private GameObject _newPanel;

    private ChangeClothes _changeClothes;

    public GameObject inventoryPanel;

    private void Start()
    {
        _changeClothes = GetComponent<ChangeClothes>();
        GetAllClothes();
    }

    public void GetAllClothes()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (SO_BodyPart cloth in clothes)
        {
            AddClothUI(cloth);
        }
    }

    public void AddClothUI(SO_BodyPart cloth)
    {
        _newPanel = Instantiate(panelPrefab, content.transform.position, Quaternion.identity);
        _newPanel.GetComponentInChildren<TextMeshProUGUI>().text = cloth.ClothName;
        _newPanel.GetComponentInChildren<Image>().sprite = cloth.Icon;
        _newPanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { _changeClothes.Change(cloth); });
        _newPanel.transform.SetParent(content.transform, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            BuyCloth(test);
        }
        if (Input.GetKeyDown("p"))
        {
            SellCloth(test);
        }
        if (Input.GetKeyDown("i"))
        {
            GetAllClothes();
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void BuyCloth(SO_BodyPart cloth)
    {
        if(money >= cloth.ClothPrice)
        {
            money -= cloth.ClothPrice;
            clothes.Add(cloth);

            AddClothUI(cloth);
        }
    }

    public void SellCloth(SO_BodyPart cloth)
    {
        for(int i = 0; i < clothes.Count; i++)
        {
            if(clothes[i] == cloth)
            {
                clothes.RemoveAt(i);
                money += cloth.ClothPrice * 0.7f;
                return;
            }
        }
    }

}
