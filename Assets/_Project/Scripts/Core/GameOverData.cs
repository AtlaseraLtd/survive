public static class GameOverData
{
    public static float survivalTime = 0f;
    public static string causeOfDeath = "Zombies";

    public static void Reset()
    {
        survivalTime = 0f;
        causeOfDeath = "Zombies";
    }
}