abstract class Equipment {
    public Equipment() {

    }

    public abstract void OnTurnStart(Entity entity);

    public abstract Action GetActions();

    public abstract void OnKill(Entity entity);

    public abstract void OnTurnEnd(Entity entity);

    public abstract void OnSufferHit(Entity entity, Damage damage);
}