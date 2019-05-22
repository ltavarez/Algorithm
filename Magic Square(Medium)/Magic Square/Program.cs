using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Magic_Square
{

    class Solution
    {
        static int row1 = 0;
        static int row2 = 0;
        static int row3 = 0;
        static int column1 = 0;
        static int column2 = 0;
        static int column3 = 0;
        static int primaryDiagonal = 0;
        static int secondaryDiagonal = 0;
        static int changesValue = 0;

        static void CalculateMatrizValue(int m, int n, int[,] s)
        {
            row1 = 0;
            row2 = 0;
            row3 = 0;
            column1 = 0;
            column2 = 0;
            column3 = 0;
            primaryDiagonal = 0;
            secondaryDiagonal = 0;
            // Get the sum of diferent rows , columns and diagonals
            if (m == n)
            {
                for (int row = 0; row < m; row++)
                {
                    for (int column = 0; column < n; column++)
                    {
                        if (row == 0)
                        {
                            row1 += s[row, column];
                        }

                        if (row == 1)
                        {
                            row2 += s[row, column];
                        }

                        if (row == 2)
                        {
                            row3 += s[row, column];
                        }

                        if (column == 0)
                        {
                            column1 += s[row, column];
                        }

                        if (column == 1)
                        {
                            column2 += s[row, column];
                        }

                        if (column == 2)
                        {
                            column3 += s[row, column];
                        }

                        if (row == column)
                        {
                            primaryDiagonal += s[row, column];
                        }

                        if ((row == 0 && column == 2) || (row == 1 && column == 1) || (row == 2 && column == 0))
                        {
                            secondaryDiagonal += s[row, column];
                        }

                    }
                }
            }
        }

        // Complete the formingMagicSquare function below.
        static int formingMagicSquare(int[,] s)
        {
            int m = s.GetLength(0);
            int n = s.GetLength(1);

            // Get the sum of diferent rows , columns and diagonals
            CalculateMatrizValue(m, n, s);

            // Calculate the magic number of the matriz.
            int[] ElementSums = new int[] { row1, row2, row3, column1, column2, column3, primaryDiagonal, secondaryDiagonal };
            int magicNumber = 0;
            int coincidence = 0;

            for (int i = 0; i < ElementSums.Length; i++)
            {
                if (magicNumber < ElementSums[i])
                {
                    magicNumber = ElementSums[i];
                }
            }

            for (int i = 0; i < ElementSums.Length; i++)
            {
                if (magicNumber == ElementSums[i])
                {
                    coincidence++;
                }
            }

            if(coincidence < (ElementSums.Length / 2) && coincidence > 1)
            {
                magicNumber++;
            }          

            // Change the matriz to convert in magic square
            if (m == n)
            {
                for (int row = 0; row < m; row++)
                {
                    for (int column = 0; column < n; column++)
                    {
                        int[] affectElement = new int[8];
                        int element = 0;

                        if (row == 0)
                        {
                            affectElement[element] = row1;
                            element++;
                        }

                        if (row == 1)
                        {
                            affectElement[element] = row2;
                            element++;
                        }

                        if (row == 2)
                        {
                            affectElement[element] = row3;
                            element++;
                        }

                        if (column == 0)
                        {
                            affectElement[element] = column1;
                            element++;
                        }

                        if (column == 1)
                        {
                            affectElement[element] = column2;
                            element++;
                        }

                        if (column == 2)
                        {
                            affectElement[element] = column3;
                            element++;
                        }

                        if (row == column)
                        {
                            affectElement[element] = primaryDiagonal;
                            element++;
                        }

                        if ((row == 0 && column == 2) || (row == 1 && column == 1) || (row == 2 && column == 0))
                        {
                            affectElement[element] = secondaryDiagonal;
                            element++;
                        }

                        int accumulator = 0;

                        for (int i = 0; i < affectElement.Length; i++)
                        {
                            int actualElement = affectElement[i];

                            if(actualElement == 0)
                            {
                                continue;
                            }

                            int diffMagicNumber = magicNumber - actualElement;
                            bool canChange = true;

                            for (int j = 0; j < affectElement.Length; j++)
                            {
                                int interactionElement = affectElement[j];

                                if (interactionElement == 0)
                                {
                                    continue;
                                }

                                interactionElement += diffMagicNumber + accumulator;

                                

                                if (interactionElement > magicNumber)
                                {
                                    canChange = false;
                                    break;
                                }

                            }

                            if (canChange)
                            {
                                accumulator += diffMagicNumber;
                            }

                        }

                        changesValue += accumulator;

                        int changeNumber = s[row, column] + accumulator;

                        if(changeNumber > 0 && changeNumber < 10)
                        {
                            s[row, column] += accumulator;
                        }                        
                        // Recalculate the sum of diferent rows , columns and diagonals
                        CalculateMatrizValue(m, n, s);

                    }
                }
            }

            return changesValue;
        }

        static void Main(string[] args)
        {
            //  TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int[,] s = new int[3, 3];

            s[0, 0] = 2;
            s[0, 1] = 9;
            s[0, 2] = 8;
            s[1, 0] = 4;
            s[1, 1] = 2;
            s[1, 2] = 7;
            s[2, 0] = 5;
            s[2, 1] = 6;
            s[2, 2] = 7;

            int result = formingMagicSquare(s);

            //   textWriter.WriteLine(result);

            //   textWriter.Flush();
            //  textWriter.Close();
        }
    }

}


