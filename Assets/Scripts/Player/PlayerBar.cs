using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private PlayerCombat player;
    [SerializeField] private Sprite strengthSprite;
    [SerializeField] private Sprite intelligenceSprite;
    [SerializeField] private Sprite charismaSprite;
    [SerializeField] private Sprite upgradedStrengthSprite;
    [SerializeField] private Sprite upgradedIntelligenceSprite;
    [SerializeField] private Sprite upgradedCharismaSprite;
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
        _name.text = player.name;
        // player.GetActiveMasque().
        // _icon.sprite = player.GetActiveMasque().type switch
        // {
        //     MasqueType.Strength => strengthSprite,
        //     MasqueType.Intelligence => intelligenceSprite,
        //     MasqueType.Charisma => charismaSprite,
        //     _ => _icon.sprite
        // };
    }
}
