using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public class Character
    {
        Class classType { get; set; }

        bool isAlive { get; set; }
        byte[] stats_values { get; set; }
        byte[] stats_growths { get; set; }
        byte level { get; set; }
        short exp { get; set; }

        Weapon weaponEquipped { get; set; }

        List<Ability> abilities;

        public Character()
        {
            abilities = new List<Ability>(0);
        }

        public bool hasAbility(Ability ability)
        {
            return abilities.Contains(ability);
        }

        public void removeAbility(Ability ability)
        {
            abilities.Remove(ability);
        }

        public void addAbility(Ability ability)
        {
            abilities.Add(ability);
        }
    }
}
