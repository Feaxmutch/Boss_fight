namespace BossFight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const ConsoleKey CommandBaseAttack = ConsoleKey.Z;
            const ConsoleKey CommandFireBall = ConsoleKey.X;
            const ConsoleKey CommandPotion = ConsoleKey.C;

            Random random = new();

            int playerMaxHealth = 1000;
            int playerHealth = playerMaxHealth;
            int playerMaxMana = 100;
            int playerMana = playerMaxMana;
            int playerBaseAttackDamage = 15000;
            int playerExploseDamage = 40000;
            float playerHealthRecoveryPercent = 0.4f;
            float playerManaRecoveryPercent = 0.3f;
            float playerCriticalChance = 0.4f;
            float playerCriticalDamageMultiplyer = 2.3f;
            int playerPotionsCount = 5;
            int playerExploseAttackStunEffects = 3;
            int playerFireBallActivationCost = 25;
            bool isFireBallActive = false;

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

                if (enemyActiveStunEffectsCount > 0)
                {
                    Console.WriteLine($"Эффектов оглушения: {enemyActiveStunEffectsCount}");
                }

                Console.WriteLine();
                Console.WriteLine($"{CommandBaseAttack}) Базовая атака");

                if (isFireBallActive)
                {
                    Console.WriteLine($"{CommandFireBall}) Взорвать огненный шар");
                }
                else
                {
                    Console.WriteLine($"{CommandFireBall}) Выпустить огненный шар (Стоимость: {playerFireBallActivationCost} маны)");
                }

                Console.WriteLine($"{CommandPotion}) Использовать зелье (Количество: {playerPotionsCount})");

                ConsoleKey userInput = Console.ReadKey(true).Key;

                switch (userInput)
                {
                    default:
                        Console.WriteLine("Вы замешкались, дав противнику сделать ход раньше");
                        break;

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

                    case CommandFireBall:
                        if (isFireBallActive)
                        {
                            isFireBallActive = false;
                            isAttack = true;
                            isCritical = random.NextSingle() <= playerCriticalChance;
                            damage = playerExploseDamage;

                            if (isCritical)
                            {
                                damage = (int)(damage * playerCriticalDamageMultiplyer);
                            }

                            damage = random.Next((int)(damage * (1 / damageRandomizationOffset)), (int)(damage * damageRandomizationOffset));
                            enemyHealth -= damage;
                            enemyActiveStunEffectsCount += playerExploseAttackStunEffects;
                            Console.WriteLine($"На противника наложено {playerExploseAttackStunEffects} эффекта оглушения");
                        }
                        else
                        {
                            if (playerMana >= playerFireBallActivationCost)
                            {
                                playerMana -= playerFireBallActivationCost;
                                isFireBallActive = true;
                                Console.WriteLine("Огненый шар выпущен");
                            }
                            else
                            {
                                Console.WriteLine("Недостаточно маны");
                            }
                        }
                        break;

                    case CommandPotion:
                        if (playerPotionsCount > 0)
                        {
                            playerHealth = Math.Min(playerMaxHealth, playerHealth + (int)(playerMaxHealth * playerHealthRecoveryPercent));
                            playerMana = Math.Min(playerMaxMana, playerMana + (int)(playerMaxMana * playerManaRecoveryPercent));
                            playerPotionsCount--;
                            Console.WriteLine("Вы востановили здоровьте и ману");
                        }
                        else
                        {
                            Console.WriteLine("У вас недостаточно зелий");
                        }
                        
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
                    if (enemyActiveStunEffectsCount <= 0)
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
                        enemyActiveStunEffectsCount--;
                    }
                }
                else
                {
                    Console.WriteLine("Противник побеждён");
                }

                if (playerHealth <= 0)
                {
                    Console.WriteLine("Вы проиграли");
                }

                Console.ReadKey(true);
                isFight = playerHealth > 0 && enemyHealth > 0;
            }
        }
    }
}
