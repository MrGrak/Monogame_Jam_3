using System;
using System.Collections.Generic;
using System.Text;

namespace LowBudgetJam
{
    public abstract class Screen
    {
        public String Name = "Base Screen";
        public DisplayState displayState;
        public ExitAction exitAction;

        public Screen() { }
        public virtual void Open() { }
        public virtual void Close(ExitAction EA) { }
        public virtual void HandleInput() { }
        public virtual void Update() { }
        public virtual void Draw() { }
    }
}
