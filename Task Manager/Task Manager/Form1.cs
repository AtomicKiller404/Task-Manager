using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static void CreateDatabase()
        {
            string connectionString = "Data Source=E:\\GitHub\\Task-Manager\\Task Manager\\Task Manager\\db\\taskmanager.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string tableCommand = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT,
                    IsCompleted BOOLEAN NOT NULL CHECK (IsCompleted IN (0, 1)),
                    DueDate TEXT
                )";

                using (var createTable = new SqliteCommand(tableCommand, connection))
                {
                    createTable.ExecuteNonQuery();
                }

                connection.Close();
            }

            Console.WriteLine("Database and table created successfully.");
        }
    }
}
