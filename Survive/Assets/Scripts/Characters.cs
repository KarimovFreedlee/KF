using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Scripts
{
    
    interface Ichara
    {
        
        string Name { get; set; }
        int hp { get; set; }
        int minDamage { get; set; }
        int maxDamage { get; set; }
    }
    class Characters: Ichara
    {
        private string _name;
        private int _hp;
        private int _minDamage;
        private int _maxDamage;

        public Characters(string Name, int Hp, int MinDamage, int MaxDamage)
        {
            _name = Name;
            _hp = Hp;
            _minDamage = MinDamage;
            _maxDamage = MaxDamage;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public int minDamage { get { return _minDamage; } set { _minDamage = value; } }
        public int maxDamage { get { return _maxDamage; } set { _maxDamage = value; } }
        public int hp { get { return _hp; } set { _hp = value; } }
    }




    class Undead : Ichara
    {
        Random random = new Random();
        private string _name;
        private int _hp;
        private int _minDamage;
        private int _maxDamage;
        public void RandNum()
        {
          int result = random.Next(0, 3);
            switch (result)
            {
                case 0:
                    UndeadCreate("Zombi", 8, 4, 6);
                    break;
                case 1:
                    UndeadCreate("Undead dog", 6, 6, 8);
                    break;
                case 2:
                    UndeadCreate("Legless undead", 5, 2, 3);
                    break;
                case 3:
                    UndeadCreate("Blind undead", 10, 1, 3);
                    break;
            }
        }

        public void UndeadCreate(string Name, int Hp, int MinDamage, int MaxDamage)
        {
            _name = Name;
            _hp = Hp;
            _minDamage = MinDamage;
            _maxDamage = MaxDamage;
        }
        
        public string Name { get { return _name; } set { _name = value; } }
        public int minDamage { get { return _minDamage; } set { _minDamage = value; } }
        public int maxDamage { get { return _maxDamage; } set { _maxDamage = value; } }
        public int hp { get { return _hp; } set { _hp = value; } }

    }

   
       
}
