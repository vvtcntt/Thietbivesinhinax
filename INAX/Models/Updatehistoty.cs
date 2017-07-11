using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INAX.Models;
namespace INAX.Models
{
    public class Updatehistoty
    {
        public DatabaseINAXContext db = new DatabaseINAXContext();
        public static void UpdateHistory(string task,string FullName,string UserID)
        {

            DatabaseINAXContext db = new DatabaseINAXContext();
            tblHistoryLogin tblhistorylogin = new tblHistoryLogin();
            tblhistorylogin.FullName = FullName;
            tblhistorylogin.Task = task;
            tblhistorylogin.idUser = int.Parse(UserID);
            tblhistorylogin.DateCreate = DateTime.Now;
            tblhistorylogin.Active = true;
            
            db.tblHistoryLogins.Add(tblhistorylogin);
            db.SaveChanges();
           
        }
    }
}