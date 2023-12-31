
//��H���A���Aabstract
public abstract class BaseState
{
    //�@�ӫO�@���ܼơA�i�x�sEnemy����A�L����l�Ʀb��k��
    protected Enemy currentEnemy;

    //BaseState ��H���O�����@�ө�H��k�A�Ӥ�k�����@�� Enemy �ѼơA�åB�S�����骺��{�C�o��ܥ����~�Ӧ� BaseState �����骬�A���ݭn��{�o�Ӥ�k�C
    public abstract void OnEnable(Enemy enemy);

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void OnExit();

}
