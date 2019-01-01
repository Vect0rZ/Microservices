using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.CQRS;

namespace Microservices
{
    [Serializable]
    public class RegisterUser : ICommand
    {
        public string UserName = "User1";
        public string Email = "User1@user1.user1";
        public string Password = "User1Pass";
    }

    [Serializable]
    public class SmallData : IEvent
    {
        public int Data = 100;
    }

    [Serializable]
    public class EventSending : IEvent
    {
        public string a =  "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf" +
                           "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf";
        public string bytes_100 = "asdfasdfasasdfasdfasasdfasdfasasdfasdfasasdfasdfas";
        public int A;
        public int B;
        public string C;
        public List<string> big = new List<string>();
        public float x, y, z;
    }

    [Serializable]
    public class Event2 : IEvent
    {

    }
}
