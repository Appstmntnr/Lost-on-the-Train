using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Visual {
    class question {
        public string question_text; // content of the question
        public string speaker; // whoever poses the question
        public List<string> options; // options available
        public List<int> responses; // responses to all options
        public string setting; // the place in which the question takes place
        public int convo_change; // the index number of the conversation that should take place directly after this dialog node

        // question() allocates memory for a new question
        // effects: allocates memory
        // efficiency: O(1)
        public question() {
            this.options = new List<string>();
            this.responses = new List<int>();
            this.convo_change = -1;
        }

        // set_setting(new_sett) changes setting to new_sett
        // effects: mutates data
        // efficiency: O(1)
        public void set_setting(string new_sett) {
            this.setting = new_sett;
        }

        // set_setting(new_cv) changes convo_change to new_cv
        // effects: mutates data
        // efficiency: O(1)
        public void set_convo_change(int new_cv) {
            this.convo_change = new_cv;
        }

        // set_text(new_text) changes question_text to new_text
        // effects: mutates data
        // efficiency: O(1)
        public void set_text(string new_text) {
            this.question_text = new_text;
        }

        // set_speaker(spk) changes speaker to spk
        // effects: mutates data
        // efficiency: O(1)
        public void set_speaker(string spk) {
            this.speaker = spk;
        }

        // addOption(new_op) adds new_op to options
        // effects: mutates data
        // efficiency: O(1)
        public void addOption(string new_op) {
            this.options.Add(new_op);
        }

        // addResponse(new_re) adds new_re to responses
        // effects: mutates data
        // efficiency: O(1)
        public void addResponse(int new_re) {
            this.responses.Add(new_re);
        }

        // get_speaker() returns speaker
        // efficiency: O(1)
        public string get_speaker() { return speaker; }

        // get_text returns question_text
        // efficiency: O(1)
        public string get_text() { return question_text; }

        public void activate(Text question_text, Text speaker, Text option1, Text option2, Text option3, 
                            GameObject option1_button, GameObject option2_button, GameObject option3_button, 
                            GameObject next, GameObject submit, InputField text_input, 
                            GameObject Background_Canvas, int current_convo) {

            foreach (Transform background in Background_Canvas.transform) {
                background.gameObject.SetActive(false);
            }
            Background_Canvas.gameObject.transform.FindChild(this.setting).gameObject.SetActive(true);

            // de-activate all buttons
            option1_button.gameObject.SetActive(false);
            option2_button.gameObject.SetActive(false);
            option3_button.gameObject.SetActive(false);

            // activate only neccessary buttons
            if (this.options.Count >= 1) { option1_button.gameObject.SetActive(true); option1.text = this.options[0]; }
            if (this.options.Count >= 2) { option2_button.gameObject.SetActive(true); option2.text = this.options[1]; }
            if (this.options.Count >= 3) { option3_button.gameObject.SetActive(true); option3.text = this.options[2]; }

            // adjust font size
            while (this.options.Count >= 1) {
                if (this.options[0].Length <= 20) { Debug.Log("too short to resize"); break; }
                else if ((4 * this.options[0].Length) >= (7 * option1.fontSize)) { Debug.Log("ratio is good"); break; }
                else { option1.fontSize--; Debug.Log("font size: " + option1.fontSize); }
            }
            while(this.options.Count >= 2) {
                if (this.options[1].Length <= 20) { Debug.Log("too short to resize"); break; }
                else if ((4 * this.options[1].Length) >= (7 * option2.fontSize)) { Debug.Log("ratio is good"); break; }
                else { option2.fontSize--; Debug.Log("font size: " + option2.fontSize); }
            }
            while (this.options.Count >= 3) {
                if (this.options[2].Length <= 20) { Debug.Log("too short to resize"); break; }
                else if ((4 * this.options[2].Length) >= (7 * option3.fontSize)) { Debug.Log("ratio is good"); break; }
                else { option3.fontSize--; Debug.Log("font size: " + option3.fontSize); }
            }

            // special events
            if (this.options.Count >= 1 && this.options[0] == "INPUT[NAME]") {
                next.gameObject.SetActive(false);
                option1_button.gameObject.SetActive(false); 
                submit.gameObject.SetActive(true);
                text_input.gameObject.SetActive(true);
                text_input.text = "Enter your name";
            }
            else {
                next.gameObject.SetActive(true);
                submit.gameObject.SetActive(false);
                text_input.gameObject.SetActive(false);
            }
            
            if (this.options.Count >= 1 && this.options[0].Contains("CHANGE_CONVERSATION")) {
                Debug.Log("We should change convo");
                if (this.options[0].Length >= 31) { 
                    current_convo = (this.options[0][20] - '0');
                    Debug.Log("current_convo: " + current_convo);
                }
            }
            
            if (this.question_text.Contains("NAME")) 
                { question_text.text = strlib.replace_sub(this.question_text, "NAME", player_data.name); }
            else question_text.text = this.question_text;

            if (this.speaker == "NAME") this.speaker = player_data.name;
            speaker.text = this.speaker;
        }
    }
}
