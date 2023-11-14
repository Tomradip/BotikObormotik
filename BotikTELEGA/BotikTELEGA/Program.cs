using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace PasswordBot
{
    class Program
    {


        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6784502678:AAFGefRikN7V0vBsoMvekczpr_Xo-E4sesw");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }


        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;



            if (message.Text != null && message.Text.StartsWith("/generate"))
            {
                string[] input = message.Text.Split(' ');
                if (input.Length == 2)
                {
                    if (int.TryParse(input[1], out int length) && length > 0)
                    {
                        string password = GeneratePassword(length);
                        await client.SendTextMessageAsync(message.Chat.Id, "Your random password: " + password);
                    }
                    else
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Please provide a valid number for password length.");
                    }
                }
                else
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "Invalid input. Please use the format: /generate <length>");
                }
            }
            if (message.Text != null)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Введите длину пароля типа /generate (длина)\n");
                    
                return;
            }
        }
    
            private static string GeneratePassword(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!?)(*$"; // Допустимые символы для пароля
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
            private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
            {
                throw new NotImplementedException();
            }
     }

}
