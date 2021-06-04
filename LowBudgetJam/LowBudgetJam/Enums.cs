using System;
using System.Collections.Generic;
using System.Text;

namespace LowBudgetJam
{
    public enum DisplayState : byte
    {
        Opening, Opened, Closing, Closed
    }

    public enum MouseButtons : byte
    {
        LeftButton,
        RightButton
    }

    public enum ExitAction
    {
        Title,
        Game,
        Score,
        Exit,
    }
}