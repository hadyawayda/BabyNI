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
        readonly private HashSet<int>   disabledColumns = new HashSet<int> { 3, 11, 19 }, // 3 total disabled columns
                                        staticColumns = new HashSet<int> { 4, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18 }; // 13 total static columns
        private List<string>        output, fetchedLine;
        private static string?      filePath, backupFilePath, parsedFile;
        private StreamWriter        writer;
        private StreamReader        reader;
        private int                 totalColumns, corruptRows, rows, lines, d, e, f, g, emptyCells;
        private bool                toBeSkipped, newRow;
        private string?             LINK, TID, FARENDTID, SLOT, PORT;

        public RadioLinkParser(string file)
        {
            filePath = Path.Combine(parserDirectory, file);
            backupFilePath = Path.Combine(parserBackupDirectory, file);  // parser/processed/radioLinkPower.txt
            parsedFile = Path.Combine(loaderDirectory, Path.GetFileNameWithoutExtension(file) + ".csv");        // loader/radioLinkPower.txt
            totalColumns = corruptRows = rows = lines = emptyCells = d = e = f = g = 0;
            toBeSkipped = newRow = false;
            output = new List<string>(22);
            fetchedLine = new List<string>(18);

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
                        processHeader(line);

                        continue;
                    }

                    // Fix this shit
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
            
            totalColumns = output.Count;

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
            // Push to a csv file and add line break
            if ( !toBeSkipped)
            {
                if ( !isHeader && output.Count == totalColumns )
                {
                    writer.WriteLine(string.Join(",", output));

                    rows++;
                }

                if ( isHeader )
                {
                    writer.WriteLine(string.Join(",", output));
                }
            }

            if ( newRow )
            {
                if (!isHeader && output.Count == totalColumns)
                {
                    // Insert data manipulation logic for PORT column
                    output[21] += 1;

                    writer.WriteLine(string.Join(",", output));

                    rows++;
                }
            }

            //Console.WriteLine($"{(isHeader ? "Header" : "Line")} has been processed, moving on to next line.\n");

            // Then empty array and update line counter
            output = new List<string>(22);

            toBeSkipped = newRow = false;

            lines++;
        }

        private void processMiddleColumns(List<string> data)
        {
            for (int i = 0; i < 18; i++)
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
            splitObject(fetchedLine[2]);
            generateLINK();
            generateTID();
            generateFARENDTID();
            generateSLOT();
            generatePORT();
        }

        private void generateNetworkSID()
        {
            // Create an optional hashing function that takes the order into consideration i.e. KMP
            output.Add(Math.Abs((fetchedLine[7] + fetchedLine[8]).GetHashCode()).ToString());
        }

        private void generateDateTimeKey()
        {
            //output.Add("DATETIME_KEY");

            // Create an optional hashing function that takes the order into consideration i.e. KMP
            output.Add(Math.Abs((fetchedLine[3]).GetHashCode()).ToString());

        }

        private void processFailureDescription(string data)
        {
            output.Add(data);
            
            if (data != "-") { 
                toBeSkipped = true;

                e++;
            }
            
            //output.Add("ToBeProcessed");
        }

        private void processObject(string data)
        {
            if (data == "Unreachable Bulk FC")
            {
                d++;
                
                toBeSkipped = true;
            }

            if (data.Contains("."))
            {
                int slashIndex1 = -1, dotIndex = -1, slashIndex2 = -1;

                for ( int i = 0; i < data.Length; i++ )
                {
                    if ( data[i] == '/' && slashIndex1 == -1 )
                    {
                        slashIndex1 = i;
                    }
                    
                    if ( data[i] == '.' )
                    {
                        dotIndex = i;
                    }

                    if (data[i] == '/' && slashIndex1 != -1)
                    {
                        slashIndex2 = i;
                    }
                }

                SLOT = data.Substring(slashIndex1 + 1, dotIndex - (slashIndex1 + 1));

                PORT = data.Substring(dotIndex + 1, slashIndex2 - (dotIndex + 1));

                Console.WriteLine($"Line {lines} includes a '.' value\n");
                
                g++;
            }

            if (data.Contains("+"))
            {
                //Console.WriteLine(data);

                
                newRow = true;
                
                f++;
            }

            if (data.Contains("+") && data.Contains("."))
            {
                Console.WriteLine("***WARNING***\n\nWe have an exception! Extreme case detected.\nObject column contains both a '+'& '.'\nPlease handle accordingly!");
            }

            output.Add(data);
        }

        private void splitObject(string input)
        {
            int pointer = 0;

            LINK = TID = FARENDTID = null;

            for ( int i = 0; i < input.Length - 1; i++ )
            {
                if (input[i] == '_' && input[i + 1] == '_' )
                {
                    if ( LINK == null )
                    {
                        LINK = input.Substring(0, i);
                        pointer = i;
                    }

                    else if ( TID == null )
                    {
                        TID = input.Substring(pointer + 2, i - pointer - 2);
                        pointer = i;
                    }
                }
            }

            FARENDTID = input.Substring(pointer + 2);
        }
        private void generateLINK()
        {
            output.Add(LINK!);
        }
        private void generateTID()
        {
            output.Add(TID!);
        }

        private void generateFARENDTID()
        {
            output.Add(FARENDTID!);
        }

        private void generateSLOT()
        {
            output.Add(SLOT!);
        }

        private void generatePORT()
        {
            output.Add(PORT!);
        }

        private void closeFile()
        {
            // Close csv writer
            writer.Close();

            // Close csv reader
            reader.Close();

            Console.WriteLine($"Parser: Parsing done on file.\n\nParser: Initiating file move.\n");
            Console.WriteLine($"{d} 'Unreachable bulk FC' records so far!");
            Console.WriteLine($"{e} 'Failure Description' records so far!");
            Console.WriteLine($"{f} 'Object contains a + sign' rows generated so far!");
            Console.WriteLine($"{g} 'Object contains a . sign' rows found so far!");
            Console.WriteLine($"{lines - 1} records in total have been fetched");
            Console.WriteLine($"{rows} lines in total have been parsed");
            Console.WriteLine($"{f + rows} expected total rows in output file");
            Console.WriteLine($"{corruptRows} is total detected corrupt rows (empty records or missing cells)");
            Console.WriteLine($"{emptyCells} total empty cells in entire file");


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
