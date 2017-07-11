using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public static class ClassData
    {

        public static Class getRandomClass()
        {
            Random rng = new Random();
            return classData[rng.Next(classData.Length)].className;
        }

        public static byte[] getBaseGrowths (this Class className)
        {
            return classData[(int)className].baseGrowths;
        }

        public static byte[] getBaseStats(this Class className)
        {
            return classData[(int)className].baseStats;
        }

        public static Weapon getBaseWeapon(this Class className)
        {
            return classData[(int)className].baseWeapon;
        }

        public static Ability[] getBaseAbilities(this Class className)
        {
            return classData[(int)className].baseAbilities;
        }


        //Really thinking this can be heavily improved. For now, it's okay for easy access, but in the future
        //I should externalize the data. All the data in a static class will be loaded in RAM even if it's not used.
        //Not desirable, when it's just a dictionary of data.

        public static ClassStats[] classData =
        {
            new ClassStats
                (
                    Class.WARRIOR, // 0
                    new byte[] { 50, 30, 30, 20, 20, 30, 20  },
                    new byte[] { 10, 0, 0, 0, 0, 0, 0  },
                    Weapon.IRON_SWORD,
                    new Ability[] { }
                )
        };

        public struct ClassStats
        {
            public Class className { get; }
            public byte[] baseGrowths { get; }
            public byte[] baseStats { get; }
            public Weapon baseWeapon { get; }
            public Ability[] baseAbilities { get; }

            public ClassStats(Class className, byte[] baseGrowths, byte[] baseStats, Weapon baseWeapon, Ability[] baseAbilities)
            {
                this.className = className;
                this.baseGrowths = baseGrowths;
                this.baseStats = baseStats;
                this.baseWeapon = baseWeapon;
                this.baseAbilities = baseAbilities;
            }
        }
    }
}
