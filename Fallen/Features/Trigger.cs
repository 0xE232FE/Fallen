﻿#region

using Fallen.API;
using Memory;
using System.Threading;

#endregion

namespace Fallen.Features
{
    internal class Trigger
    {
        internal void Run()
        {
            while (true)
            {
                ///TO DO
                ///Add World2Screen and Raytrace and add VIS Check, not expected soon!

                Thread.Sleep(1);

                if (Settings.Trigger.Enabled)
                {
                    var entityInCrossId = MemoryManager.ReadMemory<int>(SDK.LocalPlayer.m_iBase + Offsets.m_iCrosshairId);
                    if (entityInCrossId != 0)
                    {
                        var entityBase = MemoryManager.ReadMemory<int>(MainClass.ClientPointer + Offsets.dwEntityList + (entityInCrossId - 1) * 0x10);
                        var entityTeam = MemoryManager.ReadMemory<int>(entityBase + Offsets.m_iTeamNum);

                        if (Settings.Trigger.Key && entityTeam != SDK.LocalPlayer.m_iTeam)
                        {
                            MemoryManager.WriteMemory<bool>(MainClass.ClientPointer + Offsets.dwForceAttack, true);
                            Thread.Sleep(1);
                            MemoryManager.WriteMemory<bool>(MainClass.ClientPointer + Offsets.dwForceAttack, false);
                        }
                    }
                }
            }
        }
    }
}