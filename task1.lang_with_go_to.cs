using System;
using System.IO;
using System.Text;

namespace Lab1
{
    class Task1
    {
        static void Main(string[] args)
        {
            int wordsLength = 0;
            string[] stopWords =
            {
                "the", "in", "on", "a",
                "an"
            };
            int j = 0;


            string inputTextFromFile = File.ReadAllText(@"C:\Users\korol\RiderProjects\ConsoleApp1\ConsoleApp1\text.txt");

            int inputLength = 0;
            int inputWordLength = 0;
            input:
            if ((inputTextFromFile[inputLength] == ' ' || inputTextFromFile[inputLength] == '\r' || inputTextFromFile[inputLength] == '\n'))
            {
                inputWordLength++;
            }

            inputLength++;
            if (inputLength != inputTextFromFile.Length - 1)
                goto input;

            string[] wordsInFile = new string[inputWordLength];


            int splIndex = 0;
            int arrIndex = 0;
            string splWord = "";
            splitCycle:
            // типичная проверка на разделение между словами
            if ((inputTextFromFile[splIndex] == ' ' || inputTextFromFile[splIndex] == '\r' || inputTextFromFile[splIndex] == '\n'))
            {
                if (splWord.Length != 0)
                {
                    wordsInFile[arrIndex++] = splWord;
                    splWord = "";
                }
            }
            else
            {
                if (inputTextFromFile[splIndex] >= (int) 'A' && inputTextFromFile[splIndex] <= (int) 'z')
                {
                    if (inputTextFromFile[splIndex] >= (int) 'A' && inputTextFromFile[splIndex] <= (int) 'Z')
                    {
                        splWord += (char) (inputTextFromFile[splIndex] + 32);
                        // сразу же приводим все слова к маленьким буквам
                    }
                    else
                    {
                        splWord += inputTextFromFile[splIndex];
                    }
                }
            }

            splIndex++;
            if (splIndex != inputTextFromFile.Length)
                goto splitCycle;

            // adiitional variables 
            
            string currentWord = " ";
            int wordsCapacity = 4;
            Pair[] words = new Pair[wordsCapacity];
            bool foundInStopWords = false;
            int wordIndex = 0;
            
            whileTrue:
            if (wordIndex >= wordsInFile.Length)
                goto afterWhileTrue;
            currentWord = wordsInFile[wordIndex];
            wordIndex++;
            if (wordIndex > wordsInFile.Length)
            {
                goto afterWhileTrue;
            }
            
            // Пррверям наше слово со стоп-словом
            j = 0;
            foundInStopWords = false;
            filterStopWords:
            if (j >= stopWords.Length)
            {
                goto afterFilterWords;
            }

            if (currentWord == stopWords[j])
            {
                foundInStopWords = true;
            }
            j++;
            goto filterStopWords;
            
            afterFilterWords:

            
            if (!foundInStopWords)
            {
                bool found = false;
                j = 0;
                // пробегаемся по массиву слов и ищем одинаковые
                countingSimilars:
                if (j >= wordsLength)
                {
                    goto afterCountingSimilars;
                }

                if (words[j].Word == currentWord)
                {
                    words[j].Count++;
                    found = true;
                }

                j++;
                goto countingSimilars;
                afterCountingSimilars:
                if (!found)
                {
                    if (wordsLength + 1 > wordsCapacity)
                    {
                        wordsCapacity *= 2;
                        Pair[] tmpWords = new Pair[wordsCapacity];
                        int z = 0;
                        copyingWords:
                        if (z >= wordsLength)
                        {
                            goto afterCopyingWords;
                        }

                        tmpWords[z] = words[z];
                        z++;
                        goto copyingWords;
                        afterCopyingWords:
                        words = tmpWords;
                    }

                    words[wordsLength] = new Pair();
                    words[wordsLength].Word = currentWord;
                    words[wordsLength].Count = 1;
                    wordsLength++;
                }
            }

            goto whileTrue;
            afterWhileTrue:
            // Buble Sort
            int i;
            i = 0;
            upperFor:
            if (i >= wordsLength - 1)
            {
                goto afterUpperFor;
            }

            j = i + 1;
            innerFor:
            if (j >= wordsLength)
            {
                goto afterInnerFor;
            }

            if (words[i].Count < words[j].Count)
            {
                Pair dump;
                dump = words[i];
                words[i] = words[j];
                words[j] = dump;
            }

            j++;
            goto innerFor;
            afterInnerFor:
            i++;
            goto upperFor;
            afterUpperFor:

            i = 0;
            outputCycle:
            if (!(i < wordsLength) )
            {
                goto finishMetod;
            }
            // После сортировки выводим
            Console.WriteLine(words[i].Word + " " + words[i].Count);
            i++;
            goto outputCycle;
            finishMetod:
            // без ретерна метку нельзя ласт. поставить
            return;
        }
        struct Pair
        {
            public string Word;
            public int Count;
        };
    }
}