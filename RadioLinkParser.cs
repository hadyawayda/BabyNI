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
        private HashSet<int>        disabledColumns = new HashSet<int> { 3, 11, 19 }, 
                                    staticColumns = new HashSet<int> { 4, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18 }; // 13; // 3
        private List<string>        output = new List<string>(22), fetchedLine = new List<string>(18);
        private static string?      fileName, filePath, backupFilePath, parsedFile;
        private StreamWriter        writer;
        private StreamReader        reader;
        private int                 counter = 0;
        private bool                toBeSkipped =  false;

        public RadioLinkParser(string file)
        {
            fileName = file;
            filePath = Path.Combine(parserDirectory, fileName);
            backupFilePath = Path.Combine(parserBackupDirectory, fileName);  // parser/processed/radioLinkPower.txt
            parsedFile = Path.Combine(loaderDirectory, Path.GetFileNameWithoutExtension(fileName) + "1.csv");        // loader/radioLinkPower.txt
            
            reader = new StreamReader(filePath!);

            writer = new StreamWriter(parsedFile!);
            
            fetchLine();
        }

        private void fetchLine()
        {
            string line;

            while ( (line = reader.ReadLine()!) != null )
            {
                if (counter == 0)
                {
                    processHeader(line);

                    continue;
                }

                // Fix this shit
                fetchedLine = line.Split(",").ToList();

                processLine(fetchedLine);
            }

            this.closeFile(fileName!);
        }

        private void processHeader(string line)
        {
            line = "NETWORK_SID,DATETIME_KEY," + line + ",LINK,TID,FARENDTID,SLOT,PORT";

            Console.WriteLine($"Header is: {line}\n");

            fetchedLine = line.Split(",").ToList();

            int c = 0;

            for (int i = 0; i < fetchedLine.Count; i++)
            {
                if ( !disabledColumns.Contains(i + 1) )
                {
                    output.Add(fetchedLine[i]);

                    c++;
                }
            }

            Console.WriteLine($"Total columns count for this file is: {output.Count}\n");

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

        private void processOutput(bool isHeader)
        {
            // Push to a csv fileName and add line break
            if ( !toBeSkipped )
            {
                writer.WriteLine(string.Join(",", output));
            }

            //Console.WriteLine($"{(isHeader ? "Header" : "Line")} has been processed, moving on to next line.\n");

            // Then empty array and update line counter
            output = new List<string>(22);

            toBeSkipped = false;

            counter++;
        }

        private void processMiddleColumns(List<string> data)
        {
            for (int i = 0; i < 18; i++)
            {
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

                if ( i == 2 )
                {
                    processObject(data[i]);
                    continue;
                }

                if ( i == 17 )
                {
                    processFailureDescription(data[i]);
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
            generateLINK();
            generateTID();
            generateFARENDTID();
            generateSLOT();
            generatePORT();
        }

        private void generateNetworkSID()
        {
            output.Add((fetchedLine[7] + fetchedLine[8]).GetHashCode().ToString());
        }

        private void generateDateTimeKey()
        {
            output.Add("DATETIME_KEY");

        }

        private void processObject(string data)
        {
            if ( data == "Unreachable Bulk FC" )
            {
                toBeSkipped = true;
            }

            if ( data.Contains("+") )
            {
                generateNewRow();
            }

            output.Add(data);
        }

        int d = 0;

        public void generateNewRow()
        {
            d++;

            Console.WriteLine($"{d} extra rows generated so far!");
        }

        private void processFailureDescription(string data)
        {
            output.Add("ToBeProcessed");
        }

        private void generateLINK()
        {
            output.Add("Link to be generated.");
        }

        private void generateTID()
        {
            output.Add("TID to be generated.");
        }

        private void generateFARENDTID()
        {
            output.Add("FARENDTID to be generated.");
        }

        private void generateSLOT()
        {
            output.Add("SLOT to be generated.");
        }

        private void generatePORT()
        {
            output.Add("PORT to be generated.");
        }

        private void closeFile(string fileName)
        {
            // Close csv writer
            writer.Close();

            // Close csv reader
            reader.Close();

            Console.WriteLine($"Parser: Parsing done on file: {fileName}\n\nParser: Initiating file move.\n");

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
