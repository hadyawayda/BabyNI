namespace BabyNI
{
    internal class RadioLinkParser
    {
        readonly private static string  rootDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File Drop-zone",
                                        parserDirectory = Path.Combine(rootDirectory, "Parser"),
                                        parserBackupDirectory = Path.Combine(parserDirectory, "Processed"),
                                        headerPrefix = "NETWORK_SID,DATETIME_KEY",
                                        headerSuffix = "LINK,TID,FARENDTID,SLOT,PORT";
        readonly private HashSet<int>   disabledColumns = new HashSet<int> { 3, 11, 19 }, // 3 total disabled columns
                                        staticColumns = new HashSet<int> { 4, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18 }; // 13 total static columns
        private List<string>            output, fetchedLine;
        private static string?          filePath, parsedFile;
        private StreamWriter            writer;
        private StreamReader            reader;
        private int                     totalColumns, corruptRows, rows, lines, e, f, emptyCells;
        private bool                    toBeSkipped, newRow;
        private string?                 LINK, TID, FARENDTID, SLOT, SLOT2, PORT;

        public RadioLinkParser(string file)
        {
            filePath = Path.Combine(parserDirectory, file);
            parsedFile = Path.Combine(rootDirectory, "Loader", Path.GetFileNameWithoutExtension(file) + ".csv");
            totalColumns = corruptRows = rows = lines = emptyCells = e = f = 0;
            toBeSkipped = newRow = false;
            output = new List<string>(22);
            fetchedLine = new List<string>(18);
            LINK = TID = FARENDTID = SLOT = SLOT2 = PORT = null;

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

        private void processHeader(string line, string prefix, string suffix)
        {
            line = prefix + "," + line + "," + suffix;

            fetchedLine = line.ToUpper().Split(",").ToList();

            for (int i = 0; i < fetchedLine.Count; i++)
            {
                if ( !disabledColumns.Contains(i + 1) )
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

        private void generateSecondSetOfColumns()
        {
            splitObject();
            output.Add(LINK!);
            output.Add(TID!);
            output.Add(FARENDTID!);
            output.Add(SLOT!);
            output.Add(PORT!);

        }

        private void splitObject()
        {
            if (!toBeSkipped)
            {
                generateFields_1_2_3();
                generateFields_4_5();
            }
        }

        private void generateFields_1_2_3()
        {
            int pointer = 0;

            string input = fetchedLine[2];

            LINK = TID = FARENDTID = null;

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == '_' && input[i + 1] == '_')
                {
                    if (LINK == null)
                    {
                        LINK = input.Substring(0, i);

                        pointer = i;
                    }

                    else if (TID == null)
                    {
                        TID = input.Substring(pointer + 2, i - pointer - 2);

                        pointer = i;
                    }
                }
            }

            LINK = LINK!.Substring(LINK!.IndexOf('/') + 1);

            FARENDTID = input.Substring(pointer + 2);
        }

        private void generateFields_4_5()
        {
            SLOT = PORT = null;

            if (LINK!.Contains("."))
            {
                splitByDot(LINK!);
            }

            else if (LINK!.Contains("+"))
            {
                splitByPlus(LINK!);
            }

            else
            {
                splitBySlash(LINK!);
            }

            if (LINK!.Contains("+") && LINK!.Contains("."))
            {
                Console.WriteLine("***WARNING***\n\nWe have an exception! Extreme case detected.\nObject column contains both a '+'& '.'\nPlease handle accordingly!");
            }
        }

        private void splitByDot(string data)
        {
            int dotIndex = -1, slashIndex = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '.')
                {
                    dotIndex = i;
                }

                if (data[i] == '/' && slashIndex == -1)
                {
                    slashIndex = i;
                }
            }

            SLOT = data.Substring(0, dotIndex);

            PORT = data.Substring(dotIndex + 1, slashIndex - (dotIndex + 1));
        }

        private void splitByPlus(string data)
        {
            newRow = true;

            int plusIndex = -1, slashIndex = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '+')
                {
                    plusIndex = i;
                }

                if (data[i] == '/')
                {
                    slashIndex = i;
                }
            }

            SLOT = data.Substring(0, plusIndex);

            SLOT2 = data.Substring(plusIndex + 1, slashIndex - (plusIndex + 1));

            PORT = data.Substring(slashIndex + 1);

            f++;
        }

        private void splitBySlash(string data)
        {
            SLOT = data.Substring(0, data.IndexOf('/'));

            PORT = data.Substring(data.IndexOf('/') + 1);
        }

        private void processObject(string data)
        {
            if (data == "Unreachable Bulk FC")
            {
                toBeSkipped = true;
            }

            output.Add(data);
        }

        private void processFailureDescription(string data)
        {
            if (data != "-")
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

            if (newRow)
            {
                if (!isHeader && output.Count == totalColumns)
                {
                    // Insert data manipulation logic for PORT column
                    output[20] = SLOT2!;

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
            Console.WriteLine($"{f} 'Object contains a + sign' rows generated so far!");
            Console.WriteLine($"{corruptRows} is total detected corrupt rows (empty records or missing cells)");
            Console.WriteLine($"{emptyCells} total empty cells in entire file");

            // Move and delete fileName
            BaseWatcher.moveFiles(Path.GetFileName(filePath)!, parserDirectory, parserBackupDirectory);
        }
    }
}
