using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsFight
{
    interface IShop
    {
        string Name { get; set; }
        int Price { get; set; }
        int Limit { get; set; }
    }

    class Weapon : IShop
    {
        private string _name;
        private int _price;
        private int _limit;
        private int _hit;
        private int _damage;

        public Weapon(string Name, int Price, int Limit, int Hit, int Damage)
        {
            _name = Name;
            _price = Price;
            _limit = Limit;
            _hit = Hit;
            _damage = Damage;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public int Price { get { return _price; } set { _price = value; } }
        public int Limit { get { return _limit; } set { _limit = value; } }
        public int Hit { get { return _hit; } set { _hit = value; } }
        public int Damage { get { return _damage; } set { _damage = value; } }
    }

    class Armor : IShop
    {
        private string _name;
        private int _price;
        private int _limit;
        private int _armorPhysical;
        private int _armorMagical;

        public Armor(string Name, int Price, int Limit, int ArmorPhysical, int ArmorMagical)
        {
            _name = Name;
            _price = Price;
            _limit = Limit;
            _armorPhysical = ArmorPhysical;
            _armorMagical = ArmorMagical;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public int Price { get { return _price; } set { _price = value; } }
        public int Limit { get { return _limit; } set { _limit = value; } }
        public int ArmorPhysical { get { return _armorPhysical; } set { _armorPhysical = value; } }
        public int ArmorMagical { get { return _armorMagical; } set { _armorMagical = value; } }
    }

    class Potion : IShop
    {
        private string _name;
        private int _price;
        private int _limit;
        private int _hp;

        public Potion(string Name, int Price, int Limit, int HP)
        {
            _name = Name;
            _price = Price;
            _limit = Limit;
            _hp = HP;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public int Price { get { return _price; } set { _price = value; } }
        public int Limit { get { return _limit; } set { _limit = value; } }
        public int HP { get { return _hp; } set { _hp = value; } }
    }
}
