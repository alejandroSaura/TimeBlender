abstract public class FSMState  <T> //T will point to the owner of the FSM
{
	abstract public void Enter (T entity);

	abstract public void Execute (T entity);

	abstract public void Exit (T entity);
}
