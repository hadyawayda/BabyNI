using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BabyNI
{
    internal class RadioLinkParser
    {
        readonly private static string  rootDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone",
                                        parserDirectory = Path.Combine(rootDirectory, "Parser"),
                                        parserBackupDirectory = Path.Combine(parserDirectory, "Processed"),
                                        loaderDirectory = Path.Combine(rootDirectory, "Loader");
        private HashSet<int>        disabledColumns = new HashSet<int> { 1, 9, 17 }, 
                                    staticColumns = new HashSet<int> { 2, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15, 16 }; // 13; // 3
        private string[]            output = new string[22], fetchedLine = new string[18];
        private static string?      fileName, filePath, backupFilePath, parsedFile;
        private StreamWriter        writer;
        private StreamReader        reader;
        private int                 counter = 0;

        public RadioLinkParser(string file)
        {
            fileName = file;
            filePath = Path.Combine(parserDirectory, fileName);
            backupFilePath = Path.Combine(parserBackupDirectory, fileName);  // parser/processed/radioLinkPower.txt
            parsedFile = Path.Combine(loaderDirectory, Path.GetFileNameWithoutExtension(fileName) + ".csv");        // loader/radioLinkPower.txt
            
            reader = new StreamReader(filePath!);

            writer = new StreamWriter(parsedFile!);
            
            fetchLine();
        }

        private void fetchLine()
        {
            string line;

            while ((line = reader.ReadLine()!) != null)
            {
                fetchedLine = line.Split(",")!;

                if (counter == 0)
                {
                    output = fetchedLine;

                    processOutput();

                    //Console.WriteLine("Header is being pushed to output CSV file...\n");

                    continue;
                }

                processLine(fetchedLine);
            }

            this.closeFile(fileName!);
        }

        private void processLine(string[] header)
        {
            //Console.WriteLine($"Line '{counter}' is being processed\n");

            generateFirstSetOfColumns();

            processMiddleColumns(header);

            generateSecondSetOfColumns();

            //Console.WriteLine("Line is being pushed to output CSV file...\n");

            processOutput();
        }

        private void processOutput()
        {
            // Push to a csv fileName and add line break
            writer.WriteLine(string.Join(",", output));

            //Console.WriteLine("Line has been processed, moving on to next line.\n");

            // Then empty array and update line counter
            output = new string[22];

            counter++;
        }

        private void processMiddleColumns(string[] header)
        {
            for (int i = 0; i < 18; i++)
            {
                if (disabledColumns.Contains(i + 1))
                {
                    //Console.WriteLine($"Disabled Column {i + 1} was removed on Line {counter}\n");
                    continue;
                }

                if (staticColumns.Contains(i + 1))
                {
                    output[i + 2] = header[i];
                    //Console.WriteLine($"Column {i + 1} on Line {counter} has been processed\n");
                    continue;
                }

                if ( i  == 2 )
                {
                    processUnreachableBulkFC(header[i]);
                    continue;
                }

                if ( i == 17 )
                {
                    processFailureDescription(header[i]);
                    continue;
                }
            }
        }

        private void generateFirstSetOfColumns()
        {
            generateNetworkSID();
            generateDateTimeKey();
        }

        private void generateSecondSetOfColumns()
        {

        }

        private void generateNetworkSID()
        {

        }

        private void generateDateTimeKey()
        {

        }

        private void processUnreachableBulkFC(string data)
        {

        }

        private void processFailureDescription(string data)
        {

        }

        private void closeFile(string fileName)
        {
            // Close csv writer
            writer.Close();

            // Close csv reader
            reader.Close();

            Console.WriteLine($"Parser: Parsing done on file: {fileName}\nParser: Initiating file move.\n");

            // Move and delete fileName
            movefiles(fileName);
        }

        private void movefiles(string fileName)
        {
            // This method could make use of a queue system as well, but it's not that important right now.
            if (File.Exists(backupFilePath))
            {
                File.Delete(backupFilePath);
            }

            // Move txt to archive directory
            File.Move(filePath!, backupFilePath!);

            //Console.WriteLine($"{fileName} has been moved and archived successfully.\n");
            //Console.WriteLine("Thank you for using our service :)\n");
        }
    }
}
