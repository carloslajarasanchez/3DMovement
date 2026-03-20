using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeLanguage : MonoBehaviour
{
    private Button _button;

    [Tooltip("Nombre del archivo JSON en Resources/i18n/ (ej: 'en', 'es')")]
    [SerializeField]
    private string _language;

    private void Awake()
    {
        this._button = GetComponent<Button>();

        if (this._button != null)
        {
            this._button.onClick.AddListener(MakeChange);
        }
    }

    private void MakeChange()
    {
        if (string.IsNullOrEmpty(this._language))
        {
            Debug.LogWarning($"[ChangeLanguage] El campo '_language' en {gameObject.name} está vacío.");
            return;
        }

        // Cambiamos el idioma
        Main.I18n.ChangeLanguage(this._language);
    }

}
