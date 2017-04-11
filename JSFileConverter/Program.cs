using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace JSFileConverter
{
    internal static class Program
    {
        #region Methods

        private static void Main(string[] args)
        {
            string fileToConvert = @"C:\Users\RS185377\Source\Repos\Draftorama2\Draftorama\wwwroot\sets\" + args[0];
            string fileToCreate = fileToConvert.Replace(".js", ".txt");
            string newFileText = string.Empty;

            string oldFileText = File.ReadAllText(fileToConvert);

            foreach (string line in oldFileText.Split('\n'))
            {
                string[] lineParts = line.Split('"');
                if (lineParts.Length > 1)
                {
                    newFileText += lineParts[1] + ";";
                    newFileText += lineParts[7] + ";";
                    newFileText += lineParts[9] + ";";
                    newFileText += lineParts[13] + ";";
                    newFileText += lineParts[11] + ";";
                    newFileText += lineParts[3] + ",";
                    newFileText += lineParts[5] + ";";
                    newFileText += "Tag" + Environment.NewLine;
                }
            }

            File.WriteAllText(fileToCreate, newFileText);

            //foreach (string line in oldFileText.Split('\n'))
            //{
            //    if (line.Contains("\""))
            //    {
            //        string newFileLine = string.Empty;
            //        bool reading = false;
            //        while (line.Length > 0)
            //        {
            //            if (line.First() == '"')
            //            {
            //                reading = !reading;
            //                line.Remove(0, 1);
            //            }
            //            else
            //            {
            //                if (reading)
            //                {
            //                    newFileLine += line.First();
            //                    line.Remove(0, 1);
            //                }
            //                else
            //                {
            //                    line.Remove(0, 1);
            //                }
            //            }
            //        }
            //    }
            //}
        }

        #endregion Methods
    }
}