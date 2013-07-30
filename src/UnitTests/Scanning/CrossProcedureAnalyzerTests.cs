﻿#region License
/* 
 * Copyright (C) 1999-2013 John Källén.
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
using Decompiler.Scanning;
using Decompiler.UnitTests.Mocks;
using NUnit.Framework;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decompiler.UnitTests.Scanning
{
    [TestFixture]
    public class CrossProcedureAnalyzerTests
    {
        [Test]
        public void Crpa_Analyze_SimpleProc()
        {
            var prog = Given_Simple_Proc();

            var crpa = new CrossProcedureAnalyzer(null);
            crpa.Analyze(prog.Procedures.Values.First());
        }

        private Program Given_Simple_Proc()
        {
            var b = new ProgramBuilder();
            b.Add("bob", m =>
            {
                m.Label("Zlon");
                m.Return();
            });
            return b.BuildProgram();
        }
    }
}
