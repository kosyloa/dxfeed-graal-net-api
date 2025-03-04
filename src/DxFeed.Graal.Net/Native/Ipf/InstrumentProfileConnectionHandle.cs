// <copyright file="InstrumentProfileConnectionHandle.cs" company="Devexperts LLC">
// Copyright © 2022 Devexperts LLC. All rights reserved.
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

using System.Runtime.InteropServices;
using DxFeed.Graal.Net.Ipf.Live;
using DxFeed.Graal.Net.Native.ErrorHandling;
using DxFeed.Graal.Net.Native.Interop;

namespace DxFeed.Graal.Net.Native.Ipf;

internal class InstrumentProfileConnectionHandle : JavaHandle
{
    public static InstrumentProfileConnectionHandle Create(string address, InstrumentProfileCollectorHandle instrumentProfileCollector) =>
        ErrorCheck.SafeCall(Import.CreateConnection(CurrentThread, address, instrumentProfileCollector));

    public string? GetAddress()
    {
        using var address = ErrorCheck.SafeCall(Import.GetAddress(CurrentThread, this));
        return address.ToString();
    }

    public long GetUpdatePeriod() =>
        ErrorCheck.SafeCall(Import.GetUpdatePeriod(CurrentThread, this));

    public void SetUpdatePeriod(long updatePeriod) =>
        ErrorCheck.SafeCall(Import.SetUpdatePeriod(CurrentThread, this, updatePeriod));

    public long GetLastModified() =>
        ErrorCheck.SafeCall(Import.GetLasModified(CurrentThread, this));

    public void Start() =>
        ErrorCheck.SafeCall(Import.Start(CurrentThread, this));

    public new void Close() =>
        ErrorCheck.SafeCall(Import.Close(CurrentThread, this));

    private static class Import
    {
        [DllImport(
            ImportInfo.DllName,
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true,
            EntryPoint = "dxfg_InstrumentProfileConnection_createConnection")]
        public static extern InstrumentProfileConnectionHandle CreateConnection(
            nint thread,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string address,
            InstrumentProfileCollectorHandle instrumentProfileCollector);

        [DllImport(
            ImportInfo.DllName,
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true,
            EntryPoint = "dxfg_InstrumentProfileConnection_getAddress")]
        public static extern JavaStringHandle GetAddress(
            nint thread,
            InstrumentProfileConnectionHandle instrumentProfileConnection);


        [DllImport(
            ImportInfo.DllName,
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true,
            EntryPoint = "dxfg_InstrumentProfileConnection_getUpdatePeriod")]
        public static extern long GetUpdatePeriod(
            nint thread,
            InstrumentProfileConnectionHandle instrumentProfileConnection);

        [DllImport(
            ImportInfo.DllName,
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true,
            EntryPoint = "dxfg_InstrumentProfileConnection_setUpdatePeriod")]
        public static extern int SetUpdatePeriod(
            nint thread,
            InstrumentProfileConnectionHandle instrumentProfileConnection,
            long updatePeriod);

        [DllImport(
            ImportInfo.DllName,
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true,
            EntryPoint = "dxfg_InstrumentProfileConnection_getLastModified")]
        public static extern long GetLasModified(
            nint thread,
            InstrumentProfileConnectionHandle instrumentProfileConnection);

        [DllImport(
            ImportInfo.DllName,
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true,
            EntryPoint = "dxfg_InstrumentProfileConnection_start")]
        public static extern int Start(
            nint thread,
            InstrumentProfileConnectionHandle instrumentProfileConnection);

        [DllImport(
            ImportInfo.DllName,
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true,
            EntryPoint = "dxfg_InstrumentProfileConnection_close")]
        public static extern int Close(
            nint thread,
            InstrumentProfileConnectionHandle instrumentProfileConnection);
    }
}
