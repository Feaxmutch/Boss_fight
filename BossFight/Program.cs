namespace BossFight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int playerMaxHealth = 1000;
            int playerHealth = playerMaxHealth;
            int playerMaxMana = 100;
            int playerMana = playerMaxMana;
            int playerBaseAttackDamage = 15000;
            int playerExploseDamage = 40000;
            float playerHealthRecoveryPercent = 0.3f;
            float playerManaRecoveryPrecent = 0.2f;
            float playerCriticalChance = 0.4f;
            float playerCriticalDamageMultiplyer = 2.3f;
            int playerHealingPotionsCount = 10;
            int playerExploseAttackStunEffects = 2;

            int enemyHealth = 1000000;
            int enemyDamage = 150;
            float enemyCriticalChance = 0.2f;
            float enemyCriticalDamageMultiplyer = 2.5f;
            int enemyActiveStunEffectsCount = 0;

            float damageRandomizationOffset = 1.2f;
        }
    }
}
