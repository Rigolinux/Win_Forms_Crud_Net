using Crud_test_1._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_test_1._0.Data
{
    public class UserAdmin
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Users> Consult()
        {
            using (Test_FormEntities context = new Test_FormEntities())
            {
                return context.Users.AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void Save(Users user)
        {
            using (Test_FormEntities context = new Test_FormEntities())
            {
                
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void Update(Users user)
        {
            using (Test_FormEntities context = new Test_FormEntities())
            {
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


    }
}
