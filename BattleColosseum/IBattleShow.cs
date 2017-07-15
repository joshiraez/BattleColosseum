using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleColosseum
{
    public interface IBattleShow
    {
        //Actually, using enums to ensure only some values, makes the interface miss his point, doesn't it?.
        //Because this interface doesn't assure that every AttackType will have an attack, for example.
        void showMessage();
        void attack(AttackType attack, Battler attacker);
        void ability(Ability ability, Battler activator);
        void damage(Battler victim);
        void healing(Battler receiver);
        void showEffectMessage(Battler subject, string message);
        
    }
}
