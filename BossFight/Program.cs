namespace BossFight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const ConsoleKey CommandBaseAttack = ConsoleKey.Z;
            const ConsoleKey CommandFireBall = ConsoleKey.X;
            const ConsoleKey CommandExplose = ConsoleKey.C;
            const ConsoleKey CommandPotion = ConsoleKey.V;

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
            int activeFireBallsCount = 0;

            int enemyHealth = 1000000;
            int enemyDamage = 150;
            float enemyCriticalChance = 0.2f;
            float enemyCriticalDamageMultiplyer = 2.5f;
            int enemyActiveStunEffectsCount = 0;

            float damageRandomizationOffset = 1.2f;
            float invertedDamageRandomizationOffset = 1 / damageRandomizationOffset;
            int totalDamage;
            bool isCritical;
            bool isAttack;

            while (playerHealth > 0 && enemyHealth > 0)
            {
                totalDamage = 0;
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
                Console.WriteLine($"{CommandFireBall}) Выпустить огненный шар (Стоимость: {playerFireBallActivationCost} маны)");
                Console.WriteLine($"{CommandExplose}) Взорвать огненный шар (Выпущенно: {activeFireBallsCount})");
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
                        totalDamage = playerBaseAttackDamage;

                        if (isCritical)
                        {
                            totalDamage = (int)(totalDamage * playerCriticalDamageMultiplyer);
                        }

                        totalDamage = random.Next((int)(totalDamage * invertedDamageRandomizationOffset), (int)(totalDamage * damageRandomizationOffset));
                        enemyHealth -= totalDamage;
                        break;

                    case CommandFireBall:
                        if (playerMana >= playerFireBallActivationCost)
                        {
                            playerMana -= playerFireBallActivationCost;
                            activeFireBallsCount++;
                            Console.WriteLine("Огненый шар выпущен");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно маны");
                        }
                        break;

                    case CommandExplose:
                        if (activeFireBallsCount > 0)
                        {
                            for (int i = 0; i < activeFireBallsCount; i++)
                            {
                                isAttack = true;
                                isCritical = random.NextSingle() <= playerCriticalChance;
                                int damage = playerExploseDamage;

                                if (isCritical)
                                {
                                    damage = (int)(damage * playerCriticalDamageMultiplyer);
                                }

                                damage = random.Next((int)(damage * (1 / damageRandomizationOffset)), (int)(damage * damageRandomizationOffset));
                                enemyActiveStunEffectsCount += playerExploseAttackStunEffects;
                                totalDamage += damage;
                                Console.WriteLine($"На противника наложено {playerExploseAttackStunEffects} эффекта оглушения");
                            }

                            enemyHealth -= totalDamage;
                            activeFireBallsCount = 0;
                        }
                        else
                        {
                            Console.WriteLine("Нет выпущенных огненных шаров");
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
                    Console.WriteLine($"Противнику нанесено {totalDamage} урона");
                }

                Console.ReadKey(true);
                Console.WriteLine();

                if (enemyHealth > 0)
                {
                    if (enemyActiveStunEffectsCount <= 0)
                    {
                        Console.WriteLine("Противник атакует");
                        isCritical = random.NextSingle() <= enemyCriticalChance;
                        totalDamage = enemyDamage;

                        if (isCritical)
                        {
                            totalDamage = (int)(totalDamage * enemyCriticalDamageMultiplyer);
                            Console.WriteLine("Противник нанёс критический урон");
                        }

                        totalDamage = random.Next((int)(totalDamage * (1 / damageRandomizationOffset)), (int)(totalDamage * damageRandomizationOffset));
                        playerHealth -= totalDamage;

                        Console.WriteLine($"Противник нанес вам {totalDamage} урона");
                    }
                    else
                    {
                        Console.WriteLine("Противник оглушён. Пропуск хода.");
                        enemyActiveStunEffectsCount--;
                    }
                }

                Console.ReadKey(true);
            }

            if (enemyHealth > 0)
            {
                Console.WriteLine("Вы проиграли");
            }
            else if (playerHealth > 0)
            {
                Console.WriteLine("Противник побеждён");
            }
            else
            {
                Console.WriteLine("Ничья");
            }

            Console.ReadKey(true);
        }
    }
}
