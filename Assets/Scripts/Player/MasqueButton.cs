using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class MasqueButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button _button;
    [SerializeField] private PlayerCombat player; 
    [SerializeField] private MasqueType masqueType; 
    void Start()
    {
        _button = GetComponentInParent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() =>
        {
            player.SetActiveMasque(masqueType);
        });
    }

    // Update is called once per frame
    void Update()
    {
        var isActive = player.GetActiveMasque()?.type == masqueType;
        var isBroken = player.GetActiveMasque()?.durability > 0;
        _button.enabled = !isActive;
    }
}
