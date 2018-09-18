using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;

namespace ConsoleV
{
    /// <summary>
    /// This is a console application that imports text data files
    /// from a specified directory location, runs staistics such as 
    /// word dictionary, and then serializes the data import statistics
    /// object into a JSON file. 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method of the program for user import of
        /// files into a processing program where statistics
        /// can be run on the data import
        /// </summary>
        /// <param name="args">No arguments and processed</param>
        static void Main(string[] args)
        {
            // Creates an object to store the processing statistics for the serialization activity
            DocumentStatistics stats = new DocumentStatistics();

            // The main file path to the directory holding the files to be processed
            string filepath = @"../../Documents";

            // Processes the files at the specified file directory location
            ProcessFiles(filepath, stats);

            // Serializes the DocumentStatistics object into a JSON file
            SerializeStats(filepath, stats);

            // Wait for the User Input
            Console.Write("Press <ENTER> to quit...");
            Console.ReadLine();
        }

        /// <summary>
        /// This determines the file listing of all files in a specified directory
        /// and processes each file by reading the contents into memory and determining
        /// statistics such as the word dictionary, document count, and list of documents
        /// that were processed as part of the statistics. 
        /// </summary>
        /// <param name="filepath">File path to the directory of where to process text files</param>
        /// <param name="stats">The object (DocumentStatistics) of which to store the processing statistics.</param>
        private static void ProcessFiles(string filepath, DocumentStatistics stats)
        {
            // Determine list of files to process from the current directory
            string[] files = Directory.GetFiles(filepath);

            // Process each file in the list of files determined above
            foreach (string file in files)
            {
                // Initialize the array to hold the words
                string[] words = null;
                
                // Output the file being processed to the console window
                Console.WriteLine($"File Being Processed: {file}");

                // Add the file name to the stats.Documents property
                stats.Documents.Add(file);

                // Add one to the stats.DocumentCount property
                stats.DocumentCount++;

                // Use a StreamReader to open the current file
                using (StreamReader reader = File.OpenText(file))
                {
                    // Read in the text in whatever way you see fit
                    // Parse the text into individual words
                    //o You can use System.Text.RegularExpressions.Regex.Split()

                    // This reads the file into one string. 
                    // If I knew the files were long, I would had used a StringBuilder object
                    string all = reader.ReadToEnd();

                    // This is the pattern to split by word
                    words = Regex.Split(all, @"\W");

                } // end of using block


                // Update the stats.WordCounts dictionary to show the counts for each word processed
                //o This dictionary uses the word as the key and the number of times that word was
                //encountered as its value
                //o This should be case-insensitive
                //o You can eliminate symbols like: ( ) : ; .
                
                foreach (string _word in words)
                {                                
                    // Check if null or whitespace skip this word processing
                    if (String.IsNullOrWhiteSpace(_word))
                    {
                        continue;
                    }

                    // change the words to lower case
                    string word = _word.ToLower();

                    // If the Word Exists, increment the value
                    if (stats.WordCounts.ContainsKey(word))
                    {
                        // The word exists, add 1 to the counter
                        stats.WordCounts[word]++;
                    }
                    // Else Add word to the dictionary with a count of 1
                    else
                    {
                        // The word does not exist, add the key/value pair
                        stats.WordCounts.Add(word, 1);
                    }
                  
                } // end of foreach
            } // end of foreach for files
        } // end of method

        /// <summary>
        /// This method will serialize a DocumentStatistics object into a 
        /// JSON file called "stats.json" at a specified file directory
        /// location
        /// </summary>
        /// <param name="filepath">File path location where to serialize </param>
        /// <param name="stats">object (DocumentStatistics) of which to serialize</param>
        private static void SerializeStats(string filepath, DocumentStatistics stats)
        {
            string filename = Path.Combine(filepath, "stats.json");

            using (FileStream stm = new FileStream(filename, FileMode.Create))
            {
                DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(DocumentStatistics), settings);
                serializer.WriteObject(stm, stats);

            } // end of using

        } // end of Serializer method

    } // end of class
} // end of namespace
