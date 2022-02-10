using System;
using System.IO;
using System.Text;

namespace DDDSample1.Domain.Util
{
    public class LerFicheiros
    {
        public static string lerFicheiro(string fileName)
        {
            StringBuilder sb = new StringBuilder();

            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(fileName);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    sb.Append(line).Append("\n");
                    //Read the next line
                    line = sr.ReadLine();

                }

                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            return sb.ToString();
        }
    }
}