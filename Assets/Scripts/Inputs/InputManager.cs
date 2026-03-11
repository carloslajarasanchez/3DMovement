using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Instancia unica del manager.
    private static InputManager _instance;

    // Asset generado por Unity Input System con todas las acciones.
    private static PlayerInputAction _actions;

    /// <summary>
    /// Enumeracion de mapas de control soportados por el juego.
    /// </summary>
    public enum ControlMap
    {
        UI,
        Player
    }

    /// <summary>
    /// Acceso global a las acciones.
    /// Si no existe instancia, la crea automaticamente.
    /// </summary>
    public static PlayerInputAction Actions
    {
        get
        {
            EnsureInstance();
            return _actions;
        }
    }

    /// <summary>
    /// Intenta obtener las acciones sin forzar creacion del singleton.
    /// Util durante apagado/destruccion para evitar crear objetos tarde.
    /// </summary>
    /// <param name="actions">Salida con la referencia actual de acciones.</param>
    /// <returns>true si ya existen acciones inicializadas; false si no.</returns>
    public static bool TryGetActions(out PlayerInputAction actions)
    {
        actions = _actions;
        return actions != null;
    }

    /// <summary>
    /// Garantiza que exista una instancia del manager y su PlayerInputActions.
    /// </summary>
    private static void EnsureInstance()
    {
        if (_instance != null && _actions != null)
            return;

        // Crea objeto runtime persistente para alojar el singleton.
        var go = new GameObject("PlayerInputManager");
        _instance = go.AddComponent<InputManager>();
        DontDestroyOnLoad(go);

        // Inicializa acciones deshabilitadas; se habilita mapa segun contexto.
        _actions = new PlayerInputAction();
        _actions.Disable();
    }

    /// <summary>
    /// Protege el patron singleton y asegura persistencia entre escenas.
    /// </summary>
    private void Awake()
    {
        // Si ya existe otra instancia, esta se destruye.
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Esta instancia pasa a ser la global.
        _instance = this;
        DontDestroyOnLoad(gameObject);

        // Inicializacion defensiva de acciones si faltan.
        if (_actions == null)
        {
            _actions = new PlayerInputAction();
            _actions.Disable();
        }
    }

    /// <summary>
    /// Cambia el ActionMap activo.
    /// Deshabilita todas las acciones y habilita solo el mapa solicitado.
    /// </summary>
    /// <param name="controlMap">Mapa de control a activar.</param>
    public static void SwitchControlMap(ControlMap controlMap)
    {
        var mapToEnable = GetInputActionMap(controlMap);

        if (mapToEnable == null)
            throw new ArgumentException($"ActionMap no soportado: {controlMap}");

        // Seguridad: no dejar mapas previos activos en paralelo.
        Actions.Disable();
        mapToEnable.Enable();
    }

    /// <summary>
    /// Traduce el enum ControlMap al InputActionMap real.
    /// </summary>
    /// <param name="controlMap">Mapa solicitado.</param>
    /// <returns>InputActionMap asociado o null si no existe.</returns>
    private static InputActionMap GetInputActionMap(ControlMap controlMap)
    {
        switch (controlMap)
        {
            case ControlMap.UI:
                return Actions.UI;
            case ControlMap.Player:
                return Actions.Player;
            default:
                return null;
        }
    }

    /// <summary>
    /// Al desactivar la instancia principal, libera recursos de Input System.
    /// </summary>
    private void OnDisable()
    {
        if (_instance != this)
            return;

        if (_actions != null)
        {
            // Importante: Disable + Dispose para evitar fugas y callbacks colgando.
            _actions.Disable();
            _actions.Dispose();
            _actions = null;
        }
    }

    /// <summary>
    /// Limpia la referencia singleton cuando se destruye la instancia activa.
    /// </summary>
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
