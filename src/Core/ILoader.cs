﻿#region License
/* 
 * Copyright (C) 1999-2015 John Källén.
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

using Decompiler.Core;
using Decompiler.Core.Assemblers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decompiler.Core
{
    /// <summary>
    /// Implementors assume responsibility for loading project files or binaries.
    /// </summary>
    public interface ILoader
    {
        string DefaultToFormat { get; set; }
        event EventHandler<ProgramEventArgs> ProgramLoaded;
        event EventHandler<TypeLibraryEventArgs> TypeLibraryLoaded;

        byte[] LoadImageBytes(string fileName, int offset);
        Program LoadExecutable(string fileName, byte[] bytes, Address loadAddress);
        Program AssembleExecutable(string fileName, string asmName, Address loadAddress);
        Program AssembleExecutable(string fileName, Assembler asm, Address loadAddress);
        Program AssembleExecutable(string fileName, byte[] bytes, Assembler asm, Address loadAddress);

        TypeLibrary LoadMetadata(string fileName);
    }

    public class ProgramEventArgs : EventArgs
    {
        public ProgramEventArgs(Program program)
        {
            this.Program = program;
        }

        public Program Program { get; private set; }
    }

    public class TypeLibraryEventArgs : EventArgs
    {
        public TypeLibraryEventArgs(TypeLibrary typelib)
        { 
            this.TypeLibrary = typelib; 
        }

        public TypeLibrary TypeLibrary { get; private set; }
    }
}
