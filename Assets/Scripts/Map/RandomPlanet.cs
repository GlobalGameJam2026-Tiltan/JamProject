using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomPlanet : MonoBehaviour
{
    private RandomPlanetData _planetData;
    private PlanetLocation _planetLocation;
    private GameManager _gameManager;
    private Image _planet;
    private Image _planetOverlay;
    private Image _hover;

    private Button _button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _planet = GetComponentsInChildren<Image>().First(x => x.name == "Planet");
        _planetOverlay = GetComponentsInChildren<Image>().First(x => x.name == "Overlay");
        _hover = GetComponentsInChildren<Image>().First(x => x.name == "Hover");
        _button = GetComponent<Button>();
        var planetName = GetComponentInChildren<TextMeshProUGUI>();

        _button.onClick.AddListener(MoveHere);

        _planetData = _gameManager.GetPlanet(_planetLocation);
        planetName.text = _planetData.planetName;
        _planet.sprite = _planetData.sprite;
        if (_planetData.overlaySprite is not null)
            _planetOverlay.sprite = _planetData.overlaySprite;
        else
        {
            _planetOverlay.sprite = null;
            _planetOverlay.gameObject.SetActive(false);
        }
        
        if(_planetData.hoverSprite is not null)
            _hover.sprite = _planetData.hoverSprite;
        else
        {
            _hover.sprite = null;
            _hover.gameObject.SetActive(false);
        }
        //StartCoroutine(PlayAnimation());
    }

    public void SetPlanetLocation(PlanetLocation location)
    {
        _planetLocation = location;
    }

    // Public function to load a scene by name
    private void MoveHere()
    {
        if (_planetData.hoverSprite is null)
        {
            _gameManager.SetCurrentLocation(_planetData.location);
            EncounterManager.Instance.SetEncounter(_planetData.encounterType);
        }
    }
    
    // private IEnumerator PlayAnimation()
    // {
    //     //TODO Hover Animation
    // }
}