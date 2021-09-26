using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchCity : MonoBehaviour
{
    [SerializeField]
    private Dropdown[] drops;
    private void Awake()
    {
        foreach (var item in ParsingCityCodeXML.m_CityCode)
        {
            drops[0].options.Add(new Dropdown.OptionData(item.Key));
        }
        drops[0].onValueChanged.AddListener((value) =>
        {
            StartCoroutine(Drops1SetList(value));
        });
        drops[1].onValueChanged.AddListener((value) =>
        {
            StartCoroutine(Drops2SetList(value));
        });
    }
    private void OnEnable()
    {
        drops[1].ClearOptions();
        drops[1].options.Add(new Dropdown.OptionData("未选择"));
        drops[2].ClearOptions();
        drops[2].options.Add(new Dropdown.OptionData("未选择"));
        drops[0].value = 0;
        drops[1].value = 0;
        drops[2].value = 0;
    }
    private IEnumerator Drops1SetList(int value)
    {
        drops[1].ClearOptions();
        drops[1].options.Add(new Dropdown.OptionData("未选择"));
        string name = drops[0].options[value].text;
        yield return null;
        if (value == 0)
        {
            drops[1].value = 0;
            yield break; 
        }
        if (ParsingCityCodeXML.m_CityCode.ContainsKey(name))
        {
            foreach (var item in ParsingCityCodeXML.m_CityCode[name])
            {
                drops[1].options.Add(new Dropdown.OptionData(item.Key));
            }
        }
        drops[1].value = 0;
        drops[1].transform.GetChild(0).GetComponent<Text>().text = "未选择";
        drops[2].transform.GetChild(0).GetComponent<Text>().text = "未选择";
    }
    private IEnumerator Drops2SetList(int value)
    {
        drops[2].ClearOptions();
        drops[2].options.Add(new Dropdown.OptionData("未选择"));
        string name = drops[0].options[drops[0].value].text;
        string name1 = drops[1].options[value].text;
        yield return null;
        if (value == 0)
        {
            drops[2].value = 0;
            yield break;
        }
        if (ParsingCityCodeXML.m_CityCode[name].ContainsKey(name1))
        {
            foreach (var item in ParsingCityCodeXML.m_CityCode[name][name1])
            {
                drops[2].options.Add(new Dropdown.OptionData(item.Key));
            }
        }
        drops[2].value = 0;
        drops[2].transform.GetChild(0).GetComponent<Text>().text = "未选择";
    }
    public void OnClickSwitchCity()
    {
        if(drops[2].value == 0)
        {
            UISystem.GetInstance().ShowTip(ShowTipType.OK , "没有选择地区");
            return;
        }
        Main.instance.SetCityCode(drops[0].options[drops[0].value].text , 
        drops[1].options[drops[1].value].text , drops[2].options[drops[2].value].text);
        OnClickClose();
    }
    public void OnClickClose()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
