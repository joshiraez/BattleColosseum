using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public class Character
    {
        public Class classType { get; private set; }

        public bool isAlive { get; private set; }
        public byte[] stats_values { get; private set; }
        public byte[] stats_growths { get; private set; }
        public byte level { get; private set; }
        public short exp { get; private set; }

        public Weapon weaponEquipped { get; set; }

        private List<Ability> abilities;

        private static byte INITIAL_BOOST_RANDOM_GROWTH = 200;
        private static byte INITIAL_BOOST_LEVEL = 10;

        private Character() { }

        public Character(Class classType)
        {
            this.classType = classType;

            this.stats_values = classType.getBaseStats();
            this.stats_growths = classType.getBaseGrowths();
            this.weaponEquipped = classType.getBaseWeapon();
            this.abilities = classType.getBaseAbilities().ToList<Ability>();

            this.isAlive = true;
            this.level = 1;
            this.exp = 0;

            this.initialGrowthBoost();
            this.initialLevelBoost();

        }



        private void initialGrowthBoost()
        {
            //This can be improved. Right now, because of rounding, I can't be sure it will exactly sum 200.
            byte[] rolls = RNG.getInstance().rollStats();
            int sum = rolls.Cast<int>().Sum();
            float pointPerRoll = (float)INITIAL_BOOST_RANDOM_GROWTH / sum;
            rolls = rolls.Select(roll => (byte)Math.Round(roll*pointPerRoll)).ToArray<byte>();
            stats_growths.Zip(rolls, (baseGrowth, extraGrowth) => baseGrowth + extraGrowth);
        }

        private void initialLevelBoost()
        {
            for (int boosts=1; boosts <= INITIAL_BOOST_LEVEL; boosts++)
            {
                this.growthCheck();
            }
        }

        public void addExp(int exp)
        {
            int sumExp = this.exp + exp;
            this.exp = (byte)(sumExp % 100);
            int levelUps = sumExp / 100;
            for(int i=1; i<=levelUps; i++)
            {
                levelUp();
            }
        }

        private void levelUp()
        {
            this.level++;
            this.growthCheck();
        }

        private void growthCheck()
        {
            byte[] rolls = RNG.getInstance().rollStats();

            for(int stat=0; stat < this.stats_growths.Length; stat++)
            {
                if (rolls[stat] < stats_growths[stat] % 100)
                {
                    this.stats_values[stat]++;
                }
                //For over 100 percent growths.
                stats_values[stat] += (byte)(stats_growths[stat] / 100); 
            }
        }

        public bool deathCheck()
        {
            if (RNG.getInstance().roll() < stats_values[(int)STATS.LCK])
            {
                return false;
            }else
            {
                this.isAlive = false;
                return true;
            }
        }

        public int dmg()
        {
            return this.weaponEquipped.getAttack() + this.stats_values[(int)STATS.POW];
        }

        public int hit()
        {
            return this.weaponEquipped.getHit() + this.stats_values[(int)STATS.SKL] * 2 + this.stats_values[(int)STATS.LCK];
        }

        public int avd()
        {
            return this.stats_values[(int)STATS.SPD] * 2 + this.stats_values[(int)STATS.LCK];
        }

        public int crt()
        {
            return this.weaponEquipped.getCrit() + this.stats_values[(int)STATS.SKL];
        }

        public int crtAvd()
        {
            return this.stats_values[(int)STATS.LCK];
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
