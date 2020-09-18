using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BasicDebug
{
    public class DebugController : MonoBehaviour
    {
        //TODO: Make sure to edit the code to actually take in inputs.

        /* HOW TO MAKE A NEW DEBUG GOMMAND
        * 
        * 1. Initialise a public static debug command in the Commands region
        * 2. On Awake(). Define the debug command in the using the following:
        * 
        * [NAME OF COMMAND] = new DebugCommand("ID","Description","Format", () =>
        * {
        *      [code of what the command does goes here]
        * });
        * 
        * 3. Add the command variable into the commandList on Awake().
        */

        //UI variables
        bool showConsole;
        bool showHelp;
        string userInput;
        Vector2 scroll;

        //All the Command initialisations. 
        #region Commands;

        public static DebugCommand HELP;

        #endregion


        public List<object> commandList;

        //Create the Command List
        private void Awake()
        {
            //Define what the commands do here.
            #region Command Defitions

            HELP = new DebugCommand("help", "shows a list of commands", "help", () =>
            {
                showHelp = true;
            });

            #endregion

            //Remember to add any new commands to this list
            commandList = new List<object>
            {
                HELP
            };

        }

        private void onEnter()
        {
            if (showConsole)
            {
                //Get controller to read and activate input then deletes text
                HandleInput();
                userInput = "";
            }
        }


        //What happens when the debug button is pressed
        private void onToggle()
        {
            showConsole = !showConsole;
        }



        //Drawing the Debug UI
        private void OnGUI()
        {
            //Disables debug. If not going to be disabled then move on
            if (!showConsole) { return; }


            float y = 0;

            //Main debug box, where the user can type in their command
            userInput = GUI.TextField(new Rect(0, y, Screen.width, 20), userInput);

            //Displays the help box. Lising all of the avaliable commands
            if (showHelp)
            {
                GUI.Box(new Rect(0, y + 20, Screen.width, 100), "");

                Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

                scroll = GUI.BeginScrollView(new Rect(0, y + 20, Screen.width, 90), scroll, viewport);

                for (int i = 0; i < commandList.Count; i++)
                {
                    DebugCommandBase command = commandList[i] as DebugCommandBase;

                    string label = $"{command.commandFormat} - {command.commandDesctiption}";

                    Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                    GUI.Label(labelRect, label);
                }

                GUI.EndScrollView();

            }
        }

        void HandleInput()
        {

            string[] properties = userInput.Split(' ');

            //Looks though list of debug commands. If user input equals a commandID, run that debug command.
            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                if (userInput.Contains(commandBase.commandID))
                {
                    if (commandList[i] as DebugCommand != null)
                    {
                        //Casts to this type and invokes command
                        (commandList[i] as DebugCommand).Invoke();
                    }
                    else if (commandList[i] as DebugCommand<int> != null)
                    {
                        (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                    }
                }

            }
        }

    }
}

