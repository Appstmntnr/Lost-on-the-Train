using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

        List<conversation> convos = new List<conversation>();
        private int current_convo;

        void Awake() {
            convos.Add(new conversation("Assets/day1.txt"));
            current_convo = 0;
            current_question = 0;

            // setup first question
            change_question(0);
        }

        public void change_question(int new_q) {
            current_question = new_q;
            convos[current_convo].Qs[current_question].activate(question_text, speaker, option1, option2, option3, 
                                                                option1_button, option2_button, option3_button, 
                                                                next_button, submit, text_input, 
                                                                Background_Canvas);
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

        public void save_game() {
            string [] lines = {"conversation: " + current_convo, "question: " + current_question, "name: " + player_data.name};
            System.IO.File.WriteAllLines(@"save.txt", lines);
        }

        public void load_game() {
            string [] lines = System.IO.File.ReadAllLines(@"save.txt");
            current_convo = lines[0][14] - '0';
            change_question(lines[1][10] - '0');
            player_data.name = lines[2].Substring(6, lines[2].Length - 6);
        }
    }
}