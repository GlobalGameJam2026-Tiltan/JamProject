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
            switch (attackIndex)
            {
                case 0:
                    player.AttackBasic();
                    break;
                case 1:
                    player.AttackMedium();
                    break;
                case 2:
                    player.AttackStrong();
                    break;
                case 3:
                    //player.Defend();
                    break;
            }
        };
    }

// Update is called once per frame
    void Update()
    {
        if (attackIndex == 3)
            _button.text = "Defend";
        else
            _button.text = player.GetActiveMasque().attacks[attackIndex].name;
    }
}