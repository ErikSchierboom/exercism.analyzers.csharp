using System;
using System.IO;

namespace Exercism.Analyzers.CSharp.CommandLine
{
    internal static class PlatformSpecificBinary
    {
        public static string GetPath(string fileName) =>
            Path.Combine("binaries", $"{GetOperatingSystem()}-{GetArchitecture()}", fileName);
        
        private static string GetOperatingSystem()
        {
            if (OperatingSystem.IsWindows())
                return "win";
            if (OperatingSystem.IsLinux())
                return "linux";
            if (OperatingSystem.IsMacOs())
                return "osx";
            
            throw new InvalidOperationException("Unsupported operating system");
        }

        private static string GetArchitecture()
        {
            if (Architecture.IsX86())
                return "x86";
            if (Architecture.IsX64())
                return "x64";

            throw new InvalidOperationException("Unsupported architecture system");
        }
    }
}