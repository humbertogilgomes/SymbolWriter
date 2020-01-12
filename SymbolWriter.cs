using System;
using System.Collections.Generic;
using System.IO;

namespace Writer
{
    /// <summary>
    /// Used to store the atomic symbols and convert words to atomic symbols
    /// </summary>
    class AtomicReader
    {
        public List<string> symbols, names;

        /// <summary>
        /// Loads the symbols and the names of the atoms from a file.
        /// </summary>
        public void LoadSymbolsAndNames()
        {
            try
            {
                //Load the file with the atom symbols and split it by commas
                symbols = new List<string>(File.ReadAllText("AtomSymbols.csv").Split(','));
                //Load the file with the atom names and split it by commas
                names = new List<string>(File.ReadAllText("AtomNames.csv").Split(','));
            }
            catch (Exception)
            {
                //The file failed to open
                Console.WriteLine("Unable to open file \"AtomSymbols.csv\" and/or \"AtomNames.csv\", where the atomic symbols are stored.");
                Environment.Exit(-1);
            }
        }

        /// <summary>
        /// Returns the possible outcome of 2 characters
        /// </summary>
        /// <returns>The possible.</returns>
        Tuple<int, int> GetPossible(char c1, char c2)
        {
            //Get the characters to the periodic table standard (Upper and lower case. Eg.: Hg)
            c1 = Char.ToUpper(c1);
            c2 = Char.ToLower(c2);
            //Check if there are character matches
            int c1index = symbols.IndexOf(c1.ToString());
            int c2index = symbols.IndexOf(c1.ToString() + c2.ToString());
            //Return the number of matches
            return new Tuple<int, int>(c1index, c2index);
        }

        /// <summary>
        /// Gets a part of a string between 2 character indexes
        /// </summary>
        string GetPart(string str, int start, int end)
        {
            string ret = "";
            for (int i = start; i < end; i++)
                ret += str[i];
            return ret;
        }

        /// <summary>
        /// Converts a string to atomic symbols
        /// </summary>
        public List<int> Convert(string word)
        {
            //Check if there's any match with the first or second character
            Tuple<int, int> res;
            if (word.Length >= 2)
                res = GetPossible(word[0], word[1]);
            else
                res = GetPossible(word[0], '\0');

            //There's no match. Impossible to convert the first two characters
            if (res.Item1 == -1 && res.Item2 == -1)
                return new List<int>();

            //If the 2 first characters match to an atomic symbol
            if (res.Item2 != -1)
            {
                //Get the rest of the string to process those characters
                string ToFeed = GetPart(word, 2, word.Length);
                if (ToFeed != "")
                {
                    //Process the rest of the string
                    List<int> ret = new List<int>();
                    ret.Add(res.Item2);
                    List<int> NewSymbols = Convert(ToFeed);
                    for (int i = 0; i < NewSymbols.Count; i++)
                        ret.Add(NewSymbols[i]);
                    if (NewSymbols.Count != 0)
                        return ret;
                }
                else
                    return new List<int>() { res.Item2 };
            }

            //Check if only the 1st character matches
            if (res.Item1 != -1)
            {
                //Get the rest of the string to process those characters
                string ToFeed = GetPart(word, 1, word.Length);
                if (ToFeed != "")
                {
                    //Process the rest of the string
                    List<int> ret = new List<int>();
                    ret.Add(res.Item1);
                    List<int> NewSymbols = Convert(ToFeed);
                    for (int i = 0; i < NewSymbols.Count; i++)
                        ret.Add(NewSymbols[i]);
                    if (NewSymbols.Count != 0)
                        return ret;
                }
                else
                    return new List<int>() { res.Item1 };
            }
            return new List<int>();
        }
    }

    /// <summary>
    /// The class where the entry point is.
    /// </summary>
    static class MainClass
    {
        /// <summary>
        /// Writes the help message (when you have wrong arguments or run SymbolWriter -help).
        /// </summary>
        static void WriteHelpMessage()
        {
            Console.WriteLine("Error. SymbolWriter Usage: \"mono SymbolWriter.exe [words]\" to write words as atomic symbols or \"mono SymbolWriter.exe -help\" to get this message.");
        }

        /// <summary>
        /// The entry point of the program.
        /// </summary>
        static void Main(string[] arguments)
        {
            //Check for the right number of arguments (1 or more words)
            if (arguments.Length == 0)
            {
                WriteHelpMessage();
                Environment.Exit(-1);
            }
            //Check if the user wanted help
            if (arguments[0] == "-help")
            {
                WriteHelpMessage();
                Environment.Exit(-1);
            }
            //Join all the words into an array. This also separates arguments with multiple words.
            List<string> words = new List<string>();
            for (int i = 0; i < arguments.Length; i++)
            {
                string[] ArgumentWords = arguments[i].Split(' ');
                for (int j = 0; j < ArgumentWords.Length; j++)
                {
                    if (ArgumentWords[j] != "")
                        words.Add(ArgumentWords[j]);
                }
            }

            //Load the atomic elements
            AtomicReader reader = new AtomicReader();
            reader.LoadSymbolsAndNames();

            for (int i = 0; i < words.Count; i++)
            {
                //Convert the word to atoms
                List<int> converted = reader.Convert(words[i]);
                //Print the atoms to the console
                string ToPrint = words[i] + ": ";
                //The list is empty, there is no way of writing a word with element symbols
                if (converted.Count == 0)
                {
                    ToPrint += "Impossible to convert";
                    Console.WriteLine(ToPrint);
                }
                else
                {
                    //Write atom symbols
                    for (int j = 0; j < converted.Count; j++)
                    {
                        ToPrint += reader.symbols[converted[j]];
                        //Don't write the last space
                        if (j != converted.Count - 1)
                            ToPrint += ' ';
                    }
                    ToPrint += "; ";

                    //Write atom names
                    for (int j = 0; j < converted.Count; j++)
                    {
                        ToPrint += reader.names[converted[j]];
                        //Don't write the last space
                        if (j != converted.Count - 1)
                            ToPrint += ' ';
                    }
                    ToPrint += "; ";

                    //Write element numbers (Z)
                    for (int j = 0; j < converted.Count; j++)
                    {
                        ToPrint += (converted[j] + 1).ToString();
                        //Don't write the last space
                        if (j != converted.Count - 1)
                            ToPrint += ' ';
                    }

                    //Print everything to the console
                    Console.WriteLine(ToPrint);
                }
            }
        }
    }
}