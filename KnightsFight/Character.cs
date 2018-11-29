using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsFight
{
    class Character
    {
        public string CharacterName { get; set; } = "";
        public string CharacterClass { get; set; }
        public int Initiative { get; set; }
        public int Hp { get; set; }
        public int Strength { get; set; }
        public int StrengthMod { get; set; }
        public int Dexterity { get; set; }
        public int DexterityMod { get; set; }
        public int Constitution { get; set; }
        public int ConstitutionMod { get; set; }
        public int Intelligence { get; set; }
        public int IntelligenceMod { get; set; }
        public int Wisdom { get; set; }
        public int WisdomMod { get; set; }
        public int Charisma { get; set; }
        public int CharismaMod { get; set; }

        public int ArmorClass { get; set; } = 10;
        public int AttackClass { get; set; } = 1;
        public int HitClass { get; set; } = 1;

        public bool FightResult { get; set; } = true;
        public int Gold { get; set; }

        public int PotionHP10 { get; set; } = 5;
        public int PotionHP20 { get; set; } = 2;
        public int PotionHP30 { get; set; } = 0;

        public int[] Inventory { get; set; }
        public Weapon PersonWeapon { get; set; }
        public Armor PersonArmor { get; set; }
    }
}
