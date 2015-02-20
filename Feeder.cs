/*
 * Created by SharpDevelop.
 * User: Webster Systems
 * Date: 18/02/2015
 * Time: 17:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Messaging;

namespace Feed
{
	class Feeder
	{
		// Class variables constructed in constructor then used outside class
		public MessageQueue msQueue = null;
		
		public void NewTopic (string destination)
		{
			if (MessageQueue.Exists(@".\Private$\" + destination))
			{
				msQueue = new MessageQueue(@".\Private$\" + destination);
				msQueue.Label = "Testing Queue";
			}
			else
			{
				// Create the Queue
				MessageQueue.Create(@".\Private$\" + destination);
				msQueue = new MessageQueue(@".\Private$\" + destination);
				msQueue.Label = "Newly Created Queue";
			}
		}		

		public static void Main(string[] args)
		{
			Console.WriteLine("Starting feeder...");
			
			Feeder f = new Feeder(); 
			f.NewTopic("cttofix");
			
			Console.WriteLine("");
			Console.WriteLine("USE:");
			Console.WriteLine("q: quote request for 1mio EURUSD");
			Console.WriteLine("qc: quote cancel");			
		    Console.WriteLine("md: market data snapshot in 1mio EURUSD");
		    Console.WriteLine("mr: market data stream in 1mio EURUSD");		
		    Console.WriteLine("u: unsubscribe from market data stream");				    
		    Console.WriteLine("o: place new order to buy 1mio EURUSD");	
			Console.WriteLine("logout: logout");	
			Console.WriteLine("logon: logon");			
		    Console.WriteLine("quit: exit program");	

		    while (true)
		    {
		    	String line = Console.ReadLine();
		    	
		    	if (line == "quit") break;
		    	
		    	f.Publish(line);		    	
		    }
		    
		    Console.WriteLine("Out Feeder");
		    Console.ReadKey();
		}
		
		public void Publish(String data)
		{
			msQueue.Send(data, "cttofix");
		}
	}
}