using System;
using System.Linq;
using System.Threading;
using Memory.Win64;
using Memory.Utils;
using System.Diagnostics;


namespace SDcheat
{
    class SDcheat
    {
        private static MemoryHelper64 helper;
        private static ulong targetAdd;

        static void Main()
        {

            Process p = Process.GetProcessesByName("Stardew Valley").FirstOrDefault(); //Get the Stardew Process
            if (p == null)
            {
                Console.WriteLine("Stardew Dew.exe not detected.\n");
                Console.WriteLine("Press Enter to close...");
                Console.ReadLine();
                return;
            }
            helper = new MemoryHelper64(p);
            ulong moduleBaseAddress = 0;
            ulong moduleBase = getModuleBaseAddress();
            ulong getModuleBaseAddress () {
            
            foreach (ProcessModule module in p.Modules)
            {
                if (module.ModuleName == "System.Private.Xml.dll")//You can change the module name to whatever CheatEngine finds.
                {
                    moduleBaseAddress = (ulong)module.BaseAddress.ToInt64();// Get the module used and convert its base address to Int64
                }
            }
                return moduleBaseAddress;
            }
           
            ulong process = moduleBase + 0x000221B8;//pointer and module
            int[] offset = {0x0, 0xB8, 0x10, 0x12C, 0x0, 0x458, 0x2D4};// Offsets gotten from Cheat Engine.
            targetAdd = MemoryUtils.OffsetCalculator(helper, process, offset);
            
            Thread addMoney = new Thread(Money);
            addMoney.Start();
            System.Threading.Thread.Sleep(50);
            Console.Write("Cheat Active. Setting money to ");
            Console.Write(helper.ReadMemory<int>(targetAdd).ToString());// read memory values and write in console.
            Console.ReadLine();
            
        }
    private static void Money()
        {
           while (true) 
           {
                helper.WriteMemory<int>(targetAdd, 99999999);
                
           }

        } 
    }
}
