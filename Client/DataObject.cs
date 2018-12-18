using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class DataObject
    {
        // type => {sync, syncresponse, request, response, partallocation, datasharing, leader election, leader electionresponse,
        //          findtoken, findtokenresponse, }
        public String type;
        public String data;
        public String sender;
        public String receiver;

        public DataObject(String type, String data, String sender, String receiver)
        {
            this.type = type;
            this.data = data;
            this.sender = sender;
            this.receiver = receiver;
        }
    }
}
