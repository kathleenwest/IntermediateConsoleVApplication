using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleV
{
    /// <summary>
    /// This class holds mostly state information in regards to the 
    /// document statistics of reading documents using serialization
    /// techniques from the main program class
    /// </summary>
    [DataContract]
    class DocumentStatistics
    {
        #region Properties

        /// <summary>
        /// The number of documents that were read and serialized
        /// </summary>
        [DataMember]
        public int DocumentCount { get; set; }

        /// <summary>
        /// The list of document file names and path locations
        /// that were serialized in a specified directory 
        /// </summary>
        [DataMember]
        public List<string> Documents { get; set; }

        /// <summary>
        /// The listing of distinct words and their count
        /// as read from the document location path
        /// </summary>
        [DataMember]
        public Dictionary<string, int> WordCounts { get; set; }

        #endregion Properties

        /// <summary>
        /// Constructs and initializes an object
        /// ready for the serialization techniques
        /// </summary>
        public DocumentStatistics()
        {
            DocumentCount = 0;
            Documents = new List<string>();
            WordCounts = new Dictionary<string, int>();
        } // end of method

    } // end of class
} // end of namespace
