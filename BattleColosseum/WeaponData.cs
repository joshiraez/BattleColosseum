using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public static class WeaponData
    {
        public struct WeaponStats
        {
            byte atk { get; set; }
            byte hit { get; set; }
            byte crt { get; set; }
            bool isMagic { get; set; }
            WeaponType type { get; }
            //Abilities?

            public WeaponStats(byte atk, byte hit, byte crt, bool isMagic, WeaponType type)
            {
                this.atk = atk;
                this.hit = hit;
                this.crt = crt;
                this.isMagic = isMagic;
                this.type = type;
            }
        }


    }
}
