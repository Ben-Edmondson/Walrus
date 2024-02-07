namespace Walrus_Class_Library.Input
{
    public class UserInput
    {
        public string GetUserName(string errorMessage)
        {
            Console.WriteLine(errorMessage);
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
            Console.WriteLine(errorMessage);
            var userInput = Console.ReadLine();
            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine(errorMessage);
                userInput = Console.ReadLine();
            }
            return userInput;
        }

        public int Options(string errorMessage)
        {
            var userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out _))
            {
                Console.WriteLine(errorMessage);
                userInput = Console.ReadLine();
            }
            return int.Parse(userInput);
        }
    }
}
