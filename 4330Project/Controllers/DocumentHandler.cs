using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Specialized;

namespace _4330Project.Controllers
{
    public class DocumentHandler
    {
        //this is a HashSet of all our available stopwords to remove from the document
        HashSet<string> stopWords = new HashSet<string>() { "a", "about", "above", "after", "again", "against", "all", "am",
                "an", "and", "any", "are", "as", "at", "be", "because", "been", "before", "being", "below", "been",
                "before", "being", "below", "between", "both", "but", "by", "could", "did", "do", "does", "doing", "down",
                "during", "each", "few", "for", "from", "further", "had", "has", "have", "having", "he", "he'd", "he'll",
                "he's", "her", "here", "here's", "hers", "herself", "him", "himelf", "his", "how", "how's", "I", "I'd",
                "I'll", "I'm", "I've", "if", "in", "into", "is", "it", "it's", "its", "itself", "let's", "me", "more",
                "most", "my", "myself", "nor", "of", "on", "once", "only", "or", "other", "ought", "our", "ours", "ourselves",
                "out", "over", "own", "same", "she", "she'd", "she'll", "she's", "should", "so", "some", "such", "than", "that",
                "that's", "the", "their", "theirs", "them", "themselves", "then", "there", "there's", "these", "they", "they'd",
                "they'll", "they're", "they've", "this", "those", "through", "to", "too", "under", "until", "up", "very", "was",
                "we", "we'd", "we'll", "we're", "we've", "were", "what", "what's", "when", "when's", "where", "where's", "which",
                "while", "who", "who's", "whom", "why", "why's", "with", "would", "you", "you'd", "you'll", "you're", "you've",
                "your", "yours", "yourself", "yourselves" };

        /*Converts the contents of the document given as a string to the documents
         * location and then converted the document into a single string to be parsed/cleaned up*/ 
        private string convertDocToString(string docPath) { 
            //we're going to read the contents of a file. Because of Azure stuff, I'm unsure of how to get the proper document pathing. 
            if (!File.Exists(docPath))
            {
                //Throw an error if it is unable to find a particular document
                throw new System.Exception("File not Found.");
            }

            string readText = File.ReadAllText(docPath);
            return readText;
        }

        /*This method is passed a string that is our document as an argument where it will parse the document (processing it for all valid strings that aren't
         * stop words (the, and, etc...) and maintaining them in the document. 
         * */
        private List<KeyValuePair<string, int>> parseString(string s, HashSet<string> ignore)
        {
            Dictionary<string, int> documentKeywords = new Dictionary<string, int>();

            char[] sep = "\n\t !@#$%^&*()_+{}|[]\\:\";<>?,./".ToCharArray();
            Regex.Replace(s, @"[^a-zA-Z]+", "");
            string[] words = s.ToLower().Split(sep);
            ignore.Add(""); //generated from consecutive splitting characters
            foreach (string w in words)
            {
                if (!ignore.Contains(w))
                {
                    if (documentKeywords.ContainsKey(w))
                    { //increase count of word already seen
                        documentKeywords[w]++;
                    }
                    else
                    { // make a first entry
                        documentKeywords[w] = 1;
                    }
                }
            }
           return sortDictByValue(documentKeywords);
        }

        /*Helper method to sort our dictionary after it's processed. Should return all the words sorted by the most common iterations*/
        private List<KeyValuePair<string,int>> sortDictByValue(Dictionary<string,int> dict)
        {
            //converts the dictionary to a list, sorts it, then returns the dictionary property
            var dictList = dict.ToList();

            dictList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            return dictList;
            //OrderedDictionary retDict = new OrderedDictionary();
            //foreach (var item in dictList)
            //{
            //    retDict.Add(item.Key, item.Value);
            //}
            //return retDict;
        }
    }
}