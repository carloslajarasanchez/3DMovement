using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float _invincibilityDuration = 2f;
    [SerializeField] private float _blinkInterval = 0.1f;
    [SerializeField] private Renderer[] _playerRenderers; // arrastra los renderers del modelo

    private void Start()
    {
        Main.CustomEvents.OnPlayerDamaged?.AddListener(HandleDamaged);
    }

    private void HandleDamaged()
    {
        StartCoroutine(InvincibilityRoutine());
    }

    private IEnumerator InvincibilityRoutine()
    {
        Main.Player.SetInvincible(true);

        float elapsed = 0f;
        while (elapsed < _invincibilityDuration)
        {
            SetRenderersVisible(false);
            yield return new WaitForSeconds(_blinkInterval);
            SetRenderersVisible(true);
            yield return new WaitForSeconds(_blinkInterval);
            elapsed += _blinkInterval * 2f;
        }

        SetRenderersVisible(true);
        Main.Player.SetInvincible(false);
    }

    private void SetRenderersVisible(bool visible)
    {
        foreach (var renderer in _playerRenderers)
            renderer.enabled = visible;
    }

    private void OnDestroy()
    {
        Main.CustomEvents.OnPlayerDamaged?.RemoveListener(HandleDamaged);
    }
}
