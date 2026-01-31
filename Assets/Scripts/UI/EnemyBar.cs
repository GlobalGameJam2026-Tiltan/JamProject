using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBar : MonoBehaviour
{
    private Slider _slider;
    private Image _icon;
    private TextMeshProUGUI _name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        _name.text = EncounterManager.Instance.GetEnemy().name;
        var data = EncounterManager.Instance.GetEnemy().GetData();
        _icon.sprite = data.icon;
        _slider.maxValue = data.maxHealth;
        _slider.value = data.health;
    }
}
