﻿using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediTournamentConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            JediTournamentInterface consoleInterface = new JediTournamentInterface();
            consoleInterface.launchMenu();
        }
    }
}
