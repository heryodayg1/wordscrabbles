using System;
using System.Collections.Generic;
using System.Linq;
using Scrambles.MagicWord;
using Scrambles.MagicWord.Data;
using Scrambles.MagicWord.Workers;

namespace Scrambles
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
         private const string wordListFileName = "wordlist.txt";
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
            Console.WriteLine("we are launching the project");



            bool continueGame = true;
            do
            {
               Console.WriteLine(Constants.OptionOnHowToEnterScrambledWords);
               var input = Console.ReadLine() ?? string.Empty;
               
               switch(input.ToUpper())
               {
                  case Constants.Manual:
                       Console.Write(Constants.EnterScrambledWordManually);
                       ManaulInputDecision();
                  break;
                  case Constants.File:
                     Console.Write(Constants.EnterScrambledsViaFile);
                     FileInputDecision();
                  break;
                  default:
                  Console.WriteLine(Constants.EnterScrambledWordsOptionNotRecognized);
                  break;
               }
                var ContinueGamenow = string.Empty;
                do
                {
                  Console.WriteLine(Constants.OptionsOnContinuingTheProgram);
                  ContinueGamenow = (Console.ReadLine() ?? string.Empty);
                }
                while(!ContinueGamenow.Equals(Constants.Yes , StringComparison.OrdinalIgnoreCase)&&
                !ContinueGamenow.Equals(Constants.No , StringComparison.OrdinalIgnoreCase));
                
                continueGame = ContinueGamenow.Equals(Constants.Yes ,StringComparison.OrdinalIgnoreCase);
            }
            while(continueGame); 
            }
            catch (Exception ex)
            {
               Console.WriteLine(Constants.ErrorProgramWillbeTerminated + ex.Message);
            }
           
        }

        private static void FileInputDecision()
        {
            var manualinput = Console.ReadLine() ?? string.Empty;
            string [] Scrambledwords= manualinput.Split(',');
            DisplayBothnewFileandManual(Scrambledwords);
        }

        private static void ManaulInputDecision()
        {
            try
            {
              var filename = Console.ReadLine() ?? string.Empty;
            string [] ScrambledWords= _fileReader.Read(filename);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(Constants.ErrorScrambledWordsCannotBeLoaded + ex.Message);

            }
            
            
        }

        public static void DisplayBothnewFileandManual(string[] scrambledWords)
        {
            string [] wordList = _fileReader.Read(wordListFileName);
           List <MatchedWord> matchedWords = _wordMatcher.Match( scrambledWords , wordList);

           if (matchedWords.Any())
           {
              foreach ( var matchedWord in matchedWords)
              {
                  Console.WriteLine(Constants.MatchFound, matchedWord.scrambledWord , matchedWord.Word);
              }

           }
           else{
               Console.WriteLine(Constants.MatchNotFound);
           }

        }

    
    }
}
