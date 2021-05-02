using DemoLinq.Context;
using DemoLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLinq.Factories
{
    /*
     * 
     * 
     * 
     */
    class DepartmentTuple
    {
        public Department Department { get; set; }
        public Employee Employee { get; set; }
    }

    class EmployeesOfDepartment 
        {
        public List<Employee> employees { get; set; }
        public Manager Manager { get; set; }
        }
    class CompanyFactory
    {
        private readonly CompanyContext _companyContext;

        public CompanyFactory(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }


        #region Employees
 
        public List<Employee> getEmployees()
        {
            return (from emp in _companyContext.Employees select emp).ToList<Employee>();
        }

        public Employee GetEmployeeByName(string n)
        {
            return _companyContext.Employees.Select(s => s).Where(st => st.Name.Equals(n)).FirstOrDefault();
        }

        public void AddEmployee(Employee e)
        {
            Console.WriteLine("Adding Employee {0}", e.Name);
            _companyContext.Employees.Add(e);
            _companyContext.SaveChanges();
        }

        public void UpdateEmployee(int id, Employee e)
        {
            Employee emp = _companyContext.Employees.FirstOrDefault(s => s.EmployeeId == id);
            _companyContext.Entry(emp).CurrentValues.SetValues(e);
            _companyContext.Update(emp);
            _companyContext.SaveChanges();
        }

        #endregion


        #region Managers

        public List<Manager> GetManagers()
        {
            return (from man in _companyContext.Managers.Include(m => m.Department) select man) .ToList();
        }

        public Manager GetManager(string s)
        {
            return _companyContext.Managers.Include(m => m.Department).Select(s => s).Where(w => w.Name == s).FirstOrDefault();
        }


        public List<Employee> GetEmployeesOfManager(Manager m)
        {

            var query = _companyContext.Employees.Select(e => e).Include(d => d.Department).Where(w => w.Department.DepartmentId == m.Department.DepartmentId);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            return new List<Employee>();
        }

        public void AddManager(Manager m)
        {
            Console.WriteLine("Adding Manager {0}", m.Name);
            _companyContext.Managers.Add(m);
            _companyContext.SaveChanges();
        }

        #endregion


         #region Department

        public void AddDepartment(Department d)
        {
            Console.WriteLine("Adding Department {0}", d.DepartmentName);
            _companyContext.Departments.Add(d);
            _companyContext.SaveChanges();
        }
        public List<Department> GetDepartments()
        {
            return _companyContext.Departments.ToList();
        }

        public DepartmentTuple getDepartmentFromEmployee(Employee emp)
        {
            //var query = (from d in _companyContext.Departments join e in _companyContext.Employees on d.DepartmentId equals e.Department.DepartmentId where e.EmployeeId == emp.EmployeeId select new DepartmentTuple { Department = d, Employee = e });

            var query = (from e in _companyContext.Employees.Include(es => es.Department) join d in _companyContext.Departments on e.Department.DepartmentId equals d.DepartmentId where e.EmployeeId == emp.EmployeeId select new DepartmentTuple{Department = d, Employee = e });

            return query.FirstOrDefault();
        }

        public Department GetDepartmentByName(string s)
        {
            return _companyContext.Departments.Select(s => s).Where(w => w.DepartmentName == s).FirstOrDefault();
        }


        public EmployeesOfDepartment GetDepartmentEmployees(Department d)
        {
            var query = ( from e in _companyContext.Employees.Include(d => d.Department).Where(w => w.Department == d) select e ).ToList();
            var query2 = (from m in _companyContext.Managers.Include(d => d.Department).Where(w => w.Department == d) select m).First();

            return new EmployeesOfDepartment { employees = query, Manager = query2 };

        }

        public Department getDepartmentByID(int v)
        {

            return _companyContext.Departments.Select(d => d).Where(a => a.DepartmentId == v).First();
        }

        #endregion

        public void SaveChanges()
        {
            _companyContext.SaveChanges();
        }

       
    }
}
