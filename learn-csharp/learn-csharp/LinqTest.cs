using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace learn_csharp
{
    class LinqTest
    {
        public List<LinqUser> users;

        public LinqTest()
        {
            this.users = new List<LinqUser>();
        }

        public void TestFirstOrDefault()
        {
            users.Add(new LinqUser(1, "abc"));
            users.Add(new LinqUser(2, "def"));
            users.Add(new LinqUser(3, "ghi"));
            users.Add(new LinqUser(4, "jkl"));

            LinqUser findUser = users.FirstOrDefault(user => String.Equals(user.Name, "ghi"));
            Console.WriteLine("findUser, id: {0}, name: {1}", findUser.Id, findUser.Name);

            // 이와 같은 경우 findUser2 에 null 이 대입된다.
            LinqUser findUser2 = users.FirstOrDefault(user => String.Equals(user.Name, "han"));
            if (findUser2 == null)
            {
                Console.WriteLine("findUser2 is null");
            }
        }
    }

    class LinqUser
    {
        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }

        private int id;
        private string name;

        public LinqUser(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

    }
}
