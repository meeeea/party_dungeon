using System;
using System.Diagnostics;
using System.Reflection.Emit;

abstract class Damage {
    private int typeId = 0;
    private int _amount = 0;
    public int amount => _amount;

    public virtual Damage ApplyResistance(int TypeId, int flatValue, bool canFullBlock = false) {
        if (typeId == TypeId) {
            _amount -= flatValue;
            if (_amount < 1) {
                if (!canFullBlock) {
                    _amount = 1;
                }
            }
        }
        if (_amount < 0) {
            _amount = 0;
        }
        return this;
    }

    public virtual Damage ApplyResistance(int TypeId, float percentage, bool canFullBlock = false) {
        if (typeId == TypeId) {
            _amount += (int) Math.Ceiling(_amount * - percentage);
            if (_amount < 1) {
                if (!canFullBlock) {
                    _amount = 1;
                }
            }
        }
        if (_amount < 0) {
            _amount = 0;
        }
        return this;
    }

    class Bludgeoning : Damage {
        new private static int typeId => 1; 
    }

    class Piercing : Damage {
        new private static int typeId => 2;
    }

    class Slashing : Damage {
        new private static int typeId => 3;
    }
}