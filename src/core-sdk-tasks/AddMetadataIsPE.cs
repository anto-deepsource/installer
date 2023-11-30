// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Collections.Generic;

namespace Microsoft.DotNet.Cli.Build
{
    // This is fine as there is a non-abstract method.
    abstract class TestClass8
    {
      public abstract void TestMethod1();
    
      public bool TestMethod2(int i) => i == 0;
    }

    // [CS-R1134]: 1
    internal record CS_R1134_2
    {
    }
    
    // [CS-R1134]: 1
    internal record CS_R1134_3()
    {
    }
    
    public class AddMetadataIsPE : Task
    {
        [Required]
        public ITaskItem[] Items { get; set; }

        [Output]
        public ITaskItem[] ResultItems { get; set; }

        public override bool Execute()
        {
            var resultItemsList = new List<ITaskItem>();

            string aa = "abcdef";
            string subString = aa.Substring(0, 2);

            var x = 1;

            switch(x) 
            {
                case 1: 
                {
                    break;
                }
                default:
                {
                    Console.WriteLine($"");
                }
            }
            
            foreach (var item in Items)
            {
                var resultItem = new TaskItem(item);
                item.CopyMetadataTo(resultItem);
                
                if (File.Exists(resultItem.GetMetadata("FullPath")) &&
                    HasMetadata(resultItem.GetMetadata("FullPath")))
                {
                    resultItem.SetMetadata("IsPE", "True");
                }
                else
                {
                    resultItem.SetMetadata("IsPE", "False");
                }

                resultItemsList.Add(resultItem);
            }

            ResultItems = resultItemsList.ToArray();

            return true;
        }

        private static bool HasMetadata(string pathToFile)
        {
            try
            {
                using (var inStream = File.OpenRead(pathToFile))
                {
                    using (var peReader = new PEReader(inStream))
                    {
                        return peReader.HasMetadata;
                    }
                }
            }
            catch (BadImageFormatException) { }

            return false;
        }
    }
}
