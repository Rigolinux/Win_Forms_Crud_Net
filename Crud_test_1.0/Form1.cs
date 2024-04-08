using Crud_test_1._0.Data;
using Crud_test_1._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_test_1._0
{
    public partial class Form1 : Form
    {
        private DataTable table;
        UserAdmin user = new UserAdmin();
        private Nullable<int> UserId;

        private void InitializeDatatable()
        {
            table = new DataTable();
            table.Columns.Add("UserID");
            table.Columns.Add("FirstName");
            table.Columns.Add("LastName");
            table.Columns.Add("Email");
            table.Columns.Add("DateOfBirth");
            dgUsersView.DataSource = table;
            dgUsersView.Columns["UserID"].HeaderText = "ID de Usuario";
            dgUsersView.Columns["FirstName"].HeaderText = "Nombre";
            dgUsersView.Columns["LastName"].HeaderText = "Apellido";
            dgUsersView.Columns["Email"].HeaderText = "Correo Electrónico";
            dgUsersView.Columns["DateOfBirth"].HeaderText = "Fecha de Nacimiento";

        }

        public Form1()
        {
            InitializeComponent();
            InitializeDatatable();
            Consult();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dpDateOfBirth.Value = DateTime.Now;
            UserId = null;
        }
        
        private void Consult()
        {
            List<Users> list = user.Consult();
            // remove all rows
            table.Clear();

            foreach(var item in list)
            {
                string formattedDateOfBirth = item.DateOfBirth.HasValue ? item.DateOfBirth.Value.ToString("yyyy-MM-dd") : "";
                DataRow row = table.NewRow();
                row["UserID"] = item.UserID; // Asigna el ID de Usuario a la columna correspondiente
                row["FirstName"] = item.FirstName; // Asigna el Nombre a la columna correspondiente
                row["LastName"] = item.LastName; // Asigna el Apellido a la columna correspondiente
                row["Email"] = item.Email; // Asigna el Correo Electrónico a la columna correspondiente
                row["DateOfBirth"] = formattedDateOfBirth;
                

                table.Rows.Add(row);
            }
            
            
        }

        private void LastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void dgUsersView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Obtén la fila seleccionada
                DataGridViewRow row = dgUsersView.Rows[e.RowIndex];

                // Llena los campos del formulario con los datos de la fila seleccionada
                txtFirstName.Text = row.Cells["FirstName"].Value?.ToString();
                txtLastName.Text = row.Cells["LastName"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                UserId = Convert.ToInt32(row.Cells["UserID"].Value);
                // dpDateOfBirth.Text = row.Cells["DateOfBirth"].Value?.ToString();
                string dateOfBirthString = row.Cells["DateOfBirth"].Value?.ToString();
                
                DateTime dateOfBirth;
                
                if (DateTime.TryParseExact(dateOfBirthString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
                {
                    // La conversión fue exitosa, dateOfBirth contiene el valor de la fecha
                    dpDateOfBirth.Text = dateOfBirth.ToString("yyyy-MM-dd");
                }
                else
                {
                    // La conversión falló, la cadena de fecha no estaba en el formato esperado
                    Console.WriteLine("La cadena de fecha no estaba en el formato esperado.");
                }







            }

        }

       


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (UserId != null)
                return;
            var userC = new Users()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                DateOfBirth = dpDateOfBirth.Value,
            };
            user.Save(userC);
            Consult();
            Clear();

        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UserId == null)
                return;

            var userM = new Users()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                DateOfBirth = dpDateOfBirth.Value,
                UserID = UserId.Value
            };

            user.Update(userM);
            Clear();
            Consult();
        }
    }
}

