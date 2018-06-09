using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Visual {
    public static class strlib {
        public static string replace_sub(string word, string placeholder, string insert) {
            string newword = string.Empty;
            int start = 0;
            for (int i = 0, j = 0; i < word.Length; i++) {
                if (word[i] == placeholder[j]) {
                    if (j == 0) start = i;
                    j++;
                    if (j == placeholder.Length) {
                        newword += insert;
                        j = 0;
                    }
                }
            else if (j > 0 && word[i] != placeholder[j]) {
                j = 0; 
                for (int k = start; k < i; k++) newword += word[k];
            }
            else newword += word[i];
            }
            return newword;
        }
    }       
}