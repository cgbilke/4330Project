using System;
using Microsoft;
using Microsoft.Office;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Word;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq.Enumberable;
using System.Web;
using System.Web.MVC;

namespace _4330Project.Controllers
{
    public class DocumentController
    {
        /*Following recommended documentation for opening a MS word document in order to 
        parse through it's contents without using Third Party API and making the changes
        to the native document itself*/
        private Dictionary parseDocument(object newFile) {
            //Creating a dictionary to hold a glossary of all words within the document we are parsing
            //in the format <string, int> in order to hold the keyword as the key and the number of occurences
            //as the value (in <key, value> pairing)
            IDictionary<string, int> document_keywords = new Dictionary<string, int>();

            try {
                //Implement parsing of Word document here

                if(document_keywords.containsKey(word)) {
                    document_keywords(word) = document_keywords.getValue() + 1;
                } else {
                    document_keywords.add(word, 1);
                }

                highlightWordDocument(newFile, document_keywords);

                varDoc.Save();
            } catch(Expection e) {
                MessageBox.Show("Error:\n" + e.Message, "Error Message");
            }
            
            return document_keywords;
        }

        private void highlightWordDocument(Dictionary<string, int> table) {
            //Sorting the table by Value in order to get the most common keywords
            table.Sort((pair1,pair2) => pair1.Value.CompareTo(pair2.Value));
            
            //Implement initial highlighting of Word Document here, iterate through sorted list
            
        }
    }
}