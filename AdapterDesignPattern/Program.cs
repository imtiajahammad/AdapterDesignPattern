using System;

namespace AdapterDesignPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region original implementation
            EmployeeManager empManager = new EmployeeManager();
            string xmlValue = empManager.GetAllEmployees();
            #endregion


            #region adapter design pattern implementation
            IEmployee emp = new EmployeeAdapter();
            string value = emp.GetAllEmployees();
            #endregion




            #region original implementation
            EmployeeManagerSimple empManagerS = new EmployeeManagerSimple();
            string xmlValueSimple = empManagerS.GetAllEmployeesSimple();
            #endregion


            #region tubelight implementation
            EmployeeNoAdapterSimple empS = new EmployeeNoAdapterSimple();
            string valueSimple = empS.GetAllEmployeesSimple();
            #endregion

            Console.ReadLine();
            Console.WriteLine("Hello World END!");

        }
    }
    #region original scenario


    [Serializable]
    public class Employee
    {
        Employee() { }
        public Employee(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        [System.Xml.Serialization.XmlAttribute]
        public int ID { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public string Name { get; set; }
    }

    public class EmployeeManager
    {
        public System.Collections.Generic.List<Employee> employees;
        public EmployeeManager()
        {
            employees = new System.Collections.Generic.List<Employee>();
            this.employees.Add(new Employee(1, "John"));
            this.employees.Add(new Employee(2, "Michael"));
            this.employees.Add(new Employee(3, "Jason"));
        }
        public virtual string GetAllEmployees()
        {
            var emptyNamepsaces = new System.Xml.Serialization.XmlSerializerNamespaces(new[] { System.Xml.XmlQualifiedName.Empty });
            var serializer = new System.Xml.Serialization.XmlSerializer(employees.GetType());
            var settings = new System.Xml.XmlWriterSettings(); settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            using (var stream = new System.IO.StringWriter())
            using (var writer = System.Xml.XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, employees, emptyNamepsaces);
                return stream.ToString();
            }
        }

    }
    #endregion original

    #region adapter design pattern solution
    public interface IEmployee
    {
        string GetAllEmployees();
    }
    public class EmployeeAdapter : EmployeeManager, IEmployee
    {
        public override string GetAllEmployees()
        {
            string returnXml = base.GetAllEmployees();
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(returnXml);
            return Newtonsoft.Json.JsonConvert.SerializeObject(doc, Newtonsoft.Json.Formatting.Indented);
        }
    }
    #endregion adapter design pattern solution

    #region simple inheritance solution
    public class EmployeeManagerSimple
    {
        public System.Collections.Generic.List<Employee> employees;
        public EmployeeManagerSimple()
        {
            employees = new System.Collections.Generic.List<Employee>();
            this.employees.Add(new Employee(1, "John"));
            this.employees.Add(new Employee(2, "Michael"));
            this.employees.Add(new Employee(3, "Jason"));
        }
        public string GetAllEmployeesSimple()
        {
            var emptyNamepsaces = new System.Xml.Serialization.XmlSerializerNamespaces(new[] { System.Xml.XmlQualifiedName.Empty });
            var serializer = new System.Xml.Serialization.XmlSerializer(this.employees.GetType());
            var settings = new System.Xml.XmlWriterSettings(); settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            using (var stream = new System.IO.StringWriter())
            using (var writer = System.Xml.XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, employees, emptyNamepsaces);
                return stream.ToString();
            }
        }
    }
    public class EmployeeNoAdapterSimple : EmployeeManagerSimple
    {
        public new string GetAllEmployeesSimple()
        {
            string returnXml = base.GetAllEmployeesSimple();
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(returnXml);
            return Newtonsoft.Json.JsonConvert.SerializeObject(doc, Newtonsoft.Json.Formatting.Indented);
        }
    }
    #endregion simple inheritance solution



    #region  Adaptee attribute solution
    public class EmployeeManagerAdapteeAttribute
    {
        public System.Collections.Generic.List<Employee> employees;
        public EmployeeManagerAdapteeAttribute()
        {
            employees = new System.Collections.Generic.List<Employee>();
            this.employees.Add(new Employee(1, "John"));
            this.employees.Add(new Employee(2, "Michael"));
            this.employees.Add(new Employee(3, "Jason"));
        }
        public string GetAllEmployeesAdapteeAttribute()
        {
            var emptyNamepsaces = new System.Xml.Serialization.XmlSerializerNamespaces(new[] { System.Xml.XmlQualifiedName.Empty });
            var serializer = new System.Xml.Serialization.XmlSerializer(this.employees.GetType());
            var settings = new System.Xml.XmlWriterSettings(); settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            using (var stream = new System.IO.StringWriter())
            using (var writer = System.Xml.XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, employees, emptyNamepsaces);
                return stream.ToString();
            }
        }
    }
    public class EmployeeNoAdapterAdapteeAttribute
    {
        private EmployeeManagerAdapteeAttribute employeeManagerAdapteeAttribute;
        public EmployeeNoAdapterAdapteeAttribute()
        {
            this.employeeManagerAdapteeAttribute = new EmployeeManagerAdapteeAttribute();
        }
        public string GetAllEmployeesSimple()
        {
            string returnXml = employeeManagerAdapteeAttribute.GetAllEmployeesAdapteeAttribute();
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(returnXml);
            return Newtonsoft.Json.JsonConvert.SerializeObject(doc, Newtonsoft.Json.Formatting.Indented);
        }
    }
    #endregion  Adaptee attribute solution

}
