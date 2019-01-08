using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSCF.Models.Entities;

namespace SSCF.Models
{
    public class Raffle
    {
        public Raffle(int f, int y)
        {
            SecretSantaContext DB = new SecretSantaContext();
            Family = DB.Families.Find(f);
            Year = y;
        }
        public Family Family { get; set; }
        public int Year { get; set; }

        public bool DrawNames()
        {
            // try to draw names 30 times
            for (int i = 0; i < 30; i++)
            {
                if (AttemptDrawNames(true))
                {
                    return true;
                }
            }
            // try to draw names 10 times, with more leniency
            for (int i = 0; i < 10; i++)
            {
                if (AttemptDrawNames(false))
                {
                    return true;
                }
            }

            return false;
        }
        bool AttemptDrawNames(bool enforceCannotBuyFor)
        {
            // declare variables
            SecretSantaContext DB = new SecretSantaContext();
            Random r = new Random();
            List<RaffleDetails> tickets = Family.RaffleDetails;
            Queue<Person> people = new Queue<Person>();

            // add family members to the queue. These are the irl people pulling the names
            foreach (RaffleDetails ticket in tickets)
            {
                people.Enqueue(ticket.Receiver);
            }

            // shuffle the tickets
            tickets = tickets.OrderBy(x => r.Next()).ToList();
            
            // pull each ticket
            foreach (RaffleDetails ticket in tickets)
            {
                List<Person> returnTicket = new List<Person>();

                // check if the person giving is allowed to buy for the receiver
                while (people.First().CannotBuyFor.Contains(ticket.Receiver))
                {
                    // put the name in a temporary pile to be added back to the queue
                    returnTicket.Add(people.Dequeue());

                    // if no more compatible names
                    if(people.Count == 0)
                    {
                        if (enforceCannotBuyFor)
                        {
                            return false;
                        }
                        else
                        {
                            // handle case of giver and receiver matching
                            if (ticket.Receiver == returnTicket.First())
                            {
                                // if only option, return false
                                if (returnTicket.Count == 1) { return false; }

                                // move matching name to the end
                                returnTicket.Reverse();
                            }

                            // ignore the CannotBuyFor rule
                            break;
                        }
                    }
                }
                // merge temp pile back into the queue
                foreach(Person p in returnTicket)
                {
                    people.Enqueue(p);
                }

                // assign receiver to giver and the year this raffle is for
                ticket.Giver = people.Dequeue();
                ticket.Year = Year;
            }

            // modify the entries in the database
            foreach(RaffleDetails t in tickets)
            {
                DB.Entry(t).State = System.Data.Entity.EntityState.Modified;
            }
            DB.SaveChanges();

            // send emails to everyone
            foreach (RaffleDetails t in tickets)
            {
                SendEmail(t);
            }

            return true;
        }
        void SendEmail(RaffleDetails t)
        {
            // construct email!

            string address = t.Giver.Email;
            string giver = t.Giver.FName + t.Giver.LName;
            string receiver = t.Receiver.FName + t.Receiver.LName;

        }
    }
}