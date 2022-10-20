using System.Collections;
using System.Collections.Generic;

namespace CommandPattern
{
    public interface ICommand {
        void Execute();
        void ExecuteUndo();
    }

    public class Invoker
    {
        Stack<ICommand> history;
        Stack<ICommand> undone;

        public Invoker()
        {
            history = new Stack<ICommand>();
            undone = new Stack<ICommand>();
        }

        public void Execute(ICommand command, bool calledFromRedo = false)
        {
            if(command != null)
            {
                history.Push(command);
                history.Peek().Execute();

                if(!calledFromRedo) undone.Clear();
            }
        }

        public void Undo()
        {
            if(history.Count > 0)
            {
                history.Peek().ExecuteUndo();
                undone.Push(history.Peek());                
                history.Pop();
            }
        }

        public void Redo()
        {
            if (undone.Count > 0)
            {
                Execute(undone.Peek(), true);
                undone.Pop();
            }
        }

    }
}
