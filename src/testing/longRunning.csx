using System;

int i = 0;
while (i++ < 10)
{
    Console.WriteLine(i);
    System.Threading.Thread.Sleep(500);
}