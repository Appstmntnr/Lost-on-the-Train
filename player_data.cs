using System;
using System.Collections.Generic;

namespace Visual
{
    static class player_data {
        public static string name = "Player";

        public static void get_player_name() {
            //Console.WriteLine("Please enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("Player's name is now: " + name);
        }
    }
}