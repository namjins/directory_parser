using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryParser
{
    class Family
    {
        private string head = "";
        private List<String> phone_numbers = new List<String>();
        private string address = "";
        private List<String> children = new List<String>();

        //setters
        public void SetHead(String _head)
        {
            this.head = _head;
        }

        public void AddPhoneNumber(String _phone_number)
        {
            if (!this.phone_numbers.Contains(_phone_number))
            {
                this.phone_numbers.Add(_phone_number);
            }
        }

        public void SetAddress(String _address)
        {
            this.address = _address;
        }


        public void AddChild(String _child)
        {
            if (!this.children.Contains(_child))
            {
                this.children.Add(_child);
            }
        }


        //getters
        public string GetHead()
        {
           return this.head;
        }

        public List<String> GetPhoneNumbers()
        {
            return this.phone_numbers;
        }

        public string GetAddress()
        {
            return this.address;
        }

        public List<String> GetChildren()
        {
            return this.children;
        }

    }
}
