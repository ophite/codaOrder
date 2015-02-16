using CodaRemoting;
using CodaRemoting.Datasets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApplication3.Entity.Repositories
{
    public static class Tools
    {
        public static void CutTable(ref CodaDS.CodaDataTable table)
        {
            string[] fieldArray = table.Columns.Cast<DataColumn>()
                                              .Select(i => i.ColumnName)
                                              .Except(new CodaDS.CodaDataTable().Columns.Cast<DataColumn>()
                                                                .Select(i => i.ColumnName)
                                                                .Except(new string[] { "OID" }))
                                                                .ToArray();
            CodaDS.CodaDataTable tempTable = (CodaDS.CodaDataTable)table.Copy();

            foreach (DataColumn column in table.Columns)
            {
                if (!fieldArray.Contains(column.ColumnName))
                    tempTable.Columns.Remove(column.ColumnName);
            }

            table = (CodaDS.CodaDataTable)tempTable.Copy();
        }

        public static CodaDS.CodaDataTable GetTableByEntity<T>(T[] lines)
            where T : BaseEntity
        {
            DocTradeDS.DocTradeLineDataTable lineDT = new DocTradeDS.DocTradeLineDataTable();

            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T line in lines)
            {
                DocTradeDS.DocTradeLineRow row = lineDT.NewDocTradeLineRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    var value = prop.GetValue(line);
                    if (value == null)
                        value = DBNull.Value;

                    row[prop.Name] = value;
                }

                lineDT.Rows.Add(row);
            }

            CodaDS.CodaDataTable dt = (CodaDS.CodaDataTable)lineDT;
            CutTable(ref dt);

            return dt;
        }

        /// <summary>
        /// TODO для нормальной работы нужно переделать под стороннюю библиотеку например MSMQ
        /// </summary>
        public static void SendEmail(string email, string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("kobernik.u@asnova.com");
            mail.Subject = "iOrder restore password";
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.asnova.com";
            smtp.Port = 25;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("kobernik.u", "Vostochnaya_93");// Enter seders User name and password  
            smtp.Send(mail);
        }
    }
}