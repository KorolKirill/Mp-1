using System;
using System.IO;
using System.Text;

namespace Lab1
{
    class Task2
    {
        static void Main(string[] args)
        {
            string currentWord = "";

            string inputText = File.ReadAllText(@"C:\Users\korol\RiderProjects\ConsoleApp1\ConsoleApp1\3123.txt");
            // amount of words counter
            int inputLength = 0;
            int inputWordLength = 0;
            input:
            if ((inputText[inputLength] == ' ' || inputText[inputLength] == '\r' || inputText[inputLength] == '\n'))
            {
                inputWordLength++;
            }
            inputLength++;
            if (inputLength != inputText.Length - 1)
                goto input;

            PageStruct[] wordsInFile = new PageStruct[inputWordLength];

            int splitIndex = 0;
            int arrayIndex = 0;
            int line = 0;
            string splitWord = "";
            split:
            {
                if (inputText[splitIndex] == '\n')
                    line++;
                if ((inputText[splitIndex] == ' ' || inputText[splitIndex] == '\r' || inputText[splitIndex] == '\n'))
                {
                    if (splitWord.Length != 0)
                    {
                        wordsInFile[arrayIndex++] = new PageStruct()
                        {
                            Word = splitWord,
                            Page = line / 45 + 1,
                            IsUsed = false,
                        };
                        splitWord = "";
                    }
                }
                else
                {
                    if (inputText[splitIndex] >= (int) 'A' && inputText[splitIndex] <= (int) 'z')
                    {
                        if (inputText[splitIndex] >= (int) 'A' && inputText[splitIndex] <= (int) 'Z')
                        {
                            splitWord += (char) (inputText[splitIndex] + 32);
                        }
                        else
                        {
                            splitWord += inputText[splitIndex];
                        }
                    }
                }

                splitIndex++;
                if (splitIndex != inputText.Length)
                    goto split;
            }

            PagesStruct[] words = new PagesStruct[inputWordLength];

            int i = 0;
            int resultWordIndex = 0;
            componing:
            {
                if (wordsInFile[i].IsUsed == false)
                {
                    int sameWordsCount = 0;

                    int j = 0;
                    findWords:
                    {
                        if (wordsInFile[i].Word == wordsInFile[j].Word)
                        {
                            wordsInFile[j].IsUsed = true;
                            sameWordsCount++;
                        }

                        j++;
                        if (j != inputWordLength)
                            goto findWords;
                    }

                    j = 0;
                    int pagesIndex = 0;
                    words[resultWordIndex] = new PagesStruct()
                    {
                        Word = wordsInFile[i].Word,
                        Pages = new int[sameWordsCount],
                    };
                    int lastPageNumber = 0;
                    addWords:
                    {
                        if (wordsInFile[i].Word == wordsInFile[j].Word)
                        {
                            if (wordsInFile[j].Page != lastPageNumber)
                            {
                                words[resultWordIndex].Pages[pagesIndex] = wordsInFile[j].Page;
                                pagesIndex++;
                            }

                            lastPageNumber = wordsInFile[j].Page;
                        }

                        j++;
                        if (j != inputWordLength)
                            goto addWords;
                    }
                    resultWordIndex++;
                }

                i++;
                if (i != inputWordLength)
                    goto componing;
            }
            // Bubble sort
            {
                i = 0;
                upperFor:
                if (i >= inputWordLength - 1)
                {
                    goto afterUpperFor;
                }

                int j = i + 1;
                innerFor:
                if (j >= inputWordLength)
                {
                    goto afterInnerFor;
                }

                if (words[i].Word == words[j].Word)
                {
                    PagesStruct tmp;
                    tmp = words[i];
                    words[i] = words[j];
                    words[j] = tmp;
                }

                j++;
                goto innerFor;
                afterInnerFor:
                i++;
                goto upperFor;
                afterUpperFor:
                int y;
            }

            i = 0;
            outputCycle:
            if (!(i < inputWordLength))
            {
                goto finish;
            }

            if (words[i].Pages?.Length < 100 && words[i].Word != "")
            {
                Console.Write(words[i].Word + " - ");
                int pageIndex = 0;
                writePages:
                {
                    if (words[i].Pages[pageIndex] != 0)
                        Console.Write(words[i].Pages[pageIndex] + ",");
                    pageIndex++;
                    if (pageIndex != words[i].Pages.Length)
                        goto writePages;
                }
                Console.WriteLine();
            }

            i++;
            goto outputCycle;
            finish:
            return;
        }
        struct PagesStruct
        {
            public string Word;
            public int[] Pages;
        };

        struct PageStruct
        {
            public string Word;
            public int Page;
            public bool IsUsed;
        };
    }
}