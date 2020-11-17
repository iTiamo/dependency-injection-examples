using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dependency_injection_examples.Services
{
    public class WindowsStorageService : IStorageService
    {
        public void StoreFile()
        {
            File.WriteAllText("helloWorld.txt", "Hello, world!");
        }
    }
}
