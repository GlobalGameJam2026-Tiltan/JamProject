using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button _button;
    [SerializeField] private AttackType attackType;
    [SerializeField] private Sprite strengthSprite;
    [SerializeField] private Sprite intelligenceSprite;
    [SerializeField] private Sprite charismaSprite;
    private Image _image;
    private TextMeshProUGUI _name;
    private TextMeshProUGUI _hitChance;
    private TextMeshProUGUI _damage;

    private void Start()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            switch (attackType)
            {
                case AttackType.Basic:
                    EncounterManager.Instance.GetPlayer().AttackBasic();
                    break;
                case AttackType.Medium:
                    EncounterManager.Instance.GetPlayer().AttackMedium();
                    break;
                case AttackType.Strong:
                    EncounterManager.Instance.GetPlayer().AttackStrong();
                    break;
            }
        });

        var textChildren = GetComponentsInChildren<TextMeshProUGUI>();
        _name = textChildren.First(x => x.name == "Name");
        _hitChance = textChildren.First(x => x.name == "Hit Chance");
        _damage = textChildren.First(x => x.name == "Damage");
    }

    // Update is called once per frame
    private void Update()
    {
        var currentAttack = EncounterManager.Instance.GetPlayer().GetActiveMasque().attacks[(int)attackType];
        _name.text = currentAttack.attackName;
        _damage.text = $"Damage: {currentAttack.damage}";
        _hitChance.text = $"Hit: {currentAttack.hitChance * 100}%";

        _image.sprite = EncounterManager.Instance.GetPlayer().GetActiveMasque().type switch
        {
            MasqueType.Strength => strengthSprite,
            MasqueType.Intelligence => intelligenceSprite,
            MasqueType.Charisma => charismaSprite,
            _ => _image.sprite
        };

        _button.enabled = EncounterManager.Instance.PlayerTurn;
    }
}