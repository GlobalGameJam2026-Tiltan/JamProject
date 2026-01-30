using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class MasqueButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button _button;
    private Image _icon;
    [SerializeField] private MasqueType masqueType; 
    void Start()
    {
        _button = GetComponent<Button>();
        _icon = GetComponentsInChildren<Image>().First(x => x.name == "Icon");
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            EncounterManager.Instance.GetPlayer().SetActiveMasque(masqueType);
        });
    }

    // Update is called once per frame
    void Update()
    {
        var activeMasque = EncounterManager.Instance.GetPlayer().GetActiveMasque();
        var isActive = activeMasque?.type == masqueType;
        var isBroken = activeMasque?.durability > 0;
        _button.enabled = !isActive;

        _icon.sprite = EncounterManager.Instance.GetPlayer().GetMasqueByType(masqueType)?.swapIcon;
    }
}
