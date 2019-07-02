using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// CSV processing test
    /// </summary>

    public class DMObjectFull
    {
        public string Asset_Id { get; set; }
        public string FileName { get; set; }
        public string Mime_Type { get; set; }
        public string Created_By { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
    public class DMObject
    {
        public string Asset_Id { get; set; }
        public string Mime_Type { get; set; }
        public string Country { get; set; }
    }

    public class CsvProcessingTest : ITest
    {
        public void Run()
        {
            // TODO
            // Create a domain model via POCO classes to store the data available in the CSV file below
            // Objects to be present in the domain model: Asset, Country and Mime type
            // Process the file in the most robust way possible
            // The use of 3rd party plugins is permitted

            //string csvFile = Resources.AssetImport;
            string csvFile = File.ReadAllText(@"C:\Users\User\Desktop\OneDrive - Vrije Universiteit Brussel\StyleLabs\dotNet\dotNET developer 2019\GeneralKnowledge.Test\Resources\AssetImport.csv", Encoding.UTF8);

            //string csvFile = "/n/nasset id,file_name,mime_type,created_by,email,country,description\n51df6a98 - 614e-40ef - 8885 - 95ae50940058,ElitProin.aam,application / x - authorware - map,sblack0,jmitchell0 @huffingtonpost.com,United States, Maecenas ut massa quis\n\n";
            //string csvFile = "";

            List<DMObject> DM = new List<DMObject>();
            StringReader reader = new StringReader(csvFile);

            // find the first line by skipping any empty lines at the beginning and stops when a line containing some information is found
            // "if(line_split.Length >= 7)" makes sure we don't process any empty lines at the end of the file
            string firstLine;
            string[] firstLine_split = null;
            while ((firstLine = reader.ReadLine()) != null)
            {
                firstLine_split = firstLine.Split(',');
                if (firstLine_split.Length >= 7)
                    break;
            }

            // Stops code if cvfFile == "" or " "
            if (firstLine_split == null || firstLine_split.Length == 1)
                return;


            // Find the indices of the Asset, Country and Mime type in the splitted first line
            // Find the objet index for example: by checking if the object contains "sset" for "Asset", but we skip "A" because we are unsure if it's "A" or "a"
            int index_Asset = 0;
            int index_Country = 0;
            int index_MimeType = 0;
            for (int i = 0; i < firstLine_split.Length; i++)
            {
                if (firstLine_split[i].ToLowerInvariant().Contains("asset"))
                    index_Asset = i;
                else if (firstLine_split[i].ToLowerInvariant().Contains("mime"))
                    index_MimeType = i;
                else if (firstLine_split[i].ToLowerInvariant().Contains("country"))
                    index_Country = i;
            }

            // Store all the csvFile's data in the object list
            // "if(line_split.Length >= 7)" makes sure we don't process any empty lines at the end of the file
            string line;
            string[] line_split;
            while ((line = reader.ReadLine()) != null)
            {
                line_split = line.Split(',');
                DMObject DM_temp = new DMObject();
                if(line_split.Length >= 7) { 
                    DM_temp.Asset_Id = line_split[index_Asset];
                    DM_temp.Mime_Type = line_split[index_MimeType];
                    DM_temp.Country = line_split[index_Country];
                    DM.Add(DM_temp);
                }
            }

            //Prints the object
            Console.WriteLine(String.Format("{0,-40} {1,-40} {2,20}","Asset Id:", "Mime Type:", "Country:"));
            for (int i = 0; i < 10; i++)
               Console.WriteLine(String.Format("{0,-40} {1,-40} {2,20}", DM[i].Asset_Id, DM[i].Mime_Type, DM[i].Country));
            Console.WriteLine("....................................");

            /*
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter("dope.txt", true))
            {
                file.WriteLine(csvFile);
            }*/
        }
    }

}
