#region License
/* 
 * Copyright (C) 1999-2019 John Källén.
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

using Reko.Core;
using Reko.Core.Serialization;
using Reko.Core.Types;
using Reko.Arch.M68k;
using System;
using Reko.Core.Expressions;
using System.Collections.Generic;

namespace Reko.Environments.AtariTOS
{
    // The TOSCall calling convention takes all arguments on stack, ignoring 
    // the first one since it is the system call selector.
    public class TOSCallingConvention : CallingConvention
    {
        private IProcessorArchitecture arch;

        public TOSCallingConvention(IProcessorArchitecture arch)
        {
            this.arch = arch;
        }

        public void Generate(ICallingConventionEmitter ccr, DataType dtRet, DataType dtThis, List<DataType> dtParams)
        {
            int stackOffset = 4 + 4;   // Skip the system call selector + return address.
            ccr.LowLevelDetails(4, stackOffset);
            if (dtRet != null)
            {
                ccr.RegReturn(Registers.d0);
            }

            if (dtThis != null)
            {
                //ImplicitThis = null, //$TODO
                throw new NotImplementedException("C++ implicit `this` arguments are not implemented for Atari TOS.");
            }
            for (int iArg = 0; iArg < dtParams.Count; ++iArg)
            {
                ccr.StackParam(dtParams[iArg]);
            }
            // AFAIK the calling convention on Atari TOS is caller-cleanup, 
            // so the only thing we clean up is the return value on the stack.
            ccr.CallerCleanup(4);
        }

        public bool IsArgument(Storage stg)
        {
            return stg is StackStorage;
        }

        public bool IsOutArgument(Storage stg)
        {
            return Registers.d0 == stg;
        }
    }
}