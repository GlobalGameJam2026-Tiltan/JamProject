using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Image _icon;
    private TextMeshProUGUI _name;

    void Start()
    {
        var images = GetComponentsInChildren<Image>();
        _icon = images.First(x => x.name == "Icon");
        var textChildren = GetComponentsInChildren<TextMeshProUGUI>();
        _name = textChildren.First(x => x.name == "Name");
    }

    // Update is called once per frame
    void Update()
    {
        _name.text = EncounterManager.Instance.GetPlayer().name;
        var masque = EncounterManager.Instance.GetPlayer().GetActiveMasque();
        _icon.sprite = masque.icon;
    }
}