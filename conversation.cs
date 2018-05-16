using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Visual {
    class conversation {
        public List<question> Qs;
        public int current_question;

        // conversation(filename) loads the file at the relative location filename
        // effects: allocates memory
        // efficiency: O(n^2)
        public conversation(string filename) {
            this.Qs = new List<question>();
            current_question = 0;

            // Store each line of the file in contents
            string [] contents = System.IO.File.ReadAllLines(@filename);

            int i = -1; // index variable

            // Parse through the dialogue file and make dialogue nodes
            foreach (string line in contents) {
                if (line.Contains("DIALOGUE")) {
                    question q = new question();
                    Qs.Add(q);
                    i++;
                }
                else if (line.Contains("SPEAKER")) {
                    Qs[i].set_speaker(line.Substring(9, line.Length - 9));
                }
                else if (line.Contains("TEXT")) Qs[i].set_text(line.Substring(6, line.Length - 6));
                else if (line.Contains("OPTION")) {
                    if (!line.Contains("NULL") && !line.Contains("STALL")) Qs[i].addOption(line.Substring(10, line.Length - 10));
                }
                else if (line.Contains("RESPONSE")) {
                    //int l = line[12];  
                    int num = 0;
                    
                    for (int j = 12; j < line.Length && line[j] >= 48 && line[j] <= 57; j++) {
                        num *= 10;
                        num += (line[j] - '0');
                    }
                    
                    Qs[i].addResponse(num);
                }
                else if (line.Contains("SETTING"))  {
                    Qs[i].set_setting(line.Substring(9, line.Length - 9));
                }
            }
        }
    }
}