using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.Data;


namespace ADO_Net_DZ2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataSet setCounter = new DataSet();
                DataSet setProduct = new DataSet();
                SqlDataAdapter adapterCounter = new SqlDataAdapter("SELECT * FROM Counter", "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Storage; Integrated Security = True");
                SqlDataAdapter adapterProduct = new SqlDataAdapter(" SELECT * FROM Product", "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Storage; Integrated Security = True");
                
                adapterCounter.Fill(setCounter);
                adapterProduct.Fill(setProduct);
                SqlCommandBuilder builderCounter = new SqlCommandBuilder(adapterCounter);
                SqlCommandBuilder builderProduct = new SqlCommandBuilder(adapterProduct);

                adapterCounter.InsertCommand = builderCounter.GetInsertCommand();
                adapterCounter.UpdateCommand = builderCounter.GetUpdateCommand();
                adapterCounter.DeleteCommand = builderCounter.GetDeleteCommand();

                adapterProduct.InsertCommand = builderProduct.GetInsertCommand();
                adapterProduct.UpdateCommand = builderProduct.GetUpdateCommand();
                adapterProduct.DeleteCommand = builderProduct.GetDeleteCommand();

                /*Console.WriteLine(builderCounter.GetInsertCommand().CommandText);
                Console.WriteLine(builderCounter.GetUpdateCommand().CommandText);
                Console.WriteLine(builderCounter.GetDeleteCommand().CommandText);

                Console.WriteLine(builderProduct.GetInsertCommand().CommandText);
                Console.WriteLine(builderProduct.GetUpdateCommand().CommandText);
                Console.WriteLine(builderProduct.GetDeleteCommand().CommandText);

                Console.WriteLine(adapterCounter.InsertCommand);
            // автоматически генерируемы параметры. запрос пишется вручную (экранированние)
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName= "@p1";
                sqlParameter.Value = "drop table counter";
                SqlCommand command = new SqlCommand("SELECT * FROM Counter WHERE id = @p1");
                command.Parameters.Add(sqlParameter);*/

                //передаем параметры в таблицу
                //DataRow dr = setCounter.Tables[0].NewRow();
                //dr.SetField(0, 2);
                //dr.SetField(1, "Роллы");
                //dr.SetField(2, 10);
                //dr.SetField(3, DateTime.Now);
                //setCounter.Tables[0].Rows.Add(dr);
                //третий вариант записи заполнения таблицы данными
               // setCounter.Tables[0].Rows.Add(3,"Шаверма", 12, DateTime.Now);

                //добавление товара в таблицу третий вариант
                /*for(int i=4; i<100; i++)
                {
                    
                    setCounter.Tables[0].Rows.Add(i, "Бургер", 15, DateTime.Now);
                }*/
                //показать максимальное количество товаров у поставщика
                adapterCounter.Update(setCounter);
                int maxcount = 0;
                string counterName = "";
                foreach(DataRow  dr in setCounter.Tables[0].Rows)
                {
                    if(dr.Field<int>("Count")> maxcount)
                    {maxcount = dr.Field<int>("Count");
                        counterName = dr.Field<string>("CounterName");

                    }
                        
                }
                Console.WriteLine(counterName + " : " + maxcount + "товаров");

                DataViewManager dvm = new DataViewManager(setCounter);
                DataView dv = dvm.CreateDataView(setCounter.Tables[0]);
                dv.RowFilter = "Count = MAX(Count)";
                Console.WriteLine(dv.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
