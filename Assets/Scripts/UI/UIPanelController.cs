using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _firstSelectedItem;
    private GameObject _lastSelectedItem;
    private void OnEnable()
    {
        var selectedItem = this.GetSelectedItem();
        EventSystem.current.SetSelectedGameObject(selectedItem);
    }

    private GameObject GetSelectedItem()
    {
        if (this._lastSelectedItem == null)
            return this._firstSelectedItem;


        return this._lastSelectedItem; 

    }

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        this._lastSelectedItem = EventSystem.current.currentSelectedGameObject;
        this.gameObject.SetActive(false);
    }

    public void ForgetLastItem()
    {
        this._lastSelectedItem = null;
    }
}
