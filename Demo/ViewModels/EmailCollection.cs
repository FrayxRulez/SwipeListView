// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Collections.ObjectModel;

namespace Demo.ViewModels
{
    public class EmailCollection : ObservableCollection<EmailObject>
    {
        public EmailCollection()
            : base()
        {
            Add(new EmailObject("Anne Wallace", "Did you have fun on your trip?", "It's awesome that you got the chance to ...", "1:34p", true));
            Add(new EmailObject("Jeff Smith", "Awesome job!", "I just went through the code you submitted ... ", "12:22p", true));
            Add(new EmailObject("Adriana Giorgi", "(no subject)", "Hello there. It's been really long since we ...", "11:11a", true));
            Add(new EmailObject("Richard Carey", "RE: Welcome aboard!", "Hey, Jose, Congratulations on your offer! Can't wait to ...", "10:34a", true));
            Add(new EmailObject("Bruno Denuit", "Where are we going for lunch today?", "I vote for the thai place across the street ...", "9:48a", false));
            Add(new EmailObject("Dave Barnett", "The supply room seems to be missing some ...", "Okay, who's the funny guy who thought it ...", "Thu", false));
            Add(new EmailObject("Gregory Weber", "Please send me the documents regarding ...", "I'm going to need them so I can schedule ...", "Thu", false));
            Add(new EmailObject("Erin M. Hagens", "OOF on 7/18", "Woke up with a very bad headache. I'll be working from home today.", "Wed", false));
            Add(new EmailObject("Michelle Alexander", "Missed conversation with Michelle Alexander.", "Michelle Alexander [8:40 a.m.]: Do you want ...", "Tue", false));
            Add(new EmailObject("Anne Wallace", "Wanna go watch a movie tonight?", "I know there aren't that many good movies this summer, but ...", "Mon", false));
            Add(new EmailObject("Jennifer Kensok", "Did you upload the photos from yesterday?", "Just curious. I want to show them to my friend ...", "Mon", false));
            Add(new EmailObject("Jonas Brandel", "hawaii pictures are up!", "Check them out. You'll have to tag yourselves in there 'cause I'm lazy. ;)", "Sun", false));
            Add(new EmailObject("Melissa Kerr", "Cancelled: BBQ", "The weather decided not to cooperate for this event.", "7/9", false));
            Add(new EmailObject("Steve Lasker", "How are things going?", "You should come and pay us a visit from time to time, you know?", "7/9", false));
            Add(new EmailObject("Andrew Sullivan", "Plans for the weekend!", "Anyone got plans for this weekend? I feel like ...", "7/8", false));
            Add(new EmailObject("Terry Earls", "Could you send me the email address of the guy that worked with ...", "Thanks a lot! Sent from my Windows Phone", "7/6", false));
            Add(new EmailObject("Belinda Newman", "Belinda @ Seattle, WA", "Yes, you heard right! I'm coming to visit you, so ...", "7/5", false));
            Add(new EmailObject("Anne Wallace", "Wanna go grab lunch today?", "It's been ages since last time. For some reason, you're always ...", "7/5", false));
            Add(new EmailObject("Christof Sprenger", "REMINDER: End of summer event!", "This is a reminder about tomorrow's event, which is gonna be ...", "7/5", false));
            Add(new EmailObject("Robert Rounthwaite", "Happy 4th of July!", "Can't wait to see you all tonight! Bring some ...", "7/4", false));
        }
    }
}
