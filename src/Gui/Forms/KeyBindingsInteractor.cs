﻿#region License
/* 
 * Copyright (C) 1999-2018 John Källén.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; see the file COPYING.  If not, write to
 * the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reko.Gui.Windows.Forms;

namespace Reko.Gui.Forms
{
    public class KeyBindingsInteractor
    {
        private IKeyBindingsDialog dlg;
        private Dictionary<CommandID, string> cmdNames;
        private List<ListOption> windowNames;

        internal void Attach(IKeyBindingsDialog keyBindingsDialog)
        {
            this.dlg = keyBindingsDialog;
            keyBindingsDialog.Load += dlg_Load;
            this.cmdNames = GetCommandNames();
        }

        public CommandID SelectedCommand
        {
            get
            {
                var option = ((ListOption)dlg.Commands.SelectedItem);
                var cmdID = (CommandID)option?.Value;
                return cmdID;
            }
        }

        private void dlg_Load(object sender, EventArgs e)
        {
            this.windowNames = GetWindowNames();
            PopulateCommands();
            PopulateCommandKeys();
            PopulateWindows();
            PopulateKeyCommands();

            dlg.CommandName.TextChanged += CommandName_TextChanged;
            dlg.Commands.SelectedIndexChanged += Commands_SelectedIndexChanged;
            dlg.Shortcut.KeyUp += Shortcut_KeyUp;
        }

        private void Shortcut_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyData == System.Windows.Forms.Keys.ControlKey ||
                e.KeyData == System.Windows.Forms.Keys.ShiftKey ||
                e.KeyData == System.Windows.Forms.Keys.Menu)
            {
                return;
            }
            else
            {
                dlg.Shortcut.Text = dlg.RenderKey((int)e.KeyData);
            }
        }

        private Dictionary<CommandID, string> GetCommandNames()
        {
            return typeof(CmdIds).GetFields(BindingFlags.Public|BindingFlags.Static)
                .ToDictionary(
                    field => new CommandID(CmdSets.GuidReko, (int)field.GetValue(null)),
                    field => field.Name);
        }

        private List<ListOption> GetWindowNames()
        {
            return new ListOption[] { new ListOption { Text = "Any window", Value = "" } }
                .Concat(dlg.KeyBindings.Keys
                    .Where(windowName => !string.IsNullOrEmpty(windowName))
                    .Select(windowName => new ListOption {
                        Text = windowName,
                        Value = windowName }))
                    .ToList();
        }

        private void PopulateCommands()
        {
            dlg.Commands.Items.Clear();
            foreach (var cmdName in this.cmdNames
                .OrderBy(de => de.Value)
                .Where(de => 
                    dlg.CommandName.Text == "" ||
                    de.Value.IndexOf(
                        dlg.CommandName.Text,
                        StringComparison.InvariantCultureIgnoreCase) >= 0)
                .Select(de => new ListOption { Text = de.Value, Value = de.Key }))
            {
                dlg.Commands.Items.Add(cmdName);
            }
        }

        private void PopulateCommandKeys()
        {
            dlg.CommandKeys.Items.Clear();
            if (dlg.Commands.SelectedIndex < 0 || dlg.Commands.SelectedItem == null)
                return;
            foreach (var cmd in dlg.KeyBindings.Values.SelectMany(e => e)
                .Where(e => e.Value.ID == SelectedCommand.ID)
                .Select(e => new ListOption {  Text = dlg.RenderKey(e.Key), Value = e.Key }))
            {
                dlg.CommandKeys.Items.Add(cmd);
            }
            if (dlg.CommandKeys.Items.Count > 0)
                dlg.CommandKeys.SelectedIndex = 0;
        }

        private void PopulateWindows()
        {
            dlg.Windows.Items.Clear();
            foreach (var window in this.windowNames)
            {
                dlg.Windows.Items.Add(window);
            }
        }

        private void PopulateKeyCommands()
        {
        }

        private void CommandName_TextChanged(object sender, EventArgs e)
        {
            PopulateCommands();
        }

        private void Commands_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCommandKeys();
        }
    }
}