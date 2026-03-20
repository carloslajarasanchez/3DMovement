using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TranslatableItem : MonoBehaviour
{
    private TextMeshProUGUI _text;

    [Tooltip("Clave técnica que coincide con el JSON (ej: '_title')")]
    [SerializeField] private string _key;

    private void Awake()
    {
        this._text = GetComponent<TextMeshProUGUI>();

        Main.CustomEvents.OnLanguageChanged.AddListener(UpdateText);
    }

    private void Start()
    {
        // Forzamos primera actualizacion al inicio
        this.UpdateText();
    }

    private void UpdateText()
    {
        if (this._text == null)
            return;

        // Obtenemos la traduccion
        string translatedValue = Main.I18n.Get(this._key);

        this._text.text = translatedValue;
    }

    private void OnDestroy()
    {
        if (Main.CustomEvents != null)
        {
            Main.CustomEvents.OnLanguageChanged.RemoveListener(UpdateText);
        }
    }
}
