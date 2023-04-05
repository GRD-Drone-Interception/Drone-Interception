using System.Collections.Generic;
using UnityEngine;

namespace DroneCommands
{
    public class CommandInvoker : MonoBehaviour
    {
        private Stack<ICommand> _commandStack = new();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commandStack.Push(command);
        }

        public void UndoCommand()
        {
            if (_commandStack.Count == 0)
            {
                Debug.Log("No commands to undo.");
                return;
            }

            ICommand command = _commandStack.Pop();
            command.Undo();
        }
    }
}
