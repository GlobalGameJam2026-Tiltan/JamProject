using UnityEngine;
using UnityEngine.UIElements;

public class AttackButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button _button;
    [SerializeField] private PlayerCombat player; 
    [SerializeField] private int attackIndex; 
    void Start()
    {
        _button = GetComponentInParent<Button>();
        _button.clicked += () =>
        {
            //player.SetActiveMasque(masqueType);
        };
    }

    // Update is called once per frame
    void Update()
    {
        // var isActive = player.GetActiveMasque()?.type == masqueType;
        // var isBroken = player.GetActiveMasque()?.durability > 0;
        // _button.SetEnabled(isActive);
    }
}
