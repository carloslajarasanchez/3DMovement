using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private List<UIPanelController> _panels;

    private Stack<UIPanelController> _panelStack = new Stack<UIPanelController>();

    private void OnEnable()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.UI);
        this.PushPanel(this._panels[0]);
    }

    private void OnDisable()
    {
        this._panelStack.Clear();
    }

    private void PushPanel(UIPanelController panel)
    {
        foreach (var p in this._panelStack)// Ocultamos los paneles
        {
            p.HidePanel();
        }

        this._panelStack.Push(this._panels[0]);
        panel.ShowPanel();
    }

    public void LoadPanel(UIPanelController panel)
    {
        this.PushPanel(panel);
    }
    // Contenedor de dependencias, inyeccion de dependencias
    public void Back()
    {
        var panelToHide = this._panelStack.Pop();
        panelToHide.HidePanel();
        panelToHide.ForgetLastItem();

        var panelToShow = this._panelStack.Peek();
        panelToShow.ShowPanel();
    }
}
