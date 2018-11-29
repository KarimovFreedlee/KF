using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsFight
{
    class Program
    {
        static bool again = true;
        static bool newgame = true;
        Random random = new Random();

        static void Main()
        {
            Console.WriteLine("Menu:" + System.Environment.NewLine);
            if (newgame)
            {
                int selectedAction = MultipleChoice(Console.CursorTop, "New Game", "Exit");
                switch (selectedAction)
                {
                    case 0:
                        Game(newgame);
                        break;
                    case 1:
                        again = false;
                        return;
                }
            }
            else
            {
                int selectedAction = MultipleChoice(Console.CursorTop, "Continue", "New Game", "Exit");
                switch (selectedAction)
                {
                    case 0:
                        Game(newgame);
                        break;
                    case 1:
                        newgame = true;
                        Game(newgame);
                        break;
                    case 2:
                        again = false;
                        return;
                }
            }
            
        }

        static void Game(bool newgame)
        {
            Potion PotionHP10 = new Potion("Lesser potion of heal [+10 HP]", 40, 20, 10);
            Potion PotionHP20 = new Potion("Medium potion of heal [+20 HP]", 50, 15, 20);
            Potion PotionHP30 = new Potion("Great potion of heal [+30 HP]", 100, 10, 30);
            Weapon Sword = new Weapon("Sword [+2 hit] | [+2 damage]", 300, 1, 2, 2);
            Armor LeatherArmor = new Armor("Leather armor [+4 armor]", 350, 1, 4, 0);

            int selectedAction;
            Random random = new Random();
            
            Character PlayerFighter = new Character();
                
                while (PlayerFighter.CharacterName.Length < 3 || PlayerFighter.CharacterName.Length > 10 || string.IsNullOrWhiteSpace(PlayerFighter.CharacterName))
                {
                    Console.Write("Please, enter your Name [3-10 symbols]: ");
                    PlayerFighter.CharacterName = Console.ReadLine().Trim();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (string.IsNullOrWhiteSpace(PlayerFighter.CharacterName)) Console.WriteLine("String is empty!");
                    else if (PlayerFighter.CharacterName.Length > 10) Console.WriteLine("Your name is to long!");
                    else if (PlayerFighter.CharacterName.Length < 3) Console.WriteLine("Your name is to short!");
                    Console.ResetColor();
                }
            ChangeClass:
                Console.WriteLine("Greatings, " + PlayerFighter.CharacterName + "!" + System.Environment.NewLine + System.Environment.NewLine + "Choose your class:");
                selectedAction = MultipleChoice(Console.CursorTop, "Warrior", "Barbarian", "Bard", "Paladin");
                Choose(selectedAction, PlayerFighter, random);

            int fullHp = PlayerFighter.Hp;
            int level = 0;

            string[] names = { "Wardrich", "Tinedon", "Keken", "Gwinlin", "Ren mini BOSS", "Vupsen` & Pupsen`", "Nagibator2000", "Losdral", "Bornaz", "Dielriel BOSS" };
            for (int round = 1; round <= 10 && again && PlayerFighter.FightResult; round++)
            {

                Character EnemyFighter = new Character();
                EnemyFighter.CharacterName = names[round - 1];
                Console.ForegroundColor = ConsoleColor.Green;
                Choose(random.Next(0, 4), EnemyFighter, random);

                if (round == 5)
                {
                    level += 2;
                    EnemyFighter.AttackClass += level;
                    EnemyFighter.ArmorClass += level;
                    EnemyFighter.Gold *= 3;
                }
                else if (round == 10)
                {
                    level += 5;
                    EnemyFighter.AttackClass += level;
                    EnemyFighter.ArmorClass += level;
                    EnemyFighter.Gold *= 10;
                }

                ShowStat(PlayerFighter, ConsoleColor.Green, "You: ");
                ShowStat(EnemyFighter, ConsoleColor.Red, "Enemy: ");

                PlayerFighter.Initiative = random.Next(1, 21);
                EnemyFighter.Initiative = random.Next(1, 21);

                FightMechanic(PlayerFighter, EnemyFighter, random, round, fullHp, PotionHP10, PotionHP20, PotionHP30);
                OutputResult(PlayerFighter, EnemyFighter, round);
                PlayerFighter.Hp = fullHp;

                if (PlayerFighter.FightResult)
                {
                startLoop:
                    Console.WriteLine(System.Environment.NewLine + "Choose:");
                    selectedAction = MultipleChoice(Console.CursorTop, "Next round", "Shop", "Exit");
                    switch (selectedAction)
                    {
                        case 0:
                            continue;
                        case 1:
                        shop:
                            Console.Clear();
                            Console.WriteLine(
                               "Gold: " + PlayerFighter.Gold +
                               " | Potions[+10]: " + PlayerFighter.PotionHP10 +
                               " | Potions[+20]: " + PlayerFighter.PotionHP20 +
                               " | Potions[+30]: " + PlayerFighter.PotionHP30 + System.Environment.NewLine
                            );
                            Console.WriteLine("Choose a product:");
                            selectedAction = MultipleChoice(
                                Console.CursorTop,
                                "Back",
                                "(" + PlayerFighter.Gold / PotionHP10.Price + ") " + PotionHP10.Name + " - " + PotionHP10.Price,
                                "(" + PlayerFighter.Gold / PotionHP20.Price + ") " + PotionHP20.Name + " - " + PotionHP20.Price,
                                "(" + PlayerFighter.Gold / PotionHP30.Price + ") " + PotionHP30.Name + " - " + PotionHP30.Price,
                                "(" + PlayerFighter.Gold / Sword.Price + ") " + Sword.Name + " - " + Sword.Price,
                                "(" + PlayerFighter.Gold / LeatherArmor.Price + ") " + LeatherArmor.Name + " - " + LeatherArmor.Price
                            );
                            bool smth;
                            switch (selectedAction)
                            {
                                case 0:
                                    goto startLoop;
                                case 1:
                                    smth = Shop(PlayerFighter, PotionHP10);
                                    if (smth) PlayerFighter.PotionHP10 += 1;
                                    goto shop;
                                case 2:
                                    smth = Shop(PlayerFighter, PotionHP20);
                                    if (smth) PlayerFighter.PotionHP20 += 1;
                                    goto shop;
                                case 3:
                                    smth = Shop(PlayerFighter, PotionHP30);
                                    if (smth) PlayerFighter.PotionHP30 += 1;
                                    goto shop;
                                case 4:
                                    smth = Shop(PlayerFighter, Sword);
                                    if (smth)
                                    {
                                        PlayerFighter.HitClass += Sword.Hit;
                                        PlayerFighter.AttackClass += Sword.Damage;
                                    }
                                    goto shop;
                                case 5:
                                    smth = Shop(PlayerFighter, LeatherArmor);
                                    if (smth) PlayerFighter.ArmorClass += LeatherArmor.ArmorPhysical;
                                    goto shop;
                            }
                            break;
                        case 2:
                            newgame = false;
                            Main();
                            break;
                    }
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine(System.Environment.NewLine + "Choose:");
                    selectedAction = MultipleChoice(Console.CursorTop, "Repeat", "Change class", "Exit");
                    switch (selectedAction)
                    {
                        case 0:
                            round = 0;
                            PlayerFighter.FightResult = true;
                            continue;
                        case 1:
                            PlayerFighter.FightResult = true;
                            goto ChangeClass;
                        case 2:
                            newgame = false;
                            Main();
                            break;
                    }
                    Console.Clear();
                }
                Console.Clear();
            }
        }

        static bool Shop(Character PlayerFighter, IShop Good)
        {
            if (PlayerFighter.Gold >= Good.Price)
            {
                PlayerFighter.Gold -= Good.Price;
                Console.Clear();
                Console.WriteLine(
                    "Gold: " + PlayerFighter.Gold +
                    " | Potions[+10]: " + PlayerFighter.PotionHP10 +
                    " | Potions[+20]: " + PlayerFighter.PotionHP20 +
                    " | Potions[+30]: " + PlayerFighter.PotionHP30 + System.Environment.NewLine);
                return true;
            }
            else
            {
                Console.WriteLine(System.Environment.NewLine + "You have no money, beggar!");
                Console.ReadKey(true);
                return false;
            }
           
        }

        static int MultipleChoice(int startY, params string[] options)
        {
            const int startX = 0;
            const int optionsPerLine = 1;
            const int spacingPerLine = 14;
            int currentSelection = 0;
            ConsoleKey key;
            Console.CursorVisible = false;
            do
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(options[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= optionsPerLine)
                                currentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + optionsPerLine < options.Length)
                                currentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            Console.Clear();
            return currentSelection;
        }

        static void ShowStat(Character Fighter, ConsoleColor theColor, string value)
        {
            Console.ForegroundColor = theColor;
            Console.Write(value);
            Console.ResetColor();
            Console.WriteLine(
                Fighter.CharacterName +
                " | Class: " + Fighter.CharacterClass +
                " | HP: " + Fighter.Hp +
                " | Gold: " + Fighter.Gold + System.Environment.NewLine +
                "Attack Class: " + Fighter.AttackClass + " | " +
                "Armor Class: " + Fighter.ArmorClass + System.Environment.NewLine +
                "Strength: " + Fighter.Strength + "[" + Fighter.StrengthMod + "]" + System.Environment.NewLine +
                "Dexterity: " + Fighter.Dexterity + "[" + Fighter.DexterityMod + "]" + System.Environment.NewLine +
                "Constitution: " + Fighter.Constitution + "[" + Fighter.ConstitutionMod + "]" + System.Environment.NewLine +
                "Intelligence: " + Fighter.Intelligence + "[" + Fighter.IntelligenceMod + "]" + System.Environment.NewLine +
                "Wisdom: " + Fighter.Wisdom + "[" + Fighter.WisdomMod + "]" + System.Environment.NewLine +
                "Charisma: " + Fighter.Charisma + "[" + Fighter.CharismaMod + "]" + System.Environment.NewLine
            );
        }

        static void Choose(int selectedClass, Character Fighter, Random random)
        {
            switch (selectedClass)
            {
                case 0:
                    Settings(Fighter, "Warrior", 35, 1, 2, 0, 3, 4, 5, random);
                    break;
                case 1:
                    Settings(Fighter, "Barbarian", 30, 2, 5, 0, 1, 4, 3, random);
                    break;
                case 2:
                    Settings(Fighter, "Bard", 30, 0, 3, 1, 2, 4, 5, random);
                    break;
                case 3:
                    Settings(Fighter, "Paladin", 40, 0, 3, 1, 2, 5, 4, random);
                    break;
            }
        }

        static void Settings(
            Character Fighter,
            string CharacterClass,
            int Hp,
            int numberOfStrength,
            int numberOfDexterity,
            int numberOfConstitution,
            int numberOfIntelligence,
            int numberOfWisdom,
            int numberOfCharisma,
            Random random)
        {

            int[] results = new int[6];
            int a;

            for(int i = 0; i < 6; i++)
            {
                a = random.Next(6, 13);
                results[i] = a;            
            }
            Array.Sort(results);

            Fighter.CharacterClass = CharacterClass;
            Fighter.Hp = Hp;
            Fighter.Gold = random.Next(50, 101);

            Fighter.Strength = results[numberOfStrength] + 6;
            Fighter.StrengthMod = CheckMod(Fighter.Strength);

            Fighter.Dexterity = results[numberOfDexterity] + 6;
            Fighter.DexterityMod = CheckMod(Fighter.Dexterity);

            Fighter.Constitution = results[numberOfConstitution] + 6;
            Fighter.ConstitutionMod = CheckMod(Fighter.Constitution);

            Fighter.Intelligence = results[numberOfIntelligence] + 6;
            Fighter.IntelligenceMod = CheckMod(Fighter.Intelligence);

            Fighter.Wisdom = results[numberOfWisdom] + 6;
            Fighter.WisdomMod = CheckMod(Fighter.Wisdom);

            Fighter.Charisma = results[numberOfCharisma] + 6;
            Fighter.CharismaMod = CheckMod(Fighter.Charisma);

            Fighter.AttackClass += Fighter.StrengthMod;
            Fighter.ArmorClass += Fighter.DexterityMod;
            Fighter.Hp += Fighter.ConstitutionMod;
        }

        static int CheckMod(int value)
        {
            if (value % 2 == 0)  return (value - 10) / 2;
            else return (value - 11) / 2;           
        }

        static void FightMechanic(Character PlayerFighter, Character EnemyFighter, Random random, int round, int fullHp, Potion PotionHP10, Potion PotionHP20, Potion PotionHP30)
        {
            
            if (PlayerFighter.Initiative > EnemyFighter.Initiative)
            {
                Console.WriteLine(System.Environment.NewLine + PlayerFighter.CharacterName + " " + PlayerFighter.CharacterClass + " start fight first" + System.Environment.NewLine);
                Console.WriteLine(System.Environment.NewLine + "Press Enter to start");

                ConsoleKey key;
                do
                {
                    key = Console.ReadKey(true).Key;
                } while (key != ConsoleKey.Enter);
                
                Console.Clear();
                string roundString = "====================ROUND " + (round) + "====================";
                Console.SetCursorPosition((Console.WindowWidth - roundString.Length) / 2, Console.CursorTop);
                Console.WriteLine(roundString + System.Environment.NewLine);
                while (PlayerFighter.Hp > 0)
                {
                    string s = PlayerFighter.Hp + " HP | " + EnemyFighter.Hp + " HP";
                    string PlayerHP = new string('|', PlayerFighter.Hp);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(PlayerHP);
                    Console.ResetColor();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.Write(s);
                    string EnemyHP = new string('|', EnemyFighter.Hp);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(EnemyHP);
                    Console.ResetColor();

                    Console.WriteLine(System.Environment.NewLine + "Choose your action:");
                    int selectedAction = MultipleChoice(
                        Console.CursorTop, "Attack", "("+ PlayerFighter.PotionHP10 + ")Potion [+10]", 
                        "(" + PlayerFighter.PotionHP20 + ") Potion [+20]",
                        "(" + PlayerFighter.PotionHP30 + ") Potion [+30]"
                        );
                    bool potion;
                    switch (selectedAction)
                    {
                        case 0:
                            Damage(PlayerFighter, EnemyFighter, random);
                            if (EnemyFighter.Hp > 0) Damage(EnemyFighter, PlayerFighter, random);
                            else
                            {
                                Console.WriteLine(EnemyFighter.CharacterName + " " + EnemyFighter.CharacterClass + " is dead.");
                                EnemyFighter.FightResult = false;
                                return;
                            }
                            break;
                        case 1:
                            potion = CheckPotions(PlayerFighter, PlayerFighter.PotionHP10, fullHp, PotionHP10.HP);
                            if (potion) PlayerFighter.PotionHP10 -= 1;
                            else  break;
                            Damage(EnemyFighter, PlayerFighter, random);
                            break;
                        case 2:
                            potion = CheckPotions(PlayerFighter, PlayerFighter.PotionHP20, fullHp, PotionHP20.HP);
                            if (potion) PlayerFighter.PotionHP20 -= 1;
                            else break;
                            Damage(EnemyFighter, PlayerFighter, random);
                            break;
                        case 3:
                            potion = CheckPotions(PlayerFighter, PlayerFighter.PotionHP30, fullHp, PotionHP30.HP);
                            if (potion) PlayerFighter.PotionHP30 -= 1;
                            else break;
                            Damage(EnemyFighter, PlayerFighter, random);
                            break;
                    }

                }
                Console.WriteLine(PlayerFighter.CharacterName + " " + PlayerFighter.CharacterClass + " is dead");
                PlayerFighter.FightResult = false;
            }
            else
            {
                Console.WriteLine(System.Environment.NewLine + EnemyFighter.CharacterName + " " + EnemyFighter.CharacterClass + " start fight first" + System.Environment.NewLine);
                Console.WriteLine(System.Environment.NewLine + "Press Enter to start");
                ConsoleKey key;
                do
                {
                    key = Console.ReadKey(true).Key;
                } while (key != ConsoleKey.Enter);
                Console.Clear();
                string roundString = "====================ROUND " + (round) + "====================";
                Console.SetCursorPosition((Console.WindowWidth - roundString.Length) / 2, Console.CursorTop);
                Console.WriteLine(roundString + System.Environment.NewLine);

                while (EnemyFighter.Hp > 0)
                {
                    Damage(EnemyFighter, PlayerFighter, random);
                    if (PlayerFighter.Hp <= 0)
                    {
                        Console.WriteLine(PlayerFighter.CharacterName + " " + PlayerFighter.CharacterClass + " is dead");
                        PlayerFighter.FightResult = false;
                        return;
                    }

                battle:
                    string s = PlayerFighter.Hp + " HP | " + EnemyFighter.Hp + " HP";
                    string PlayerHP = new string('|', PlayerFighter.Hp);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(PlayerHP);
                    Console.ResetColor();
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.Write(s);
                    string EnemyHP = new string('|', EnemyFighter.Hp);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(EnemyHP);
                    Console.ResetColor();

                    Console.WriteLine(System.Environment.NewLine + "Choose your action:");
                    int selectedAction = MultipleChoice(
                        Console.CursorTop, "Attack", "(" + PlayerFighter.PotionHP10 + ")Potion [+10]",
                        "(" + PlayerFighter.PotionHP20 + ") Potion [+20]",
                        "(" + PlayerFighter.PotionHP30 + ") Potion [+30]"
                        );
                    bool potion;
                    switch (selectedAction)
                    {
                        case 0:
                            Damage(PlayerFighter, EnemyFighter, random);
                            if (EnemyFighter.Hp <= 0)
                            {
                                Console.WriteLine(EnemyFighter.CharacterName + " " + EnemyFighter.CharacterClass + " is dead.");
                                EnemyFighter.FightResult = false;
                                return;
                            }
                            break;
                        case 1:
                            potion = CheckPotions(PlayerFighter, PlayerFighter.PotionHP10, fullHp, PotionHP10.HP);
                            if (potion) PlayerFighter.PotionHP10 -= 1;
                            else goto battle;
                            Damage(EnemyFighter, PlayerFighter, random);
                            break;
                        case 2:
                            potion = CheckPotions(PlayerFighter, PlayerFighter.PotionHP20, fullHp, PotionHP20.HP);
                            if (potion) PlayerFighter.PotionHP20 -= 1;
                            else goto battle;
                            Damage(EnemyFighter, PlayerFighter, random);
                            break;
                        case 3:
                            potion = CheckPotions(PlayerFighter, PlayerFighter.PotionHP30, fullHp, PotionHP30.HP);
                            if (potion) PlayerFighter.PotionHP30 -= 1;
                            else goto battle;
                            Damage(EnemyFighter, PlayerFighter, random);
                            break;
                    }
                }
                Console.WriteLine(EnemyFighter.CharacterName + " " + EnemyFighter.CharacterClass + " is dead.");
                EnemyFighter.FightResult = false;
            }
        }

        static bool CheckPotions(Character PlayerFighter, int itemCount, int fullHp, int itemValue)
        {
            if (itemCount > 0)
            {
                if (PlayerFighter.Hp != fullHp)
                {
                    if (fullHp - PlayerFighter.Hp < itemValue)
                    {
                        PlayerFighter.Hp = fullHp;
                    }
                    else
                    {
                        PlayerFighter.Hp += itemValue;
                    }
                    Console.WriteLine("You have healed. " + itemCount + " potion [+" + itemValue + "] left.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Your fighter has full HP. Please, select another action.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("You haven't potion. Please, select another action.");
                return false;
            }
        }

        static bool HitCheck(Character FirstFighter, Character SecondFighter, Random random)
        {
            int hitFirstFighter = random.Next(FirstFighter.HitClass, 21);

            if(hitFirstFighter == 20 || hitFirstFighter >= SecondFighter.ArmorClass) return true;
            else return false;
        }

        static void Damage(Character FirstFighter, Character SecondFighter, Random random)
        {
            if (HitCheck(FirstFighter, SecondFighter, random))
            {
                int damage = FirstFighter.AttackClass + random.Next(1,5);
                if(SecondFighter.Hp > damage)
                {
                    SecondFighter.Hp = SecondFighter.Hp - damage;
                }
                else
                {
                    SecondFighter.Hp = 0;
                }
                Console.WriteLine(FirstFighter.CharacterName + " " + FirstFighter.CharacterClass + " dealed " + damage + " damage to " +SecondFighter.CharacterName+" "+ SecondFighter.CharacterClass);
            }
            else
            {
                Console.WriteLine(FirstFighter.CharacterName + " " + FirstFighter.CharacterClass + " missed");
            }
        }

        static void OutputResult(Character PlayerFighter, Character EnemyFighter, int round)
        {
            if (PlayerFighter.FightResult)
            {
                if (round == 10 && PlayerFighter.FightResult)
                {
                    string roundString = "====CONGRATULATION YOU HAVE FINISHED THIS ENEMY TOWER!====";
                    Console.SetCursorPosition((Console.WindowWidth - roundString.Length) / 2, Console.CursorTop);
                    Console.WriteLine(System.Environment.NewLine + roundString + System.Environment.NewLine);
                }
                else
                {
                    string roundString = "=============YOU WON=============";
                    Console.SetCursorPosition((Console.WindowWidth - roundString.Length) / 2, Console.CursorTop);
                    Console.WriteLine(System.Environment.NewLine + roundString + System.Environment.NewLine);
                    Console.WriteLine(
                       "Gold: " + PlayerFighter.Gold + " (+" + EnemyFighter.Gold + ")" +
                       " | Potions[+10]: " + PlayerFighter.PotionHP10 +
                       " | Potions[+20]: " + PlayerFighter.PotionHP20 +
                       " | Potions[+30]: " + PlayerFighter.PotionHP30 + System.Environment.NewLine);
                    PlayerFighter.Gold += EnemyFighter.Gold;
                }
            }
            else
            {
                string roundString = "=============YOU LOSE=============";
                Console.SetCursorPosition((Console.WindowWidth - roundString.Length) / 2, Console.CursorTop);
                Console.WriteLine(System.Environment.NewLine + roundString + System.Environment.NewLine);
            }
        }   
    }
}
