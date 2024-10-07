namespace BossFight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const ConsoleKey CommandBaseAttack = ConsoleKey.Z;

            Random random = new();

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
            int damage = 0;
            bool isCritical = false;
            bool isAttack = false;
            bool isFight = playerHealth > 0 && enemyHealth > 0;

            while (isFight)
            {
                damage = 0;
                isAttack = false;
                isCritical = false;
                Console.Clear();

                Console.WriteLine($"Здоровье: {playerHealth}");
                Console.WriteLine($"Мана: {playerMana}");
                Console.WriteLine();
                Console.WriteLine($"Здоровье противника: {enemyHealth}");
                Console.WriteLine();
                Console.WriteLine($"{CommandBaseAttack}) Базовая атака");
                ConsoleKey userInput = Console.ReadKey(true).Key;

                switch (userInput)
                {
                    case CommandBaseAttack:
                        isAttack = true;
                        isCritical = random.NextSingle() <= playerCriticalChance;
                        damage = playerBaseAttackDamage;

                        if (isCritical)
                        {
                            damage = (int)(damage * playerCriticalDamageMultiplyer);
                        }

                        damage = random.Next((int)(damage * (1 / damageRandomizationOffset)), (int)(damage * damageRandomizationOffset));
                        enemyHealth -= damage;
                        break;
                }

                Console.WriteLine();

                if (isCritical)
                {
                    Console.WriteLine("Вы нанесли критический урон");
                }

                if (isAttack)
                {
                    Console.WriteLine($"Противнику нанесено {damage} урона");
                }

                Console.ReadKey(true);
                Console.WriteLine();

                if (enemyHealth > 0)
                {
                    if (enemyActiveStunEffectsCount == 0)
                    {
                        Console.WriteLine("Противник атакует");
                        isCritical = random.NextSingle() <= enemyCriticalChance;
                        damage = enemyDamage;

                        if (isCritical)
                        {
                            damage = (int)(damage * enemyCriticalDamageMultiplyer);
                            Console.WriteLine("Противник нанёс критический урон");
                        }

                        damage = random.Next((int)(damage * (1 / damageRandomizationOffset)), (int)(damage * damageRandomizationOffset));
                        playerHealth -= damage;

                        Console.WriteLine($"Противник нанес вам {damage} урона");
                    }
                    else
                    {
                        Console.WriteLine("Противник оглушён. Пропуск хода.");
                    }
                }
                else
                {
                    Console.WriteLine("Противник побеждён");
                }

                Console.ReadKey(true);
                isFight = playerHealth > 0 && enemyHealth > 0;
            }
        }
    }
}
