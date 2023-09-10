using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using MyDll;

namespace Dz05._09._2023 {
    public class Program {
        static void Main(string[] args) {
            FlashMemory flash = new FlashMemory("Самсунг", "Г2", "Шиш", 512, 100, 300);
            DVD dvd = new DVD("Ксиоми", "D-1", "Диск", 64, 50, 40);
            HDD hdd = new HDD("Вестерн диджитал", "13у2", "название", 1024, 500, 200);
            Storage store = new Storage();
            store.AddList(flash);
            store.AddList(dvd);
            store.AddList(hdd);
            store.PrintList();
            MyFile.SaveToFile(store);
            Console.ReadKey();
        }
    }
}
