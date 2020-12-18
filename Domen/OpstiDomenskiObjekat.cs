using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public abstract class OpstiDomenskiObjekat
    {
        private string searchCondition = string.Empty;
        public string SearchCondition
        {
            set { searchCondition = value; }
        }
        public string SetSearchCondition(string newSearchCondition)
        {
            searchCondition = $" where {newSearchCondition}";
            return searchCondition;
        }
        public string SetJoin(string newSearchCondition)
        {
            searchCondition = $" {newSearchCondition}";
            return searchCondition;
        }
        public abstract string GetTableName();
        public abstract string GetColumnNames();
        public abstract string GetColumnValues();
        public abstract string SetColumnValues();
        public abstract string GetSearchCondition();
        public abstract string GetJoin();
        public abstract OpstiDomenskiObjekat GetObject(SqlDataReader reader);
        public abstract List<OpstiDomenskiObjekat> GetObjects();
    }
}
