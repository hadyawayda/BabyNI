using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BabyNI
{
    internal class RFInputParser
    {
        readonly private static string  rootDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone",
                                        parserDirectory = Path.Combine(rootDirectory, "Parser"),
                                        parserBackupDirectory = Path.Combine(parserDirectory, "Processed"),
                                        loaderDirectory = Path.Combine(rootDirectory, "Loader"),
                                        headerPrefix = "NETWORK_SID,DATETIME_KEY",
                                        headerSuffix = "SLOT,PORT";
        readonly private HashSet<int>   disabledColumns = new HashSet<int> { 11, 13, 14, 17 }, // 3 total disabled columns
                                        staticColumns = new HashSet<int> { 3, 4, 5, 6, 7, 8, 9, 10, 12, 15 }; // 13 total static columns
        private List<string>            output, fetchedLine;
        private static string?          filePath, backupFilePath, parsedFile;
        private StreamWriter            writer;
        private StreamReader            reader;
        private int                     totalColumns, rows, lines, d, e, corruptRows, emptyCells;
        private bool                    toBeSkipped;
        private string?                 SLOT, PORT;


        public RFInputParser(string file) {
            filePath = Path.Combine(parserDirectory, file);
            backupFilePath = Path.Combine(parserBackupDirectory, file);  // parser/processed/radioLinkPower.txt
            parsedFile = Path.Combine(loaderDirectory, Path.GetFileNameWithoutExtension(file) + ".csv");        // loader/radioLinkPower.txt
            totalColumns = corruptRows = rows = lines = emptyCells = d = e = 0;
            toBeSkipped = false;
            output = new List<string>(22);
            fetchedLine = new List<string>(18);
            SLOT = PORT = null;

            reader = new StreamReader(filePath!);

            writer = new StreamWriter(parsedFile!);

            fetchLine();
        }

        private void fetchLine()
        {
            string line;

            while ( (line = reader.ReadLine()!) != null )
            {
                try
                {
                    if (lines == 0)
                    {
                        processHeader(line, headerPrefix, headerSuffix);

                        continue;
                    }

                    fetchedLine = line.Split(",").ToList();

                    processLine(fetchedLine);
                }

                catch (Exception e)
                {
                    Console.WriteLine($"Corrupt record detected. Line {lines} will be skipped.");

                    corruptRows++;

                    Console.WriteLine(e.Message);
                }
            }

            closeFile();
        }

        private void processHeader(string line, string prefix, string suffix)
        {
            line = prefix + "," + line + "," + suffix;

            fetchedLine = line.ToUpper().Split(",").ToList();

            for (int i = 0; i < fetchedLine.Count; i++)
            {
                if (!disabledColumns.Contains(i + 1))
                {
                    output.Add(fetchedLine[i]);
                }
            }

            totalColumns = output.Count;

            Console.WriteLine($"Header is: {string.Join(", ", output)}\n");

            Console.WriteLine($"Total columns count for this file is: {totalColumns}\n");

            processOutput(true);
        }

        private void processLine(List<string> data)
        {
            //Console.WriteLine($"Line '{counter}' is being processed\n");
            generateFirstSetOfColumns();
            processMiddleColumns(data);
            generateSecondSetOfColumns();
            //Console.WriteLine("Line is being pushed to output CSV file...\n");
            processOutput(false);
        }

        private void generateFirstSetOfColumns()
        {
            // Create an optional hashing function that takes the order into consideration i.e. KMP
            output.Add(Math.Abs((fetchedLine[6] + fetchedLine[7]).GetHashCode()).ToString());
            output.Add(Math.Abs((fetchedLine[3]).GetHashCode()).ToString());
        }

        private void processMiddleColumns(List<string> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == "")
                {
                    emptyCells++;
                    
                    processOutput(false);
                    
                    break;
                }

                if (disabledColumns.Contains(i + 3))
                {
                    //Console.WriteLine($"Disabled Column {i + 1} was removed on Line {counter}\n");
                    
                    continue;
                }

                if (staticColumns.Contains(i + 3))
                {
                    output.Add(data[i]);
                    
                    //Console.WriteLine($"Column {i + 1} on Line {counter} has been processed\n");
                    
                    continue;
                }

                if (i == 13)
                {
                    processFailureDescription(data[i]);
                    
                    continue;
                }
            }
        }

        private void generateSecondSetOfColumns()
        {
            if (!toBeSkipped)
            {
                generateFields_1_2();
            }

            output.Add(SLOT!);
            
            output.Add(PORT!);
        }

        private void generateFields_1_2()
        {
            string input = fetchedLine[2];
            
            SLOT = PORT = null;

            int dotIndex = input.IndexOf('.'), lastSlashIndex = input.LastIndexOf('/');

            SLOT = input.Substring(0, dotIndex) + '+';

            PORT = input.Substring(dotIndex + 1, lastSlashIndex - (dotIndex + 1));
        }

        private void processFailureDescription(string data)
        {
            if (data == "----")
            {
                toBeSkipped = true;

                e++;
            }

            output.Add(data);
        }

        private void processOutput(bool isHeader)
        {
            // Push to a csv file and add line break
            if (!toBeSkipped)
            {
                if (!isHeader && output.Count == totalColumns)
                {
                    writer.WriteLine(string.Join(",", output));

                    rows++;
                }

                if (isHeader)
                {
                    writer.WriteLine(string.Join(",", output));
                }
            }

            //Console.WriteLine($"{(isHeader ? "Header" : "Line")} has been processed, moving on to next line.\n");

            // Then empty array and update line counter
            output = new List<string>(22);

            toBeSkipped = false;

            lines++;
        }

        private void closeFile()
        {
            // Close csv writer
            writer.Close();

            // Close csv reader
            reader.Close();

            Console.WriteLine($"Parser: Parsing done on file.\n\nParser: Initiating file move.\n");
            Console.WriteLine($"{lines - 1} records in total have been fetched");
            Console.WriteLine($"{rows} lines in total have been parsed");
            Console.WriteLine($"{e} 'Failure Description' records so far!");
            Console.WriteLine($"{corruptRows} is total detected corrupt rows (empty records or missing cells)");
            Console.WriteLine($"{emptyCells} total empty cells in entire file\n");


            // Move and delete fileName
            movefiles();
        }

        private void movefiles()
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
