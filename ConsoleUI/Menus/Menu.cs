using System.Collections.Generic;
using System;

namespace ConsoleUI.Menus
{
    /// <summary>
    /// Abstract class Menu
    /// </summary>
    public abstract class Menu
    {
        /// <summary>
        /// Whether menu is active
        /// </summary>
        public abstract bool IsActive { get; set; }

        /// <summary>
        /// ChooseOption of user
        /// </summary>
        public void ChooseOption()
        {
            PrintOptions();
            ExecuteCommand();
        }
        /// <summary>
        /// Execute user's option
        /// </summary>
        public void ExecuteCommand()
        {
            Console.WriteLine("Choose your option...");
            string option = Console.ReadLine();

            try
            {
                GetOperators()[option]();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Option is incorrect");
            }
        }
        /// <summary>
        /// Exit program
        /// </summary>
        protected void Exit()
        {
            IsActive = false;
        }
        /// <summary>
        /// Abstract print options
        /// </summary>
        protected abstract void PrintOptions();

        /// <summary>
        /// Abstract GetOperators
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<string, Action> GetOperators();



    }
}
