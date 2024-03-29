﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenu
    {
        event EventHandler<MenuEventArgs> MenuItemSelected;
        event EventHandler<MenuEventArgs> MenuItemSelectionChanged;

        event EventHandler<MenuItemRunEventArgs> MenuItemRunStarted;
        event EventHandler<MenuItemRunEventArgs> MenuItemRunComplete;

        Type ContainerType { get; set; }
        Type ItemAttributeType { get; set; }
        bool Selected { get; set; }
        string Name { get; }
        string DisplayName { get; }
        string Selector { get; }
        string HeaderText { get; }
        string FooterText { get; }

        ConsoleKey ExitKey { get; }

        IMenuItem? SelectedItem { get; }
        int SelectedItemIndex { get; }
        IEnumerable<IMenuItem> Items { get; }

        MenuSpec GetSpec();

        void UnselectAll();

        IMenuItem? GetItem(string selector);

        IMenuItem? SelectItem(IMenuInput menuInput);
        IMenuItem? SelectItem(string itemSelector);
        IMenuItem? SelectItem(int index);
        IMenuItem? SelectItemNumber(int itemNumber);
        IMenuItem? SelectNextItem();
        IMenuItem? SelectPreviousItem();
        IMenuItemRunResult RunItem(IMenuInput menuInput);
        IMenuItemRunResult RunItem(IMenuItem menuItem, IMenuInput? menuInput = null);
    }
}
