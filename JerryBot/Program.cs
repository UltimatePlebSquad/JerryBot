using Discord;
using Discord.API;
using Discord.Logging;
using Discord.Commands;
using Discord.ETF;
using Discord.Legacy;
using Discord.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;



namespace SearchOMatic
{
    class Program
    {

        string token = "MjQwMjgzMjc4Njg1MzcyNDE2.CvBEwQ.c27EuJWRiCvPb9vT1Z3ycVYbdLQ";
        int myRandomIndex = 0;
        bool purpose = false;
        int liambanned = 3;
        static void Main(string[] args)
        {
            new Program().Start();
        }

        private DiscordClient _client;


        public void Start()
        {



            _client = new DiscordClient(x =>
            {
                x.AppName = "JerrySmithBot";
                x.AppUrl = "";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            _client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });

            CreateComamnds();
            _client.ExecuteAndWait(async () =>
            {


                _client.MessageReceived += async (s, e) =>
                {

                    if (e.Message.IsAuthor) return;

                    if (purpose != true) {
                        await e.Channel.SendMessage("What is my purpose?");
                        purpose = true;
                    }

                    string spce = " ";

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Server: {e.Server.Name} Channel: {e.Channel.Name} User: {e.User} {spce} {e.Message.Text}");
                    Console.ResetColor();
                    if (e.Message.Channel.IsPrivate)
                    {
                        Console.WriteLine("Recieved DM " + e.User.Name + e.Message.Text);
                    }
                    if (e.Message.IsMentioningMe())
                    {

                        var myList = new List<string>(new[] { " Rick: Sorry, please proceed with your story about banging my daughter in high school. I'm not sure you wanna take romantic advice from this guy, Morty. His marriage is hanging from a thread. Jerry: My Marriage is fine, thank you!", " I wish that shotgun was my penis", " God? God's turning people into insect monsters, Beth. I'm the one beating them to death. Thank me.", " If you ever have an affair with that guy, I will come to the hotel room and blow my brains out all over your naked bodies.", " Nobody's killing me until after I catch my wife with another man." });
                        myList.Add(" The trick to cereal is keeping 70% above the milk. ");
                        myList.Add(@" Jerry: Hey, wait, hold on a second Rick. You wouldn't by any chance have some sort of crazy science thing you could whip up that could help make this dog a little smarter, would you? 
Rick: I thought the whole point of having a dog was to feel superior Jerry. If I were you I wouldn't pull that thread. 
");
                        myList.Add(" I don't get it and I don't need to.");
                        myList.Add(" I'm Mr. Crowbar, and here is my friend, who is also a crowbar!");
                        myList.Add(" Pluto's a planet.");
                        myList.Add(" It probably has space aids");
                        myList.Add(" Well look where being smart got you.");
                        myList.Add(" If I've learned one thing, it's that before you get anywhere in life, you gotta stop listening to yourself.");
                        var results = new List<string>();

                        var r = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);
                        for (int ii = 0; ii < 1; ii++)
                        {
                            myRandomIndex = r.Next(myList.Count);
                            results.Add(myList[myRandomIndex]);
                            myList.RemoveAt(myRandomIndex);
                        }
                        await e.Channel.SendMessage(e.User.Mention + string.Join("", results));
                    }

                };


                await _client.Connect(token, TokenType.Bot);
            });


        }





        public void CreateComamnds()
        {
            var cService = _client.GetService<CommandService>();

            cService.CreateCommand("game")
               .Description("Owner Only - Sets game")
               .Parameter("game", ParameterType.Required)
               .Do(async (e) =>
               {

                   if (e.User.Id == 131182268021604352)
                   {


                       if (e.GetArg("game") != null)
                       {

                           var newgame = e.GetArg("game");
                           _client.SetGame(newgame);
                           await e.Channel.SendMessage("Game set to: " + newgame);

                       }
                       else
                       {

                           await e.Channel.SendMessage("error");
                       }
                   }

               });

            cService.CreateCommand("liam")
               .Description("how much has liam been banned?")
               .Do(async (e) =>
               {
                   var text = System.IO.File.ReadAllText(@"C:\Users\Sam\Desktop\New Folder\bannedcount.txt");
                   await e.Channel.SendMessage("***LIAM*** HAS BEEN **BANNED** A TOTAL OF ***" + text + "*** TIMES. WHEN WILL HIS ***NEXT*** BAN COME?");
                   
               });

            cService.CreateCommand("liamgotkicked")
              .Description("bans liam")
              .Do(async (e) =>
              {
                  if (e.User.Id == 131182268021604352)
                  {
                      var x = 0;
                      var text = System.IO.File.ReadAllText(@"C:\Users\Sam\Desktop\New Folder\bannedcount.txt");
                      
                      if (Int32.TryParse(text, out x))
                      {
                          x = x + 1;
                          Console.WriteLine(x);
                          var writeresult = x.ToString();
                          Console.WriteLine(writeresult);
                          await e.Channel.SendMessage("Liam got banned *again*? pffttttt what did he do this time? Alright well, counting his ban!");
                          System.IO.File.WriteAllText(@"C:\Users\Sam\Desktop\New Folder\bannedcount.txt", writeresult);

                      }
                  }
                  else
                  {
                      await e.Channel.SendMessage("You aren't Fires!");
                  }
              });






        }

        //Ｆ  Ｉ  ＲＥ  Ｓ      Ａ  Ｅ  Ｓ  Ｔ  Ｈ  Ｅ  Ｔ  Ｉ  Ｃ
        //Ｃ  Ｒ  Ａ  Ｓ  Ｈ      Ｉ  Ｓ      Ａ        Ｆ  Ａ  Ｇ  Ｇ  Ｏ  Ｔ JUST LIKE DUSKA
        //Ｖ  Ａ  Ｐ  Ｏ  Ｒ  Ｇ  Ａ  Ｙ

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (e.Severity == LogSeverity.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine($"[{e.Severity}] [{e.Source}] [{e.Message}]");
            Console.ResetColor();


        }
    }
}
