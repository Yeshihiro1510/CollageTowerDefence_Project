using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private ResourcesMenuView _resourcesMenuView;
    [SerializeField] private ButtonMenuView _buttonMenuView;
    [SerializeField] private GameObject _startLabel;

    public ButtonMenuView ButtonMenuView => _buttonMenuView;
    public ResourcesMenuView ResourcesMenuView => _resourcesMenuView;

    public void SetStartScreenScreen()
    {
        _startLabel.SetActive(true);
        _buttonMenuView.gameObject.SetActive(false);
        _resourcesMenuView.gameObject.SetActive(false);
    }

    public void SetGameplayScreen()
    {
        _startLabel.SetActive(false);
        _buttonMenuView.gameObject.SetActive(true);
        _resourcesMenuView.gameObject.SetActive(true);
    }
}