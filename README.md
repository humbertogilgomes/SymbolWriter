# SymbolWriter
A program that writes words and sentences as atomic symbols. After some atomic symbol memes like "You must be made out of copper and tellurium because you're really CuTe", I decided to create a program that checks if a word can be written using atomic symbols and which ones should be used to write it.

# Compilation
Compiling this program is easy. Although there isn't a Visual Studio / Mono Develop solution, you can easily compile the code using the command line with the default .NET compiler or with Mono:

`csc SymbolWriter.cs`

# MIT License

This program uses the permissive MIT license, making forking it easier, given that you don't need to worry with annoying licensing.

# Notes

- This program will always generate the fastest way to write a word, using the least atomic symbols possible, always preferring the 2 character symbols (E.g.: Fe (Iron)) to the single character ones (E.g.: C (Carbon)). The word "cute", for example, can be written as CuTe (Copper, Tellurium) or CUTe (Carbon, Uranium and Tellurium). This program will always prefer CuTe, the shortest way that uses less elements.
