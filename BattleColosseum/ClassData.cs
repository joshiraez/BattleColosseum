using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public static class ClassData
    {
        static byte[][] baseGrowths = {
            new byte[] { 50, 30, 30, 20, 20, 30, 20  } //WARRIOR - 0
        };

        static byte[][] baseStats = {
            new byte[] { 10, 0, 0, 0, 0, 0, 0  }        //WARRIOR - 0
        };

        static Weapon[] baseWeapon =
        {
            Weapon.IRON_SWORD                           //WARRIOR - 0
        };

        static Ability[][] baseAbilities =
        {
             new Ability[] { }                          //WARRIOR - 0
        };

        public static byte[] getBaseGrowths (this Class className)
        {
            return baseGrowths[(int)className];
        }

        public static byte[] getBaseStats(this Class className)
        {
            return baseStats[(int)className];
        }

        public static Weapon getBaseWeapon(this Class className)
        {
            return baseWeapon[(int)className];
        }

        public static Ability[] getBaseAbilities(this Class className)
        {
            return baseAbilities[(int)className];
        }
    }
}
