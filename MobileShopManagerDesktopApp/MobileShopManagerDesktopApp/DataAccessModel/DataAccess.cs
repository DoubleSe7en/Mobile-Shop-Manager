using MobileShopManagerDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopManagerDesktopApp.DataAccessModel
{
    public class DataAccess
    {
        public static Account USP_GetAccountByUserName(string UserName, string passEnCode)
        {
            Account account = null;
            DataTable result = DataProvider.Ins.ExecuteQuery("USP_GetAccountByUserName @userName , @passWord", new object[] { UserName, passEnCode });
            if (result.Rows.Count > 0)
            {
                account = new Account(result.Rows[0]);
            }
            return account;
        }
        public static Employee USP_GetEmployeeByID(string ID)
        {
            Employee employee = null;
            DataTable result = DataProvider.Ins.ExecuteQuery("Exec USP_GetEmployeeByID @id", new object[] { ID });
            if (result.Rows.Count > 0)
            {
                employee = new Employee(result.Rows[0]);
            }
            return employee;
        }
        public static void USP_UpdateAccount(string UserName, string PassWord, string TypeAccount)
        {
            DataProvider.Ins.ExecuteNonQuery("exec USP_UpdateAccount @userName , @password , @typeAccount", 
                new object[] { UserName, PassWord, TypeAccount });
        }

        public static Customer USP_GetCustomerByID(string ID)
        {
            Customer customer = null;
            DataTable result =  DataProvider.Ins.ExecuteQuery("exec USP_GetCustomerByID @id", new object[] {ID});
            if (result.Rows.Count > 0)
            {
                customer = new Customer(result.Rows[0]);
            }
            return customer;
        }

        public static void USP_UpdatePointCustomer(string ID, int Point)
        {

            DataProvider.Ins.ExecuteNonQuery("exec USP_UpdatePointCustomer @id , @point", new object[] { ID, Point });
        }

        public static void USP_InsertBill(string IdCustomer, string IdEmployee, DateTime DateCheckOut, double TotalPayment)
        {
            DataProvider.Ins.ExecuteNonQuery("exec USP_InsertBill @idCustomer , @idEmployee , @dateCheckOut , @totalPayment", 
                new object[] { IdCustomer, IdEmployee, DateCheckOut, TotalPayment });
        }
        public static void USP_InsertBillWithoutIdCustomer(string IdEmployee, DateTime DateCheckOut, double TotalPayment)
        {
            DataProvider.Ins.ExecuteNonQuery("exec USP_InsertBillWithoutIdCustomer @idEmployee , @dateCheckOut , @totalPayment",
                new object[] { IdEmployee, DateCheckOut, TotalPayment });
        }

        public static int USP_GetIdBill(DateTime DateCheckOut)
        {
            object result =  DataProvider.Ins.ExecuteScalar("exec USP_GetIdtBill @dateCheckOut",
                new object[] { DateCheckOut });
            return (int)result;
        }

        public static void USP_InsertBillDetail(int IdBill, string IdProduct, int Count)
        {
            DataProvider.Ins.ExecuteNonQuery("exec USP_InsertBillDetail @idBill , @idProduct , @count",
                new object[] { IdBill, IdProduct, Count});
        }

        public static int USP_GetIdWareHouse(string IdProduct)
        {
            int Id = 0;
            DataTable result = DataProvider.Ins.ExecuteQuery("exec USP_GetIdWareHouse @idProduct",
                new object[] { IdProduct });
            if(result.Rows.Count > 0)
            {
                Id = (int)result.Rows[0]["Id"];
            }
            return Id;
        }

        public static ObservableCollection<ProductOrder> USP_GetAmountRemaining_ProductInWareHouse()
        {
            ObservableCollection<ProductOrder> list = new ObservableCollection<ProductOrder>();
            DataTable result = DataProvider.Ins.ExecuteQuery("exec USP_GetAmountRemaining_ProductInWareHouse");
            foreach (DataRow item in result.Rows)
            {
                Product product = new Product(item);
                ProductOrder productOrder = new ProductOrder(product, 0);
                productOrder.AmountRemaining = (int)item["SL"];
                if (productOrder.AmountRemaining == 0)
                {
                    productOrder.Status = "Hết hàng";
                }
                list.Add(productOrder);
            }
            return list;
        }

        public static int USP_GetAmountRemainingProductByID(string IDProduct)
        {
            object result = DataProvider.Ins.ExecuteScalar("exec USP_GetAmountRemainingProductByID @idProduct", new object[] { IDProduct});
            if(result != null)
                return (int)result;
            return 0;
        }
        public static void USP_UpdateAmountRemaining(int Id, int Count)
        {
             DataProvider.Ins.ExecuteNonQuery("exec USP_UpdateAmountRemaining @id , @count", new object[] { Id, Count });
        }
        public static ProductDetail USP_GetDetailProductByID(string IDProduct)
        {
            ProductDetail productDetail = null; 
            DataTable result = DataProvider.Ins.ExecuteQuery("exec USP_GetDetailProductByID @idProduct", new object[] { IDProduct});
            if (result.Rows.Count > 0)
            {
                productDetail = new ProductDetail(result.Rows[0]);
            }
            return productDetail;
        }
        public static ObservableCollection<ProductOrder> USP_GetProductByIDCategory(string IDCategory)
        {
            ObservableCollection<ProductOrder> list = new ObservableCollection<ProductOrder>();
            DataTable result = DataProvider.Ins.ExecuteQuery("exec USP_GetProductByIDCategory @idCategory", new object[] { IDCategory});
            foreach (DataRow item in result.Rows)
            {
                Product product = new Product(item);
                DirectoryInfo di = new DirectoryInfo(@"..\..\Images_Product\" + product.Name);
                if (di.Exists == false)
                {
                    di = new DirectoryInfo(@"..\Images_Product\" + product.Name);
                }
                product.PathAvatar = di.GetFiles()[0].FullName;
                ProductOrder productOrder = new ProductOrder(product, 0);
                productOrder.AmountRemaining = USP_GetAmountRemainingProductByID(product.Id);
                if (productOrder.AmountRemaining == 0)
                {
                    productOrder.Status = "Hết hàng";
                }
                list.Add(productOrder);
            }
            return list;
        }
        public static ObservableCollection<ProductOrder> USP_GetProductByIDCategoryOther()
        {
            ObservableCollection<ProductOrder> list = new ObservableCollection<ProductOrder>();
            DataTable result = DataProvider.Ins.ExecuteQuery("exec USP_GetProductByIDCategoryOther");
            foreach (DataRow item in result.Rows)
            {
                Product product = new Product(item);
                DirectoryInfo di = new DirectoryInfo(@"..\..\Images_Product\" + product.Name);
                if (di.Exists == false)
                {
                    di = new DirectoryInfo(@"..\Images_Product\" + product.Name);
                }
                product.PathAvatar = di.GetFiles()[0].FullName;
                ProductOrder productOrder = new ProductOrder(product, 0);
                productOrder.AmountRemaining = USP_GetAmountRemainingProductByID(product.Id);
                if (productOrder.AmountRemaining == 0)
                {
                    productOrder.Status = "Hết hàng";
                }
                list.Add(productOrder);
            }
            return list;
        }

        public static ObservableCollection<ProductOrder>LoadListProductOrder()
        {
            ObservableCollection<ProductOrder> list = new ObservableCollection<ProductOrder>();
            DataTable result = DataProvider.Ins.ExecuteQuery("Select * From Product");
            foreach (DataRow item in result.Rows)
            {
                Product product = new Product(item);
                DirectoryInfo di = new DirectoryInfo(@"..\..\Images_Product\" + product.Name);
                if (di.Exists == false)
                {
                    di = new DirectoryInfo(@"..\Images_Product\" + product.Name);
                }
                product.PathAvatar = di.GetFiles()[0].FullName;
                ProductOrder productOrder = new ProductOrder(product, 0);
                productOrder.AmountRemaining = USP_GetAmountRemainingProductByID(product.Id);
                if (productOrder.AmountRemaining == 0)
                {
                    productOrder.Status = "Hết hàng";
                }
                list.Add(productOrder);
            }
            return list;
        }
        public static ObservableCollection<ProductOrder> FindProductByName(string Name)
        {
            ObservableCollection<ProductOrder> listResult = new ObservableCollection<ProductOrder>();
            ObservableCollection<ProductOrder> listProduct = LoadListProductOrder();
            foreach (ProductOrder item in listProduct)
            {
                int check = item.Info.Name.IndexOf(Name, 0, StringComparison.OrdinalIgnoreCase);
                if (check != -1)
                {
                    listResult.Add(item);
                }
            }
            return listResult;
        }

        public static ObservableCollection<Category>LoadListCategory()
        {
            ObservableCollection<Category> list = new ObservableCollection<Category>();
            DataTable result = DataProvider.Ins.ExecuteQuery("Select * From Category");
            foreach (DataRow item in result.Rows)
            {
                Category category = new Category(item);
               
                list.Add(category);
            }
            return list;
        }
        public static Category FindCategory(string Name)
        {
            ObservableCollection<Category> listCategory = LoadListCategory();
            foreach (Category item in listCategory)
            {
                int check = item.Name.IndexOf(Name, 0, StringComparison.OrdinalIgnoreCase);
                if (check != -1)
                {
                    return item;
                }
            }
            return null;
        }

        public static ObservableCollection<Employee> LoadListEmployee()
        {
            ObservableCollection<Employee> list = new ObservableCollection<Employee>();
            DataTable result = DataProvider.Ins.ExecuteQuery("Select * From Employee");
            foreach (DataRow item in result.Rows)
            {
                Employee employee = new Employee(item);
                if(employee.IsDeleted == 1)
                    list.Add(employee);
            }
            return list;
        }

        public static ObservableCollection<Account> LoadListAccount()
        {
            ObservableCollection<Account> list = new ObservableCollection<Account>();
            DataTable result = DataProvider.Ins.ExecuteQuery("Select A.*, E.Name as NameEmployee From Account A, Employee E Where A.IDEmployee = E.ID");
            foreach (DataRow item in result.Rows)
            {
                Account account = new Account(item);
                account.NameEmployee = (string)item["NameEmployee"];
                list.Add(account);
            }
            return list;
        }
        public static ObservableCollection<ProductOrder> LoadProductSale()
        {
            ObservableCollection<ProductOrder> list = new ObservableCollection<ProductOrder>();
            DataTable resultData = DataProvider.Ins.ExecuteQuery("Select * From Product Where SalePercent > 0 Order by SalePercent Desc");
            foreach (DataRow item in resultData.Rows)
            {
                Product product = new Product(item);
                DirectoryInfo di = new DirectoryInfo(@"..\..\Images_Product\" + product.Name);
                if (di.Exists == false)
                {
                    di = new DirectoryInfo(@"..\Images_Product\" + product.Name);
                }
                ProductOrder productOrder = new ProductOrder(product, 0);
                productOrder.AmountRemaining = DataAccess.USP_GetAmountRemainingProductByID(product.Id);
                if (productOrder.AmountRemaining == 0)
                {
                    productOrder.Status = "Hết hàng";
                }
                product.PathAvatar = di.GetFiles()[0].FullName;
                list.Add(productOrder);
                if(list.Count == 5)
                {
                    break;
                }
            }
            return list;
        }

        public static ObservableCollection<ProductOrder> LoadProductHot()
        {
            ObservableCollection<ProductOrder> list = new ObservableCollection<ProductOrder>();
            DataTable resultData = DataProvider.Ins.ExecuteQuery("Select P.* From Product P, BillDetail BD " +
                                                                "Where P.ID = BD.IDProduct " +
                                                                "Group by P.ID, P.Name, P.IDCategory, P.Price, P.SalePercent, P.Price, P.IsDeleted " +
                                                                "Order by Sum(BD.Count) Desc");
            foreach (DataRow item in resultData.Rows)
            {
                Product product = new Product(item);
                DirectoryInfo di = new DirectoryInfo(@"..\..\Images_Product\" + product.Name);
                if (di.Exists == false)
                {
                    di = new DirectoryInfo(@"..\Images_Product\" + product.Name);
                }
                ProductOrder productOrder = new ProductOrder(product, 0);
                productOrder.AmountRemaining = DataAccess.USP_GetAmountRemainingProductByID(product.Id);
                if (productOrder.AmountRemaining == 0)
                {
                    productOrder.Status = "Hết hàng";
                }
                product.PathAvatar = di.GetFiles()[0].FullName;
                list.Add(productOrder);
                if (list.Count == 5)
                {
                    break;
                }
            }
            return list;
        }
        public static ObservableCollection<Product> LoadListProduct()
        {
            ObservableCollection<Product> list = new ObservableCollection<Product>();
            DataTable resultData = DataProvider.Ins.ExecuteQuery("Select* From Product");
            foreach (DataRow item in resultData.Rows)
            {
                Product product = new Product(item);
                list.Add(product);
            }
            return list;
        }

        public static ObservableCollection<WareHouse> LoadProductInWareHouse(DateTime DateStart, DateTime DateEnd)
        {
            ObservableCollection<WareHouse> list = new ObservableCollection<WareHouse>();
            DataTable resultData = DataProvider.Ins.ExecuteQuery("Select W.*, P.Name as NameProduct From WareHouse W, Product P " +
                                                                 "where DateAdded between '" + DateStart + "' and '"+ DateEnd + "' and W.IdProduct = P.ID" );
            foreach (DataRow item in resultData.Rows)
            {
                WareHouse wareHouse = new WareHouse(item);
                wareHouse.NameProduct = (string)item["NameProduct"];
                list.Add(wareHouse);
            }
            return list;
        }
        public static ObservableCollection<Bill> USP_GetListBill(DateTime DateStart, DateTime DateEnd)
        {
            ObservableCollection<Bill> list = new ObservableCollection<Bill>();
            DataTable resultData = DataProvider.Ins.ExecuteQuery("exec USP_GetListBill @dateStart , @dateEnd", new object[] { DateStart, DateEnd });
            foreach (DataRow item in resultData.Rows)
            {
                Bill bill = new Bill(item);
                if (item["NameCustomer"].ToString() != "")
                {
                    bill.NameCustomer = (string)item["NameCustomer"];
                }
                else
                {
                    bill.NameCustomer = "NULL";
                }
                bill.NameEmployee = (string)item["NameEmployee"];
                if (bill.IsDeleted == 1)
                {
                    list.Add(bill);
                }
            }
            return list;
        }
        public static ObservableCollection<BillDetail> USP_GetBillDetail(int IDBill)
        {
            ObservableCollection<BillDetail> list = new ObservableCollection<BillDetail>();
            DataTable resultData = DataProvider.Ins.ExecuteQuery("exec USP_GetBillDetail @idBill", new object[] { IDBill});
            foreach (DataRow item in resultData.Rows)
            {
                BillDetail billDetail = new BillDetail(item);
                billDetail.NameProduct = (string)item["NameProduct"];
                list.Add(billDetail);
            }
            return list;
        }

        public static ObservableCollection<Bill> FindBill(string SearchString, DateTime DateStart, DateTime DateEnd)
        {
            ObservableCollection<Bill> listBill = USP_GetListBill(DateStart, DateEnd);
            ObservableCollection<Bill> listResult = new ObservableCollection<Bill>();
            int Id = 0;
            double TotalPayment = 0;
            Int32.TryParse(SearchString, out Id);
            Double.TryParse(SearchString, out TotalPayment);

            foreach (Bill item in listBill)
            {
                if( item.Id == Id || 
                    item.IdCustomer.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 || 
                    item.NameCustomer.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 || 
                    item.IdEmployee.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.NameEmployee.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.TotalPayment == TotalPayment)
                {
                    listResult.Add(item);
                }
            }
            return listResult;
        }

        public static void UpdateIsDeleted(string table, int Isdeleted, object Where)
        {
            if(table == "Bill" && table == "WareHouse")
                 DataProvider.Ins.ExecuteNonQuery("Update " + table + " set IsDeleted = " + Isdeleted + "Where Id = " + Where);
            else
                DataProvider.Ins.ExecuteNonQuery("Update " + table + " set IsDeleted = " + Isdeleted + "Where Id = '" + Where + "'");
        }

        
        public static ObservableCollection<Employee> FindEmployee(string SearchString)
        {
            ObservableCollection<Employee> listEmployee = LoadListEmployee();
            ObservableCollection<Employee> listResult = new ObservableCollection<Employee>();
            DateTime date;
            double Salary = 0;
            DateTime.TryParse(SearchString, out date);
            Double.TryParse(SearchString, out Salary);

            foreach (Employee item in listEmployee)
            {
                if (item.Id.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.Name.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.Gender.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.Birthday == date||
                    item.DateStarted == date ||
                    item.CMND.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.Salary == Salary ||
                    item.PhoneNumber.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    listResult.Add(item);
                }
            }
            return listResult;
        }
        public static void InsertEmployee(Employee employee)
        {
            if(employee.Gender != "Nam" && employee.Gender != "Nữ")
            {
                employee.Gender = "Nam";
            }
            DataProvider.Ins.ExecuteNonQuery("exec USP_InsertEmployee @Id , @Name , @Gender , @Birthday , @CMND , @Salary , @PhoneNumber",
                new object[] {employee.Id, employee.Name, employee.Gender, employee.Birthday, employee.CMND, employee.Salary, employee.PhoneNumber});
        }

        public static void USP_UpdateEmployee(Employee employee)
        {
            if (employee.Gender != "Nam" && employee.Gender != "Nữ")
            {
                employee.Gender = "Nam";
            }
            DataProvider.Ins.ExecuteNonQuery("exec USP_UpdateEmployee @Id , @Name , @Gender , @Birthday , @CMND , @Salary , @PhoneNumber",
                new object[] { employee.Id, employee.Name, employee.Gender, employee.Birthday, employee.CMND, employee.Salary, employee.PhoneNumber });
        }
        public static ObservableCollection<Customer> LoadListCustomer()
        {
            ObservableCollection<Customer> list = new ObservableCollection<Customer>();
            DataTable result = DataProvider.Ins.ExecuteQuery("Select * From Customer");
            foreach (DataRow item in result.Rows)
            {
                Customer customer = new Customer(item);
                if (customer.IsDeleted == 1)
                    list.Add(customer);
            }
            return list;
        }
        public static ObservableCollection<Account> FindAccount(string SearchString)
        {
            ObservableCollection<Account> listAccount = LoadListAccount();
            ObservableCollection<Account> listResult = new ObservableCollection<Account>();
           

            foreach (Account item in listAccount)
            {
                if (item.IdEmployee.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.NameEmployee.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.UserName.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.TypeAccount.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    listResult.Add(item);
                }
            }
            return listResult;
        }
        public static void InsertAccount(Account account)
        {
            string passEnCode = EnCodePassWord(account.PassWord);
            DataProvider.Ins.ExecuteNonQuery("exec USP_InsertAccount @IDEmployee , @UserName , @PassWord , @TypeAccount",
                new object[] { account.IdEmployee, account.UserName, passEnCode, account.TypeAccount });
        }

        public static ObservableCollection<Customer> FindCustomer(string SearchString)
        {
            ObservableCollection<Customer> listCustomer = LoadListCustomer();
            ObservableCollection<Customer> listResult = new ObservableCollection<Customer>();
            int point = 0;
            Int32.TryParse(SearchString, out point);
            foreach (Customer item in listCustomer)
            {
                if (item.Id.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.Name.IndexOf(SearchString, 0, StringComparison.OrdinalIgnoreCase) != -1 ||
                    item.Point == point)
                {
                    listResult.Add(item);
                }
            }
            return listResult;
        }

        public static void InsertCustomer(Customer customer)
        {
            DataProvider.Ins.ExecuteNonQuery("exec USP_InsertCustomer @ID , @Name , @Point",
                new object[] { customer.Id, customer.Name, customer.Point });
        }

        //Chuyển từ base64 sang string
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        //Chuyển từ md5 sang base64
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        //Mã hóa
        public static string EnCodePassWord(string passWord)
        {
            string passEnCode = MD5Hash(Base64Encode(passWord));
            return passEnCode;
        }
    }
}
