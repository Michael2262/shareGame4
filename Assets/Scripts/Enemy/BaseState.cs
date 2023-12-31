
//抽象狀態機，abstract
public abstract class BaseState
{
    //一個保護的變數，可儲存Enemy物件，他的初始化在方法裡
    protected Enemy currentEnemy;

    //BaseState 抽象類別中的一個抽象方法，該方法接受一個 Enemy 參數，並且沒有具體的實現。這表示任何繼承自 BaseState 的具體狀態都需要實現這個方法。
    public abstract void OnEnable(Enemy enemy);

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void OnExit();

}
