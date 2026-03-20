public enum States
{
    Small,
    Big,
    Fire
}
public class Player
{
    public int Coins { get; private set; }
    public int Lives { get; private set; } = 3;
    public int TimeElapsed { get; private set; } = 400;
    public int Points { get; private set; }

    public Diffuclt CurrentDificult { get; private set; } = Diffuclt.Easy;

    /// <summary>
    /// Añade monedas al player
    /// </summary>
    /// <param name="amount"></param>
    public void AddCoins(int amount)
    {
        Coins += amount;

        if (Coins >= 100)
        {
            AddLives(1);
            Coins = 0;
        }

        Main.AudioManager.PlaySound("Coin");
        Main.CustomEvents.OnCoinsChanged?.Invoke();
    }

    /// <summary>
    /// Resta vidas al player
    /// </summary>
    /// <param name="amount"></param>
    public void RestLives(int amount)
    {
        Lives -= amount;

        if (Lives <= 0)
        {
            Main.CustomEvents.OnGameOver?.Invoke();
        }

        Main.CustomEvents.OnLivesChanged?.Invoke();
        Main.CustomEvents.OnPlayerDeath.Invoke();
    }

    /// <summary>
    /// Añade vidas al player
    /// </summary>
    /// <param name="amount"></param>
    public void AddLives(int amount)
    {
        //Main.AudioManager.PlaySound("Live");
        Lives += amount;
        Main.CustomEvents.OnLivesChanged?.Invoke();
    }

    public void AddPoints(int amount)
    {
        Points += amount;
        Main.CustomEvents.OnPointsObtained?.Invoke(amount);
        Main.CustomEvents.OnPointsChanged?.Invoke();
    }

    public void ChangeDifficult(Diffuclt diffuclt)
    {
        CurrentDificult = diffuclt;

        switch (CurrentDificult)
        {
            case Diffuclt.Easy:
                Lives = 5;
                break;
            case Diffuclt.Medium:
                Lives = 3;
                break;
            case Diffuclt.Hard:
                Lives = 1;
                break;
        }
    }

    

    public void ChangeTimeElapsed(int amount)
    {
        TimeElapsed = amount;
    }

    public void ResetPlayer()
    {
        Coins = 0;
        Lives = 3;
        TimeElapsed = 400;
        Points = 0;
        CurrentDificult = Diffuclt.Easy;
    }
}
