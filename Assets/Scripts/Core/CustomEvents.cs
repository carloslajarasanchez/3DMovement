using UnityEngine;
using UnityEngine.Events;

public class CustomEvents
{
    public UnityEvent OnLanguageChanged = new UnityEvent();

    public UnityEvent OnResumeGame = new UnityEvent();
    public UnityEvent OnPauseGame = new UnityEvent();
    public UnityEvent OnPointsChanged = new UnityEvent();
    public UnityEvent OnCoinsChanged = new UnityEvent();
    public UnityEvent OnLivesChanged = new UnityEvent();
    public UnityEvent OnPlayerDeath = new UnityEvent();
    public UnityEvent OnGameOver = new UnityEvent();
    public UnityEvent OnPowerUpTaken = new UnityEvent();
    public UnityEvent OnLevelFinished = new UnityEvent();
    public UnityEvent OnSceneLoaded = new UnityEvent();
    public UnityEvent OnHalfTimeFinished = new UnityEvent();
    public UnityEvent OnPlayerSpawned = new UnityEvent();
    public UnityEvent OnDeathAnimationFinished = new UnityEvent();
    public UnityEvent OnShooted = new UnityEvent();
    public UnityEvent OnJumped = new UnityEvent();
    public UnityEvent OnComboReset = new UnityEvent();
    public UnityEvent OnLevelUp = new UnityEvent();
    public UnityEvent OnPlayerDamaged;        // para invencibilidad y parpadeo

    public UnityEvent<GameObject> OnEnemyHit = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnEnemyDied = new UnityEvent<GameObject>();
    public UnityEvent<float> OnPlayerHit;     // para animación
    public UnityEvent<int> OnAttackCombo = new UnityEvent<int>();
    public UnityEvent<int> OnPointsObtained = new UnityEvent<int>();
    public UnityEvent<PlayerData> OnDataLoaded = new UnityEvent<PlayerData>();
}
