//Sender


using Sender.Helpers;

new Exchange().Declare("ExLogTest");
new Queue().Declare("QuLogTest");
new Queue().Bind("QuLogTest", "ExLogTest", "ExLogTestArgent");
new Send().ExchangeSendMessage("Hello World", "ExLogTest");
new Send().ExchangeSendMessage("Hello World With Key", "ExLogTest", "QuLogTest");

Console.ReadKey();