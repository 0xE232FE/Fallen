﻿#region

using Fallen.API;
using Memory;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Fallen.Features
{
    internal class Bunny
    {
        internal async void Run()
        {
            var JumpsDone = 0;
            while (true)
            {
                if (Settings.Bhop.Enabled)
                {
                    var flag = MemoryManager.ReadMemory<int>(LocalPlayer.m_iBase + 0x100);

                    if (flag == 257 && (Settings.Bhop.Key) || flag == 263 && (Settings.Bhop.Key))
                    {
                        if (Settings.Bhop.MaxJumps > JumpsDone || !Settings.Bhop.JumpLimit)
                        {
                            MemoryManager.WriteMemory<bool>(MainClass.ClientPointer + Offsets.dwForceJump, true);
                            JumpsDone++;
                            await Task.Delay(50);
                        }
                    }
                    else
                    {
                        MemoryManager.WriteMemory<bool>(MainClass.ClientPointer + Offsets.dwForceJump, false);
                    }

                    if (!Settings.Bhop.Key && Settings.Bhop.JumpLimit && JumpsDone > 1)
                    {
                        await Task.Delay(50);
                        JumpsDone = 0;
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}