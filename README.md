# SymbolWriter
A program that writes words and sentences as atomic symbols. After some atomic symbol memes like "You must be made out of copper and tellurium because you're really CuTe", I decided to create a program that checks if a word can be written using atomic symbols and which ones should be used to write it.

# MIT license and easy to compile
Compiling this program is easy. Aldo there isn't a Visual Studio / Mono Develop solution, you can easily compile the code using the command line with the default .NET compiler or with Mono:

`csc SymbolWriter.cs`

This program also uses the MIT license, which makes it easy to use this code under the condition that you include the license file.

# Notes

- This program will always generate the fastest way to write a word, using the least atomic symbols possible, always preferring the 2 character symbols (Eg.: Fe (Iron)) to the 1 character ones (Eg.: C (Carbon)). The word "cute", for example, can be written as CuTe (Copper, Tellurium) or CUTe (Carbon, Uranium and Tellurium) but this program will always prefer CuTe, the shortest way that uses less elements.
