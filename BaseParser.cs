using System.Security.Cryptography;

namespace BabyNI
{
    internal class BaseParser
    {
        internal string     LINK, TID, FARENDTID, SLOT, SLOT2, PORT;
        internal bool       newRow;

        internal BaseParser() 
        { 
            newRow = false;

            LINK = TID = FARENDTID = SLOT = SLOT2 = PORT = "";
        }

        internal void generateFields(string data)
        {
            int pointer = 0;

            LINK = TID = FARENDTID = "";

            for (int i = 0; i < data.Length - 1; i++)
            {
                if (data[i] == '_' && data[i + 1] == '_')
                {
                    if (LINK == "")
                    {
                        LINK = data.Substring(0, i);

                        pointer = i;
                    }

                    else if (TID == "")
                    {
                        TID = data.Substring(pointer + 2, i - pointer - 2);

                        pointer = i;
                    }
                }
            }

            LINK = LINK.Substring(LINK.IndexOf('/') + 1);

            FARENDTID = data.Substring(pointer + 2);

            generateFields2();
        }

        private void generateFields2()
        {
            SLOT = SLOT2 = PORT = "";

            if (LINK.Contains("."))
            {
                splitByDot(LINK);
            }

            else if (LINK.Contains("+"))
            {
                splitByPlus(LINK);
            }

            else
            {
                splitBySlash(LINK);
            }

            if (LINK.Contains("+") && LINK.Contains("."))
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
        }

        private void splitBySlash(string data)
        {
            SLOT = data.Substring(0, data.IndexOf('/'));

            PORT = data.Substring(data.IndexOf('/') + 1);
        }
    }
}
