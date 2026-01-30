using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MasqueButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button _button;
    private Image _icon;
    [SerializeField] private MasqueType masqueType; 
    private void Start()
    {
        _button = GetComponent<Button>();
        _icon = GetComponentsInChildren<Image>().First(x => x.name == "Icon");
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            EncounterManager.Instance.PlayerAction();
            EncounterManager.Instance.GetPlayer().SetActiveMasque(masqueType);
            EncounterManager.Instance.SwappedMasque();
        });
    }

    // Update is called once per frame
    private void Update()
    {
        var activeMasque = EncounterManager.Instance.GetPlayer().GetActiveMasque();
        var isActive = activeMasque?.type == masqueType;
        var isBroken = activeMasque?.durability > 0;
        _button.enabled = !isActive && EncounterManager.Instance.PlayerTurn;

        _icon.sprite = EncounterManager.Instance.GetPlayer().GetMasqueByType(masqueType)?.swapIcon;
    }
}
