using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class MenuInput : IMenuInput
    {
        static MenuInput _instance;
        static object _instanceLock = new object();
        public static IMenuInput Empty
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new MenuInput());
            }
        }

        public static MenuInput FromArguments(string[] arguments)
        {
            return new MenuInput()
            {
                Input = new StringBuilder(string.Join(" ", arguments))
            };
        }

        public StringBuilder Input
        {
            get;
            set;
        }

        public bool Exit
        {
            get;
            set;
        }

        public int ExitCode
        {
            get;
            set;
        }

        public bool Enter
        {
            get;
            set;
        }

        public bool IsMenuItemNavigation
        {
            get;
            set;
        }

        public bool IsMenuNavigation
        {
            get;
            set;
        }

        public bool IsSelector
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public string Selector
        {
            get;
            set;
        }

        public int ItemNumber
        {
            get;
            set;
        }

        public bool NextItem
        {
            get;
            set;
        }

        public bool PreviousItem
        {
            get;
            set;
        }

        public bool NextMenu
        {
            get;
            set;
        }

        public bool PreviousMenu
        {
            get;
            set;
        }
    }
}
