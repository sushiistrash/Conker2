using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{

    #region Singleton

    public static UISystem Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of ui system found!");
            return;
        }
        Instance = this;
    }

    #endregion

    [SerializeField] private Image[] _heartsImages = new Image[PlayerHealth.MaxHealth];
    [SerializeField] private Sprite _heartFull, _heartEmpty;

    [SerializeField] private TextMeshProUGUI _gemCountText;
    [SerializeField] private PlayerItems _playerItems;

    [SerializeField] private Image _fadeScreen;
    [SerializeField] private float _fadeSpeed;
    public float FadeSpeed { get { return _fadeSpeed; }  private set { _fadeSpeed = value; } }
    private bool _isFadeToBlack = false;
    private bool _isFadeFromBlack = false;
    [SerializeField] private GameObject _levelCompleteText;

    private void Start()
    {
        UpdateGemCount(_playerItems.GemsCollected);
        FadeFromBlack();
    }

    private void Update()
    {
        if (_isFadeToBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));
            if (_fadeScreen.color.a == 1f)
            {
                _isFadeToBlack = false;
            }
        }

        if(_isFadeFromBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));
            if (_fadeScreen.color.a == 0f)
            {
                _isFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        _isFadeToBlack = true;
        _isFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        _isFadeToBlack = false;
        _isFadeFromBlack = true;
    }

    public void UpdateHealthDisplay(int currentHealth)
    {
        SetHeartsToEmpty(_heartsImages.Length);

        for (int health = 0; health < currentHealth; health++)
        {
            _heartsImages[health].sprite = _heartFull;
        }
    }

    private void SetHeartsToEmpty(int amountOfHearts)
    {
        for (int heart = 0; heart < amountOfHearts; heart++)
        {
            _heartsImages[heart].sprite = _heartEmpty;
        }
    }

    public void UpdateGemCount(int gemsCollected)
    {
        _gemCountText.text = gemsCollected.ToString();
    }

    public void ShowLevelCompleteText()
    {
        _levelCompleteText.SetActive(true);
    }
}
