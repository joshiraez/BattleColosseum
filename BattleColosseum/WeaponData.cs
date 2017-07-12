using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public static class WeaponData
    {
        public static byte getAttack(this Weapon weapon)
        {
            return weaponData[(int)weapon].atk;
        }

        public static byte getHit(this Weapon weapon)
        {
            return weaponData[(int)weapon].hit;
        }

        public static byte getCrit(this Weapon weapon)
        {
            return weaponData[(int)weapon].crt;
        }

        public static bool isMagic(this Weapon weapon)
        {
            return weaponData[(int)weapon].isMagic;
        }

        private static WeaponStats[] weaponData =
        {
            new WeaponStats
            (
                Weapon.IRON_SWORD,
                5,
                85,
                0,
                false,
                WeaponType.SWORD
            )
        };

        private struct WeaponStats
        {
            public Weapon weaponName;
            public byte atk { get; }
            public byte hit { get;  }
            public byte crt { get;  }
            public bool isMagic { get;  }
            public WeaponType type { get; }
            //Abilities?

            public WeaponStats(Weapon weaponName, byte atk, byte hit, byte crt, bool isMagic, WeaponType type)
            {
                this.weaponName = weaponName;
                this.atk = atk;
                this.hit = hit;
                this.crt = crt;
                this.isMagic = isMagic;
                this.type = type;
            }
        }


    }
}
