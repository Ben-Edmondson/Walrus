namespace Walrus_Class_Library.Input
{
    public class UserInput
    {
        public string GetUserName(string errorMessage)
        {
            var userInput = Console.ReadLine();
            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine(errorMessage);
                userInput = Console.ReadLine();
            }
            return userInput;
        }

        public string GetPassword(string errorMessage)
        {
            var userInput = Console.ReadLine();
            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine(errorMessage);
                userInput = Console.ReadLine();
            }
            return userInput;
        }   
    }
}
