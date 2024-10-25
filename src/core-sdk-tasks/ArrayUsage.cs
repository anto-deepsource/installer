using System;

public class ArrayUsage
{
  public void PrintArray()
  {
    ArrayDefinition arrayDef = new ArrayDefinition();
    var array = arrayDef.Numbers;
    Console.WriteLine(array.ToString());
    Console.WriteLine(arrayDef.Numbers.ToString());
  }
}
