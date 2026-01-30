using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Slider _slider;
    private Image _icon;
    private TextMeshProUGUI _name;

    private void Start()
    {
        var images = GetComponentsInChildren<Image>();
        _icon = images.First(x => x.name == "Icon");
        var textChildren = GetComponentsInChildren<TextMeshProUGUI>();
        _name = textChildren.First(x => x.name == "Name");
        _slider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
        _name.text = EncounterManager.Instance.GetPlayer().name;
        var masque = EncounterManager.Instance.GetPlayer().GetActiveMasque();
        _icon.sprite = masque.icon;
        _slider.maxValue = masque.maxDurability;
        _slider.value = masque.durability;
    }
}