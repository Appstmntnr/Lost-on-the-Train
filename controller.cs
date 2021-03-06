using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

namespace Visual {
    public class controller : MonoBehaviour {
        public Text speaker;
        public Text question_text;
        public GameObject option1_button;
        public GameObject option2_button;
        public GameObject option3_button;
        public GameObject next_button;
        public Text option1;
        public Text option2;
        public Text option3;
        public int current_background;
        public int total_backgrounds;
        private int current_question;

        public InputField text_input;
        public GameObject submit;

        public GameObject Background_Canvas;        
        public GameObject MenuCanvas;
        public GameObject OptionsCanvas;

        List<conversation> convos = new List<conversation>();
        private int current_convo;

        void Awake() {
            convos.Add(new conversation("Assets/Text_Files/English/day1.txt"));
            convos.Add(new conversation("Assets/Text_Files/English/day4.txt"));
            convos.Add(new conversation("Assets/Text_Files/English/day5.txt"));
            convos.Add(new conversation("Assets/Text_Files/English/library.txt"));
            Debug.Log("amount of convos: " + convos.Count);
            current_convo = 0;
            current_question = 0;

            // setup first question
            change_question(0);
        }

        public void change_question(int new_q) {
            // in the case of a change in conversation
            if (convos[current_convo].Qs[current_question].convo_change >= 0) {
                current_convo = convos[current_convo].Qs[current_question].convo_change;
                current_question = 0;
            }
            // normal case
            else current_question = new_q;

            // activate the next question
            convos[current_convo].Qs[current_question].activate(question_text, speaker, option1, option2, option3, 
                                                                option1_button, option2_button, option3_button, 
                                                                next_button, submit, text_input, 
                                                                Background_Canvas, current_convo);
        }

        public void choose1() {
            change_question(convos[current_convo].Qs[current_question].responses[0]);
        }

        public void choose2() {
            change_question(convos[current_convo].Qs[current_question].responses[1]);
        }

        public void choose3() {
            change_question(convos[current_convo].Qs[current_question].responses[2]);
        }

        public void next() {
            choose1();
        }
        
        public void submit_text() {
            player_data.name = text_input.text;
            text_input.gameObject.SetActive(false);
            submit.gameObject.SetActive(false);
            change_question(convos[current_convo].Qs[current_question].responses[0]);
        }

        // save_game() saves the player's progress by writing the player's name,
        //   current_convo, and current_question to a text file
        // effects: mutates data, file I/O
        // efficiency: O(n)
        public void save_game() {
            string [] lines = {"conversation: " + current_convo, "question: " + current_question, "name: " + player_data.name};
            System.IO.File.WriteAllLines(@"save.txt", lines);
        }

        // load_game() loads the player's game save by reading from 
        //   a pre-written text file containing the player's name,
        //   current_convo, and current_question
        // effects: mutates data, file I/O
        // efficiency: O(n)
        public void load_game() {
            string [] lines = System.IO.File.ReadAllLines(@"save.txt");

            int convo_num = 0;
            for (int i = 14; i < lines[0].Length && lines[0][i] >= 48 && lines[0][i] <= 57; i++)
                { convo_num = (convo_num * 10) + (lines[0][i] - '0'); }
            current_convo = convo_num;

            int question_num = 0;
            for (int i = 10; i < lines[1].Length && lines[1][i] >= 48 && lines[1][i] <= 57; i++)
                { question_num = (question_num * 10) + (lines[1][i] - '0'); }
            change_question(question_num);

            player_data.name = lines[2].Substring(6, lines[2].Length - 6);

            MenuCanvas.gameObject.SetActive(false); // disable the menu if it's active
        }

        // start_game() deactivates the main menu and options menu so that the game can begin
        // effects: deactivates GameObjects
        // efficiency: O(1)
        public void start_game() {
            MenuCanvas.gameObject.SetActive(false); // disable the menu if it's active
        }

        // deactivate_options_menu() activates the options menu
        // effects: activates GameObjects
        // efficiency: O(1)
        public void activate_options_menu() {
            MenuCanvas.gameObject.transform.FindChild("Options Menu").gameObject.SetActive(true);
        }

        // deactivate_options_menu() deactivates the options menu
        // effects: deactivates GameObjects
        // efficiency: O(1)
        public void deactivate_options_menu() {
            MenuCanvas.gameObject.transform.FindChild("Options Menu").gameObject.SetActive(false);
        }

        public void change_lang_german() {
            for (int i = convos.Count - 1; i >=0; i--) convos.RemoveAt(i);

            string [] filenames = Directory.GetFiles("Assets/Text_Files/German");

            foreach (string path in filenames) { convos.Add(new conversation(path)); Debug.Log(path); }
        }
    }
}