namespace Parser.Parsers
{
    public class RadioLinkParser
    {
        readonly private static string  rootDirectory = @"C:\Users\User\OneDrive - Novelus\Desktop\File DropZone",
                                        parserDirectory = Path.Combine(rootDirectory, "Parser"),
                                        parserBackupDirectory = Path.Combine(parserDirectory, "Processed"),
                                        headerPrefix = "NETWORK_SID,DATETIME_KEY",
                                        headerSuffix = "LINK,TID,FARENDTID,SLOT,PORT";
        readonly private HashSet<int>   disabledColumns = new() { 3, 11, 19 },
                                        staticColumns = new() { 4, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18 };
        private List<string>            output, fetchedLine;
        private string?                 filePath, parsedFile;
        private StreamWriter            writer;
        private StreamReader            reader;
        private int                     corruptRows, rows, lines, e, emptyCells, totalColumns;
        private bool                    toBeSkipped;
        private string                  DATETIME_KEY;
        private BaseParser              parser;

        #region Parser Entry Point

        public RadioLinkParser(string file)
        {
            filePath = Path.Combine(rootDirectory, file);
            parsedFile = Path.Combine(parserDirectory, Path.GetFileNameWithoutExtension(file) + ".csv");
            totalColumns = corruptRows = rows = lines = emptyCells = e = 0;
            toBeSkipped = false;
            output = new List<string>(22);
            fetchedLine = new List<string>(18);
            DATETIME_KEY = Path.GetFileNameWithoutExtension(file).Substring(26);

            reader = new StreamReader(filePath);

            writer = new StreamWriter(parsedFile);

            // switch from instance to inheritance
            parser = new BaseParser();

            fetchLine();

            // add loader calling logic here....

        }

        private void fetchLine()
        {
            string line;

            while ((line = reader.ReadLine()!) != null)
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
            Console.WriteLine($"{emptyCells} total empty cells in entire file");

            // Move and delete fileName
            moveFiles(Path.GetFileName(filePath)!, rootDirectory, parserBackupDirectory);
        }

        private void moveFiles(string fileName, string initialDirectory, string destinationDirectory)
        {
            // This method could make use of a queue system as well, but it's not that important right now.
            string filePath = Path.Combine(initialDirectory, fileName);
            string fileDestinationPath = Path.Combine(destinationDirectory, fileName);

            if (File.Exists(fileDestinationPath))
            {
                File.Delete(fileDestinationPath);
            }

            // Move file to archive directory
            File.Copy(filePath, fileDestinationPath);

            File.Delete(filePath);
        }

        #endregion

        #region Data Processors

        private void processHeader(string line, string prefix, string suffix)
        {
            line = prefix + "," + line + "," + suffix;

            fetchedLine = line.ToUpper().Split(",").ToList();

            for (int i = 0; i < fetchedLine.Count; i++)
            {
                if (!disabledColumns.Contains(i + 1))
                {
                    output!.Add(fetchedLine[i]);
                }
            }

            totalColumns = output!.Count;

            Console.WriteLine($"Header is: {string.Join(", ", output)}\n");

            Console.WriteLine($"Total columns count for this file is: {totalColumns}\n");

            processOutput(true);
        }

        private void processLine(List<string> data)
        {
            generateFirstSetOfColumns();
            processMiddleColumns(data);
            generateSecondSetOfColumns();
            processOutput(false);
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
                    output!.Add(data[i]);
                    //Console.WriteLine($"Column {i + 1} on Line {counter} has been processed\n");
                    continue;
                }

                if (i == 2)
                {
                    processObject(data[i]);
                    continue;
                }

                if (i == 17)
                {
                    processFailureDescription(data[i]);
                    continue;
                }
            }
        }

        private void processObject(string data)
        {
            if (data == "Unreachable Bulk FC")
            {
                toBeSkipped = true;
            }

            output!.Add(data);
        }

        private void processFailureDescription(string data)
        {
            if (data != "-")
            {
                toBeSkipped = true;

                e++;
            }

            output!.Add(data);
        }

        private void processOutput(bool isHeader)
        {
            if (!toBeSkipped)
            {
                if (!isHeader && output.Count == totalColumns)
                {
                    writer.WriteLine(string.Join(",", output));

                    rows++;
                }

                if (isHeader)
                {
                    writer.WriteLine(string.Join(",", output!));
                }
            }

            if (parser.newRow)
            {
                if (!isHeader && output!.Count == totalColumns)
                {
                    // Insert data manipulation logic for PORT column
                    output[20] = parser.SLOT2!;

                    writer.WriteLine(string.Join(",", output));

                    rows++;
                }
            }

            //Console.WriteLine($"{(isHeader ? "Header" : "Line")} has been processed, moving on to next line.\n");

            // Then empty array and update line counter
            output = new List<string>(22);

            toBeSkipped = parser.newRow = false;

            lines++;
        }

        #endregion

        #region Data Generators

        private void generateFirstSetOfColumns()
        {
            // Create an optional hashing function that takes the order into consideration i.e. KMP
            output!.Add(Math.Abs((fetchedLine![6] + fetchedLine[7]).GetHashCode()).ToString());

            output.Add(Math.Abs(DATETIME_KEY.GetHashCode()).ToString());
        }

        private void generateSecondSetOfColumns()
        {
            if (!toBeSkipped)
            {
                parser.generateFields(fetchedLine[2]);
            }

            output.Add(parser.LINK);
            output.Add(parser.TID);
            output.Add(parser.FARENDTID);
            output.Add(parser.SLOT);
            output.Add(parser.PORT);
        }

        #endregion
    }
}
